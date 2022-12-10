using Domain.Entities;
using Domain.Interfaces;

using Microsoft.AspNetCore.Mvc;

namespace CPMS.Areas.Staff.Controllers
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
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Notify(Notification model)
		{
			var sup = _context.Supervisors.Find(x => x.FileNo.Equals(CurrentUser.UserName), false).FirstOrDefault();

			var newNotify = new Notification()
			{
				Content = model.Content,
				IsRead = false,
				SupervisorId = sup.SupervisorId,
			};
			_context.Notifications.Add(newNotify);
			await _context.SaveAsync();
			return RedirectToAction("Noti");
		}

		[Route("notifications")]
		[HttpGet]
		public IActionResult Notifications()
		{
			var noti = _context.Notifications.GetAll();
			return View(noti);
		}

		[Route("notifications/{id}")]
		[HttpGet]
		public IActionResult Notifications(int id)
		{
			var noti = _context.Notifications.GetById(id);
			return View(noti);
		}
	}
}