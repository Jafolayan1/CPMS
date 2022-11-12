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



        public BaseController(IUserAccessor userAccessor, IUnitOfWork context)
        {
            _userAccessor = userAccessor;
            _context = context;
        }
        public IEnumerable<Notification> GetNoti()
        {
            var stud = _context.Students.GetById(CurrentUser.UserName);
            return _context.Notifications.Find(x => x.SupervisorId.Equals(stud.SupervisorId), false).ToList();
        }


    }
}