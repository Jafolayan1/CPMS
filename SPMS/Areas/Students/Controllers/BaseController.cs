using CPMS.Helpers;

using Domain.Entities;
using Domain.Interfaces;

using Microsoft.AspNetCore.Mvc;

namespace CPMS.Areas.Students.Controllers
{
    [CustomAuthorize(Role = "Student")]
    [Area("Students")]
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
            var stud = _context.Students.GetById(CurrentUser.UserName);
            return _context.Notifications.Find(x => x.SupervisorId.Equals(stud.SupervisorId), false).ToList();
        }

        public async void sendMail()
        {
            var stud = _context.Students.GetById(CurrentUser.UserName);
            var email = new MailRequest()
            {
                ToEmail = stud.Supervisor.Email,
                Subject = "Projct Submission",
                Body = $"<p>You have a new file sbmited by {stud.FullName} with Matric No :{stud.MatricNo}</p>"
            };
            await _mail.SendEmailAsync(email, email.Body);
        }
    }
}