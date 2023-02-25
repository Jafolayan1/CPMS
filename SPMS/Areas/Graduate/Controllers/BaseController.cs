﻿using Domain.Entities;
using Domain.Interfaces;

using Microsoft.AspNetCore.Mvc;

using SPMS.Helpers;

namespace SPMS.Areas.Graduate.Controllers
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

		public Student CurrentStudent
		{
			get
			{
				if (User != null)
					return _userAccessor.GetStudent();
				else
					return null;
			}
		}

		private readonly IUserAccessor _userAccessor;
		protected IUnitOfWork _context;
		protected IMailService _mail;
		protected string _name;
		protected string _matric;
		protected string CUserName 
		{
			get {
				return CurrentUser.UserName;
			}
		}

		public BaseController(IUserAccessor userAccessor, IUnitOfWork context, IMailService mail)
		{
			_userAccessor = userAccessor;
			_context = context;
			_mail = mail;
		}

		public IEnumerable<Notification> GetNoti()
		{
			var currentUser = CurrentUser.UserName;
			var stud = _context.Students.GetByMatric(currentUser);
			return _context.Notifications.Find(x => x.SupervisorId.Equals(stud.SupervisorId), false).ToList();
		}

		public void AddNoti(Notification notification) 
		{
			//Add Notification
		}

		internal async void SendMail(string body, string toMail)
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