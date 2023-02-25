using Domain.Entities;
using Domain.Interfaces;

using Microsoft.AspNetCore.Mvc;

using SPMS.Helpers;

namespace SPMS.Areas.Staff.Controllers
{
	[CustomAuthorize(Role = "Supervisor")]
	[Area("Staff")]
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
			var currentUser = CurrentUser.UserName;
			var stud = _context.Supervisors.GetByFileNo(currentUser);
			return _context.Notifications.Find(x => x.SupervisorId.Equals(stud.SupervisorId), false).ToList();
		}

		public void AddNoti(Notification notification)
		{
			_context.Notifications.Add(notification);
			_context.SaveChanges();
		}
		public async void SendMail(string body, string toMail)
		{
			var email = new MailRequest()
			{
				ToEmail = toMail,
				Subject = "Projct Submission",
				Body = body
			};
			await _mail.SendEmailAsync(email, email.Body);
		}

		internal static Random _rnd = new();

		internal static string RandomString(int length)
		{
			const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
			return new string(Enumerable.Repeat(chars, length)
				.Select(s => s[_rnd.Next(s.Length)]).ToArray());
		}

		internal static string ManipulateFileUrl(string fileUrl)
		{
			var name = fileUrl;
			var Rname = name.Remove(0, 9);
			string[] strName = Rname.Split('.');
			var fileName = $"{strName[0]}.pdf";

			return fileName;
		}

	}
}