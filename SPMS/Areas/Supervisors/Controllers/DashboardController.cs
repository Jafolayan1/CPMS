using Domain.Interfaces;

using Microsoft.AspNetCore.Mvc;

namespace CPMS.Areas.Supervisors.Controllers
{
    public class DashboardController : BaseController
    {
        private readonly IUnitOfWork _context;

        public DashboardController(IUserAccessor userAccessor, IUnitOfWork context) : base(userAccessor)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }



    }
}