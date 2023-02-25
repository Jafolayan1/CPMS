using Domain.Entities;
using Domain.Interfaces;

using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Razor.Internal;

namespace SPMS.Helpers
{
	public abstract class BaseViewPage<TModel> : RazorPage<TModel>
	{
		[RazorInject]
		public IUserAccessor _userAccessor { get; set; }

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
	}
}