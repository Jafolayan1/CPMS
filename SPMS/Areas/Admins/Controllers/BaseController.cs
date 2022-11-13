using CPMS.Helpers;

using Domain.Entities;
using Domain.Interfaces;

using Microsoft.AspNetCore.Mvc;

namespace CPMS.Areas.Admins.Controllers
{
    [CustomAuthorize(Role = "Admin")]

    [Area("Admins")]
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


        public BaseController(IUserAccessor userAccessor, IUnitOfWork context)
        {
            _userAccessor = userAccessor;
            _context = context;
        }
    }
}