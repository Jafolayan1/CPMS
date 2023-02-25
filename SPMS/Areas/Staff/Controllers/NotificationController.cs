using Domain.Entities;
using Domain.Interfaces;

using Microsoft.AspNetCore.Mvc;

namespace SPMS.Areas.Staff.Controllers
{
	public class NotificationController : BaseController
	{
		public NotificationController(IUserAccessor userAccessor, IUnitOfWork context, IMailService mail) : base(userAccessor, context, mail)
		{
		}

		[Route("noti")]
		[HttpGet]
		public IActionResult Noti()
		{
			ViewData["Noti"] = GetNoti();
			return View();
		}

		[HttpPost]
		public IActionResult Notify(Notification model)
		{
			var sup = _context.Supervisors.Find(x => x.FileNo.Equals(CurrentUser.UserName), false).FirstOrDefault();

			var newNotify = new Notification()
			{
				Content = model.Content,
				IsRead = false,
				SupervisorId = sup.SupervisorId,
			};
			_context.Notifications.Add(newNotify);
			_context.SaveChanges();
			return RedirectToAction("Noti");
		}

		[Route("notifications")]
		[HttpGet]
		public IActionResult Notifications()
		{
			var noti = _context.Notifications.GetAll();
			ViewData["Noti"] = GetNoti();

			return View(noti);
		}

		[Route("notifications/{id}")]
		[HttpGet]
		public IActionResult Notifications(int id)
		{
			var noti = _context.Notifications.GetById(id);
			ViewData["Noti"] = GetNoti();

			return View(noti);
		}
	}
}