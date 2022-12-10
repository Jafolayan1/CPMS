using CPMS.Helpers;

using Domain.Entities;
using Domain.Interfaces;

using Microsoft.AspNetCore.Mvc;

namespace CPMS.Areas.Graduate.Controllers
{
	[CustomAuthorize(Role = "Student")]
	[Area("Graduate")]
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
			var stud = _context.Students.GetByMatric(CurrentUser.UserName);
			return _context.Notifications.Find(x => x.SupervisorId.Equals(stud.SupervisorId), false).ToList();
		}

		internal async void sendMail(string body, string toMail)
		{
			var email = new MailRequest()
			{
				ToEmail = toMail,
				Subject = "Projct Submission",
				Body = body
			};
			await _mail.SendEmailAsync(email, email.Body);
		}

		internal static int GenerateToken(int n)
		{
			Random rnd = new();
			return rnd.Next(n);
		}
	}
}