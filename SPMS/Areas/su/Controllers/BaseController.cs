using CPMS.Helpers;

using Domain.Entities;
using Domain.Interfaces;

using Microsoft.AspNetCore.Mvc;

namespace CPMS.Areas.su.Controllers
{
    [CustomAuthorize(Role = "Supervisor")]
    [Area("su")]
    public class BaseController : Controller
    {
        public User CurrentUser
        {
            get
            {
                if (User != null)
                    return _userAccessor.GetUser();
                else
                    return null;
            }
        }

        private readonly IUserAccessor _userAccessor;
        protected IUnitOfWork _context;
        protected IMailService _mail;

        public BaseController(IUserAccessor userAccessor, IUnitOfWork context, IMailService mail)
        {
            _userAccessor = userAccessor;
            _context = context;
            _mail = mail;
        }

        public IEnumerable<Notification> GetNoti()
        {
            var stud = _context.Supervisors.GetByFileNo(CurrentUser.UserName);
            return _context.Notifications.Find(x => x.SupervisorId.Equals(stud.SupervisorId), false).ToList();
        }

        public async void SendMail()
        {
            var stud = _context.Students.GetByMatric(CurrentUser.UserName);
            var email = new MailRequest()
            {
                ToEmail = stud.Email,
                Subject = "Projct Submission",
                Body = $" Hello , {stud.FullName.Split(' ')[0]}. <br> You have a new notification on the file you submitted"
            };
            await _mail.SendEmailAsync(email, email.Body);
        }
    }
}