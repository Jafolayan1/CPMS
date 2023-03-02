using Domain.Entities;
using Domain.Interfaces;

using Microsoft.AspNetCore.Identity;

namespace SPMS.Helpers
{
    public class UserAccessor : IUserAccessor
    {
        private readonly UserManager<User> _userManager;
        private IHttpContextAccessor _httpContextAccessor;
        private IUnitOfWork _context;

        public UserAccessor(UserManager<User> userManager, IHttpContextAccessor httpContextAccessor, IUnitOfWork context)
        {
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _context = context;
        }

        public User GetUser()
        {
            if (_httpContextAccessor.HttpContext.User != null)
                return _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User).Result;
            else
                return null;
        }

        public Student GetStudent()
        {
            if (_httpContextAccessor.HttpContext.User != null)
            {
                var usr = _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User).Result;
                return _context.Students.GetByMatric(usr.UserName);
            }
            else
                return null;
        }

        public Supervisor GetSupervisor()
        {
            if (_httpContextAccessor.HttpContext.User != null)
            {
                var usr = _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User).Result;
                return _context.Supervisors.GetByFileNo(usr.UserName);
            }
            else
                return null;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}