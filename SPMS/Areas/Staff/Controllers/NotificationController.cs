using Domain.Entities;
using Domain.Interfaces;

using Infrastructure;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace SPMS.Areas.Staff.Controllers
{
    public class NotificationController : BaseController
    {
        private readonly ApplicationContext _applicationContext;
        public NotificationController(IUserAccessor userAccessor, IUnitOfWork context, IMailService mail, ApplicationContext applicationContext) : base(userAccessor, context, mail)
        {
            _applicationContext = applicationContext;
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

        [Route("notification/readnotification")]
        public IActionResult ReadNotification(int Id)
        {
            var noti = _context.Notifications.GetById(Id);
            noti.IsRead = true;
            _context.Notifications.Update(noti);
            _context.SaveChanges();
            return RedirectToAction(nameof(Notifications));
        }


        [HttpGet]
        public IActionResult MarkAsRead()
        {
            _applicationContext.Database.ExecuteSqlRaw("UPDATE [Notifications] SET [IsRead] = {0}", "True");
            return View(nameof(Notification));
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