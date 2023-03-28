using AspNetCoreHero.ToastNotification.Abstractions;

using AutoMapper;

using Domain.Entities;
using Domain.Interfaces;

using Infrastructure;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace SPMS.Areas.Graduate.Controllers
{
	public class DashboardController : BaseController
	{
		private readonly IAuthenticationService _auth;
		private readonly UserManager<User> _userManager;
		private readonly IMapper _mapper;
		private readonly IFileHelper _file;
		private readonly ApplicationContext _repo;
		private readonly INotyfService _notyf;


		public DashboardController(IAuthenticationService auth, UserManager<User> userManager, IMailService mail, IUnitOfWork context, IMapper mapper, IUserAccessor o, IFileHelper file, ApplicationContext repo, INotyfService notyf) : base(o, context, mail)
		{
			_auth = auth;
			_userManager = userManager;
			_mapper = mapper;
			_file = file;
			_repo = repo;
			_notyf = notyf;
		}

		[Route("dashboard/index")]
		[HttpGet]
		public IActionResult Index()
		{

			ViewData["Noti"] = GetNoti();
			ViewData["projects"] = _context.Students.GetByMatric(CurrentUser.UserName);
			ViewData["chapters"] = _context.Chapters.GetAll();
			//ViewData["proposals"] = _context.Projects.GetAll().Where(x => x.SupervisorId == id);
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
			ViewBag.myDepartments = _context.Departments.GetDepartments().ToList();
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
					model.ImageUrl = await _file.UploadFile(model.File);
				}
				User user = await _userManager.FindByIdAsync(model.UserId.ToString());
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
				var change = _context.SaveChanges();
				if (change > 0)
				{
					return RedirectToAction(nameof(Index));

				}
				TempData["Msg"] = "One or more errors occured.";
				return View(nameof(Profile));
			}
			catch (Exception)
			{
				TempData["Msg"] = "One or more errors occured.";
				return View(nameof(Profile));
			}
		}

		[HttpGet]
		public IActionResult Complain()
		{
			return View();
		}


		[HttpPost]
		[Route("dashboard/complaint")]

		public IActionResult Complain(Complaint model)
		{
			_repo.Complaints.Add(model);
			_context.SaveChanges();
			return RedirectToAction(nameof(Index));
		}
	}
}