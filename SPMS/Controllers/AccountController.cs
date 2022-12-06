using AutoMapper;

using CPMS.Models;

using Domain.Entities;
using Domain.Interfaces;

using HtmlAgilityPack;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using System.Net;
using System.Web;

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

		[Route("forgotpass")]
		[HttpGet]
		public IActionResult ForgotPass()
		{
			return View();
		}

		[HttpGet]
		public IActionResult ForgotPassConfirm()
		{
			return View();
		}

		[Route("resetpassword")]
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
			try
			{
				var userName = model.UserName.Replace("/", string.Empty);

				User user = model.UserName.Contains('@')
					? _userManager.FindByEmailAsync(userName).Result
					: _userManager.FindByNameAsync(userName).Result;

				if (user is null)
				{
					var verifyUserType = VerifyUser(userName, model.Password);
					if (verifyUserType == null)
					{
						ModelState.AddModelError("", "Invalid Username/Password");
						return View();
					}

					var req = await Register(model.Password);
				}

				var userLogin = _auth.AuthenticateUser(userName, model.Password);
				if (userLogin != null)
				{
					if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
						return Redirect(returnUrl);

					if (userLogin.Role.Contains("Student"))
					{
						return RedirectToAction("Index", "dashboard", new { area = "st" });
					}
					else if (userLogin.Role.Contains("Supervisor"))
					{
						return RedirectToAction("Index", "dashboard", new { area = "su" });
					}
					else if (userLogin.Role.Contains("Admin"))
					{
						return RedirectToAction("Index", "dashboard", new { area = "ad" });
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
		[ValidateAntiForgeryToken]
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

				return _role;
			}


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
						HtmlDocument htmlDoc = new();
						htmlDoc.LoadHtml(student.Content.ReadAsStringAsync().Result);

						var details = htmlDoc.DocumentNode.SelectNodes("//*[@id=\"side-menu\"]/li[8]/a");
						foreach (var item in details)
						{
							_imageUrl = $"https://www.federalpolyede.edu.ng/{htmlDoc.DocumentNode.SelectSingleNode("//*[@id=\"side-menu\"]/li[8]/a/div/img[1]/@src[1]").OuterHtml.Substring(13, 29)}";
							_fullName = HttpUtility.HtmlDecode(item.SelectSingleNode("//*[@id=\"side-menu\"]/li[8]/a/strong/div[1]").InnerText);
							_username = HttpUtility.HtmlDecode(item.SelectSingleNode("//*[@id=\"side-menu\"]/li[8]/a/strong/div[2]").InnerText);
							_dpt = HttpUtility.HtmlDecode(item.SelectSingleNode("//*[@id=\"side-menu\"]/li[8]/a/strong/div[3]").InnerText);
							_level = HttpUtility.HtmlDecode(item.SelectSingleNode("//*[@id=\"side-menu\"]/li[8]/a/strong/div[4]").InnerText).Substring(0, 16);
							_cgpa = HttpUtility.HtmlDecode(item.SelectSingleNode("//*[@id=\"side-menu\"]/li[8]/a/strong/div[4]/div").InnerText);
							_role = "Student";
						}
					}
				}
			}
			return _role;
		}

		internal async Task<bool> Register(dynamic password)
		{
			if (password != null)
			{
				User newUser = new()
				{
					FullName = _fullName,
					UserName = _username,
					ImageUrl = _imageUrl
				};

				Department newdpt = new()
				{
					Name = _dpt,
				};
				var dptname = _context.Departments.GetById(newdpt.Name);
				if (dptname == null)
				{
					_context.Departments.Add(dptname);
					await _context.SaveAsync();
				}


				var request = _userManager.CreateAsync(newUser, password).Result;
				if (request.Succeeded)
				{
					string role = _role;
					var addRole = _userManager.AddToRoleAsync(newUser, role).Result;
					if (addRole.Succeeded)
					{
						try
						{
							if (role.Equals("Student"))
							{
								StudentVM student = new()
								{
									UserId = newUser.Id,
									MatricNo = _username,
									ImageUrl = _imageUrl,
									DepartmentId = dptname.DepartmentId,
									FullName = _fullName,
									Level = _level
								};
								var studentEntity = _mapper.Map<Student>(student);
								_context.Students.Add(studentEntity);
								await _context.SaveAsync();
								return true;
							}
							else if (role.Equals("Student"))
							{
								return true;
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
			return false;
		}
	}
}