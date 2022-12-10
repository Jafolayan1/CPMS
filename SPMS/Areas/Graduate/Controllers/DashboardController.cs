using AutoMapper;

using Domain.Entities;
using Domain.Interfaces;

using Infrastructure;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CPMS.Areas.Graduate.Controllers
{
	public class DashboardController : BaseController
	{
		private readonly IAuthenticationService _auth;
		private readonly UserManager<User> _userManager;
		private readonly IMapper _mapper;
		private readonly IFileHelper _file;
		private readonly ApplicationContext _repo;

		public DashboardController(IAuthenticationService auth, UserManager<User> userManager, IMailService mail, IUnitOfWork context, IMapper mapper, IUserAccessor o, IFileHelper file, ApplicationContext repo) : base(o, context, mail)
		{
			_auth = auth;
			_userManager = userManager;
			_mapper = mapper;
			_file = file;
			_repo = repo;
		}

		[Route("dashboard/index")]
		[HttpGet]
		public IActionResult Index()
		{
			ViewData["Noti"] = GetNoti();

			return View();
		}

		[Route("dashboard/notify")]
		[HttpGet]
		public IActionResult Notify()
		{
			var noti = _context.Notifications.GetAll();
			ViewData["notifs"] = noti;
			ViewData["Noti"] = GetNoti();
			return View();
		}

		[Route("dashboard/notifs")]
		[HttpGet]
		public IActionResult Notifs(int id)
		{
			var notif = _context.Notifications.GetById(id);
			notif.IsRead = true;
			return RedirectToAction(nameof(Notifs));
		}

		[Route("dashboard/profile")]
		[HttpGet]
		public IActionResult Profile()
		{
			var student = _context.Students.GetByMatric(CurrentUser.UserName);
			ViewBag.Departments = _context.Departments.GetAll();
			ViewData["student"] = student;
			ViewData["Noti"] = GetNoti();
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> ProfileUpdate(Student model)
		{
			try
			{
				if (model.File != null)
				{
					_file.DeleteFile(model.ImageUrl);
					model.ImageUrl = _file.UploadFile(model.File);
				}

				User user = await _userManager.FindByIdAsync(CurrentUser.Id.ToString());
				user.FullName = model.FullName;
				user.Email = model.Email;
				user.PhoneNumber = model.PhoneNumber;
				if (model.ImageUrl != null)
				{
					user.ImageUrl = model.ImageUrl;
				}
				else
				{
					model.ImageUrl = user.ImageUrl;
				}
				await _userManager.UpdateAsync(user);

				var student = _context.Students.GetByMatric(model.MatricNo);
				var studentEntity = _mapper.Map(model, student);
				_context.Students.Update(studentEntity);
				await _context.SaveAsync();
				return RedirectToAction(nameof(Index));
			}
			catch (Exception)
			{
				TempData["error"] = "One or more errors occured.";
				return View(nameof(Profile));
			}
		}
	}
}