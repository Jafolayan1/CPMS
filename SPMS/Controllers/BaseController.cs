using Domain.Entities;
using Domain.Interfaces;

using Microsoft.AspNetCore.Mvc;

namespace SPMS.Controllers
{
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
		protected static string _fullName;
		protected static string _username;
		protected static string _imageUrl;
		protected static string _dpt;
		protected static string _level;
		protected static string _cgpa;
		protected static string _phoneNo;
		protected static string _email;
		protected static string _role;
		public BaseController(IUserAccessor userAccessor)
		{
			_userAccessor = userAccessor;
		}
	}
}