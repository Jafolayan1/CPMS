using Domain.Entities;
using Domain.Interfaces;

using Microsoft.AspNetCore.Mvc;

namespace CPMS.Areas.su.Controllers
{
    public class NotificationController : BaseController
    {
        public NotificationController(IUserAccessor userAccessor, IUnitOfWork context, IMailService mail) : base(userAccessor, context, mail)
        {
        }

        [Route("su/notify")]
        [HttpGet]
        public IActionResult Notify()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Notify(Notification model)
        {
            var sup = _context.Supervisors.Find(x => x.EmployeeNo.Equals(CurrentUser.UserName), false).FirstOrDefault();

            var newNotify = new Notification()
            {
                Content = model.Content,
                IsRead = false,
                SupervisorId = sup.SupervisorId,
            };
            _context.Notifications.Add(newNotify);
            await _context.SaveAsync();
            return View();
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