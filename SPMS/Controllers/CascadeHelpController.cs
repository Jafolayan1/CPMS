using Domain.Entities;
using Domain.Interfaces;

using Infrastructure;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;

namespace CPMS.Controllers
{
    public class CascadeHelpController : BaseController
    {
        private readonly ApplicationContext _context;
        private readonly IMailService _mail;

        public CascadeHelpController(IUserAccessor userAccessor, ApplicationContext context, IMailService mail) : base(userAccessor)
        {
            _context = context;
            _mail = mail;
        }

        public JsonResult getSupervisors(int Id)
        {
            List<Supervisor> list = new();
            list = _context.Supervisors.Where(x => x.DepartmentId.Equals(Id)).ToList();
            list.Insert(0, new Supervisor { SupervisorId = 0, Surname = " Please Select Supervisor" });
            return Json(new SelectList(list, "SupervisorId", "FullName"));
        }

        public JsonResult readFile(string file)
        {
            var read = System.IO.File.ReadAllText(file);
            return Json(read);
        }


        public JsonResult notify(int Id)
        {
            var data = _context.Notifications.FirstOrDefault(x => x.NotificationId.Equals(Id));
            data.IsRead = true;
            return Json(data);
        }

        public void changeStatus(int Id, string Item)
        {
            try
            {
                var project = _context.Projects.FirstOrDefault(x => x.ProjectId.Equals(Id));
                project.Status = Item;
                _context.Projects.Update(project);
                _context.SaveChanges();
                SendMail();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex}");
            }
        }

        private async void SendMail()
        {
            var stud = _context.Students.FirstOrDefault(x => x.MatricNo.Equals(CurrentUser.UserName));
            var email = new MailRequest()
            {
                ToEmail = stud.Email,
                Subject = "Projct Submission",
                Body = $" Hello , {stud.Surname}. <br> You have a new notification on the file you submitted"
            };
            await _mail.SendEmailAsync(email, email.Body);

        }
    }
}