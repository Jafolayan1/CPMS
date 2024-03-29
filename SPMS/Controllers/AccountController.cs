﻿using AutoMapper;

using Domain.Entities;
using Domain.Interfaces;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using SPMS.Models;

namespace SPMS.Controllers
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
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [Route("fpassword")]
        [HttpGet]
        public IActionResult ForgotPass()
        {
            return View();
        }

        [Route("fconfirmation")]
        [HttpGet]
        public IActionResult ForgotPassConfirm()
        {
            return View();
        }

        [Route("rpassword")]
        [HttpGet]
        public IActionResult ResetPass(string token, string email)
        {
            var model = new ResetPasswordVM { Token = token, Email = email };
            return View(model);
        }

        [Route("rconfirmation")]
        [HttpGet]
        public IActionResult ResetPassConfirm()
        {
            return View();
        }

        [Route("unauthorized")]
        [HttpGet]
        public IActionResult Unauthorize()
        {
            return View();
        }

        [Route("logout")]
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _auth.Signout();
            return Redirect("~/");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM model, string returnUrl)
        {
            var userName = model.UserName.Replace("/", string.Empty);

            User user = model.UserName.Contains('@')
                ? await _userManager.FindByEmailAsync(userName)
                : await _userManager.FindByNameAsync(userName);


            if (user is null)
            {
                var verifyUserType = VerifyUser(model.UserName, model.Password);
                if (verifyUserType == null)
                {
                    ViewData["ErrorMessage"] = "Invalid Username/Password";
                }
                else
                {
                    var req = await Register(model.Password);
                    ViewData["InfoMessage"] = "User registered successfully. Please log in.";
                }
                return View();
            }
            var userLogin = _auth.AuthenticateUser(userName, model.Password);
            if (userLogin != null)
            {
                if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                    return Redirect(returnUrl);

                switch (userLogin.Role)
                {
                    case string[] role when role.Contains("Student"):
                        return RedirectToAction("index", "dashboard", new { area = "Graduate" });
                    case string[] role when role.Contains("Supervisor"):
                        return RedirectToAction("dashboard", "dashboard", new { area = "Staff" });
                    case string[] role when role.Contains("Admin"):
                        return RedirectToAction("index", "dashboard", new { area = "Admin" });
                    default:
                        ViewData["ErrorMessage"] = "Invalid Username/Password";
                        return View();
                }
            }
            else
            {
                ViewData["ErrorMessage"] = "Invalid Username/Password";
                return View();
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordVM model, MailRequest request)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(model.Email);

                if (user is null)
                    return RedirectToAction(nameof(ForgotPassConfirm));

                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var callback = Url.Action(nameof(ResetPass), "Account", new { token, email = user.Email }, Request.Scheme);

                request.ToEmail = model.Email;
                request.Subject = "Reset Password Token";
                await _mailService.SendEmailAsync(request, $"<a href='{callback}'>Click this link to reset your paswword,<br>");

                return RedirectToAction(nameof(ForgotPassConfirm));
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "One or more errors occurred in the server.");
                return RedirectToAction("Login");
            }
        }

        [HttpPost]
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

        internal string VerifyUser(string username, string password)
        {
            var staff = _context.Supervisors.GetByFileNo(username);

            if (staff != null)
            {
                _role = "Supervisor";
                _imageUrl = staff.ImageUrl;
                _fullName = staff.FullName;
                _username = staff.FileNo;
                _dpt = staff.Department.Name;
                _phoneNo = staff.PhoneNumber;
                _email = staff.Email;
            }
            else
            {
                var student = _context.Students.GetByMatric(username);
                _role = "Student";
                _imageUrl = student.ImageUrl;
                _fullName = student.FullName;
                _username = student.MatricNo;
                _dpt = student.Department.Name;
                _phoneNo = student.PhoneNumber;
                _email = student.Email;
            }
            return _role;


            //var baseAddress = new Uri("https://www.federalpolyede.edu.ng");
            //var cookieContainer = new CookieContainer();
            //using (var handler = new HttpClientHandler() { CookieContainer = cookieContainer })
            //using (var client = new HttpClient(handler) { BaseAddress = baseAddress })
            //{
            //	var homePageResult = client.GetAsync("/");
            //	homePageResult.Result.EnsureSuccessStatusCode();

            //	var content = new FormUrlEncodedContent(new[]
            //	{
            //		new KeyValuePair<string, string>("userN", username),
            //		new KeyValuePair<string, string>("password", password),
            //		new KeyValuePair<string, string>("num", "7"),
            //		new KeyValuePair<string, string>("subM", "Login"),
            //	});

            //	if (content != null)
            //	{
            //		var req = client.PostAsync("/admin_student/login_process2.php", content).Result;
            //		req.EnsureSuccessStatusCode();

            //		//if (req.RequestMessage.RequestUri.LocalPath.Equals("/admin_student/admin.php"))
            //		//{
            //		//var student = client.GetAsync("/admin_student/biodata.php").Result;
            //		var student = client.GetAsync("/admin_student/admin.php").Result;

            //		HtmlDocument htmlDoc = new();
            //		htmlDoc.LoadHtml(student.Content.ReadAsStringAsync().Result);

            //		var details = htmlDoc.DocumentNode.SelectNodes("//*[@id=\"side-menu\"]/li[8]/a");
            //		_phoneNo = htmlDoc.DocumentNode.SelectSingleNode("//*[@id=\"hiddenField5\"]").GetAttributeValue("value", null);
            //		_email = htmlDoc.DocumentNode.SelectSingleNode("//*[@id=\"email\"]").GetAttributeValue("value", null);
            //		foreach (var item in details)
            //		{
            //			_imageUrl = $"https://www.federalpolyede.edu.ng/{htmlDoc.DocumentNode.SelectSingleNode("//*[@id=\"side-menu\"]/li[8]/a/div/img[1]/@src[1]").OuterHtml.Substring(13, 29)}";
            //			_fullName = HttpUtility.HtmlDecode(item.SelectSingleNode("//*[@id=\"side-menu\"]/li[8]/a/strong/div[1]").InnerText);
            //			_username = HttpUtility.HtmlDecode(item.SelectSingleNode("//*[@id=\"side-menu\"]/li[8]/a/strong/div[2]").InnerText);
            //			_dpt = HttpUtility.HtmlDecode(item.SelectSingleNode("//*[@id=\"side-menu\"]/li[8]/a/strong/div[3]").InnerText);
            //			_level = HttpUtility.HtmlDecode(item.SelectSingleNode("//*[@id=\"side-menu\"]/li[8]/a/strong/div[4]").InnerText).Substring(0, 16);
            //			_cgpa = HttpUtility.HtmlDecode(item.SelectSingleNode("//*[@id=\"side-menu\"]/li[8]/a/strong/div[4]/div").InnerText);
            //			_role = "Student";
            //		}

            //	}
            //}
        }


        internal async Task<bool> Register(dynamic password)
        {
            if (password == null)
            {
                return false;
            }

            var newUser = new User
            {
                FullName = _fullName,
                UserName = _username,
                ImageUrl = _imageUrl,
                Email = _email,
                PhoneNumber = _phoneNo
            };

            var request = await _userManager.CreateAsync(newUser, password);
            if (!request.Succeeded)
            {
                return false;
            }

            var role = _role;
            var addRole = await _userManager.AddToRoleAsync(newUser, role);
            if (!addRole.Succeeded)
            {
                await _userManager.DeleteAsync(newUser);
                return false;
            }

            try
            {
                switch (role)
                {
                    case "Student":
                        var studentEntity = _context.Students.GetByMatric(_username);
                        studentEntity.UserId = newUser.Id;
                        _context.Students.Update(studentEntity);
                        _context.SaveChanges();
                        break;
                    case "Supervisor":
                        var supervisorEntity = _context.Supervisors.GetByFileNo(_username);
                        supervisorEntity.UserId = newUser.Id;
                        _context.Supervisors.Update(supervisorEntity);
                        _context.SaveChanges();
                        break;
                    default:
                        break;
                }
                return true;
            }
            catch (DbUpdateException)
            {
                await _userManager.DeleteAsync(newUser);
            }
            catch (Exception)
            {
                await _userManager.DeleteAsync(newUser);
            }
            return false;
        }

    }
}