﻿using CPMS.Helpers;

using Domain.Entities;
using Domain.Interfaces;

using Microsoft.AspNetCore.Mvc;

namespace CPMS.Areas.ad.Controllers
{
	[CustomAuthorize(Role = "Admin")]
	[Area("ad")]
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