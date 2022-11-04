using AutoMapper;

using Domain.Entities;
using Domain.Interfaces;

using Infrastructure;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using Notification = Domain.Entities.Notification;

namespace CPMS.Areas.Supervisors.Controllers
{
    public class NotificationController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _context;


        public NotificationController(IUserAccessor userAccessor, IMapper mapper, IUnitOfWork context, UserManager<User> userManager, ApplicationContext repo) : base(userAccessor)
        {
            _mapper = mapper;
            _context = context;
        }

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
    }
}
