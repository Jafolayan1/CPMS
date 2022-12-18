using Domain.Entities;
using Domain.Interfaces;

using Microsoft.AspNetCore.Mvc;

using SPMS.Helpers;

namespace SPMS.Areas.Admin.Controllers
{
	[CustomAuthorize(Role = "Admin")]
	[Area("Admin")]
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

		protected static List<Account> _accounts = new();

		public BaseController(IUserAccessor userAccessor, IUnitOfWork context)
		{
			_userAccessor = userAccessor;
			_context = context;
		}
	}
}