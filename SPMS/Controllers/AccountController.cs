using AutoMapper;

using CPMS.Models;

using Domain.Entities;
using Domain.Interfaces;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using System.Net;

namespace CPMS.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IAuthenticationService _auth;
        private readonly UserManager<User> _userManager;
        private readonly IMailService _mailService;
        private readonly IUnitOfWork _context;
        private readonly IMapper _mapper;
        private readonly IFileHelper _file;

        public AccountController(IAuthenticationService auth, UserManager<User> userManager, IMailService mailService, IUnitOfWork context, IMapper mapper, IUserAccessor o, IFileHelper file) : base(o)
        {
            _auth = auth;
            _userManager = userManager;
            _mailService = mailService;
            _context = context;
            _mapper = mapper;
            _file = file;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ForgotPassConfirm()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ResetPassword(string token, string email)
        {
            var model = new ResetPasswordVM { Token = token, Email = email };
            return View(model);
        }

        [HttpGet]
        public IActionResult ResetPassConfirm()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Unauthorize()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _auth.Signout();
            return Redirect("~/");
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM model, StudentVM student, SupervisorVM supervisor, string returnUrl)
        {
            try
            {
                User user = model.UserName.Contains('@')
                    ? _userManager.FindByEmailAsync(model.UserName).Result
                    : _userManager.FindByNameAsync(model.UserName).Result;

                if (user is null)
                {
                    var verifyUserType = VerifyUser(model.UserName, model.Password);
                    if (!string.IsNullOrEmpty(verifyUserType))
                    {
                        User newUser = new()
                        {
                            UserName = model.UserName.ToUpper(),
                            ImageUrl = "https://cdn-icons-png.flaticon.com/512/3135/3135755.png"
                        };
                        var request = _userManager.CreateAsync(newUser, model.Password).Result;
                        if (request.Succeeded)
                        {
                            string role = verifyUserType == "Student" ? "Student" : "Supervisor";
                            var addRole = _userManager.AddToRoleAsync(newUser, role).Result;
                            if (addRole.Succeeded)
                            {
                                try
                                {
                                    if (role.Equals("Student"))
                                    {
                                        student.UserId = newUser.Id;
                                        student.MatricNo = model.UserName.ToUpper();
                                        student.ImageUrl = "https://cdn-icons-png.flaticon.com/512/3135/3135755.png";
                                        var studentEntity = _mapper.Map<Student>(student);
                                        _context.Students.Add(studentEntity);
                                        await _context.SaveAsync();
                                    }
                                    else if (role.Equals("Supervisor"))
                                    {
                                        supervisor.UserId = newUser.Id;
                                        supervisor.EmployeeNo = model.UserName.ToUpper();
                                        supervisor.ImageUrl = "https://cdn-icons-png.flaticon.com/512/3135/3135755.png";
                                        var supervisorEntity = _mapper.Map<Supervisor>(supervisor);
                                        _context.Supervisors.Add(supervisorEntity);
                                        await _context.SaveAsync();
                                    }
                                }
                                catch (Exception)
                                {
                                    await _userManager.DeleteAsync(newUser);
                                    ModelState.AddModelError("", "One or more errors occurred in the server");
                                }
                            }
                        }
                    }
                }

                var userLogin = _auth.AuthenticateUser(model.UserName, model.Password);
                if (userLogin != null)
                {
                    if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                        return Redirect(returnUrl);

                    if (userLogin.Role.Contains("Student"))
                    {
                        return RedirectToAction("Index", "dashboard", new { area = "students" });
                    }
                    else if (userLogin.Role.Contains("Supervisor"))
                    {
                        return RedirectToAction("Index", "dashboard", new { area = "supervisors" });
                    }
                    else if (userLogin.Role.Contains("Admin"))
                    {
                        return RedirectToAction("Index", "dashboard", new { area = "admins" });
                    }
                }
                ModelState.AddModelError("", "Invalid Username/Password");
                return View();
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "One or more errors occurred in the server.");
                return View();
            }
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordVM model, MailRequest request)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user is null)
                    return RedirectToAction(nameof(ForgotPassConfirm));

                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var callback = Url.Action(nameof(ResetPassword), "Account", new { token, email = user.Email }, Request.Scheme);

                request.ToEmail = model.Email;
                request.Subject = "Reset Password Token";
                await _mailService.SendEmailAsync(request, $"<a href='{callback}'>Click this link to reset your paswword,<br>");

                return RedirectToAction(nameof(ForgotPassConfirm));
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "One or more errors occurred in the server.");
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPasswordVM model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
                RedirectToAction(nameof(ResetPassConfirm));

            var resetPassResult = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);
            if (!resetPassResult.Succeeded)
            {
                foreach (var error in resetPassResult.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }

                return View();
            }
            return RedirectToAction(nameof(ResetPassConfirm));
        }

        public string VerifyUser(string username, string password)
        {
            var user = string.Empty;
            bool isStudent = false;
            bool isStaff = false;
            var baseAddress = new Uri("https://www.federalpolyede.edu.ng");
            var cookieContainer = new CookieContainer();
            using (var handler = new HttpClientHandler() { CookieContainer = cookieContainer })
            using (var client = new HttpClient(handler) { BaseAddress = baseAddress })
            {
                var homePageResult = client.GetAsync("/");
                homePageResult.Result.EnsureSuccessStatusCode();

                var content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("userN", username),
                    new KeyValuePair<string, string>("password", password),
                });

                if (content != null)
                {
                    var student = client.PostAsync("/admin_student/login_process2.php", content).Result;
                    student.EnsureSuccessStatusCode();
                    if (student.RequestMessage.RequestUri.LocalPath.Equals("/admin_student/admin.php"))
                    {
                        isStudent = true;
                    }
                    var staff = client.PostAsync("/admin_main/login_process.php", content).Result;
                    staff.EnsureSuccessStatusCode();
                    if (staff.RequestMessage.RequestUri.LocalPath.Equals("/admin_main/admin.php"))
                    {
                        isStaff = true;
                    }
                }

                if (isStudent == true && isStaff == false)
                {
                    user = "Student";
                }
                else if (isStaff == true && isStudent == false)
                {
                    user = "Staff";
                }
            }
            return user;
        }
    }
}