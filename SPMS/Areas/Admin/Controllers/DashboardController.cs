using Domain.Entities;
using Domain.Interfaces;

using Infrastructure;

using Microsoft.AspNetCore.Mvc;

namespace CPMS.Areas.Admin.Controllers
{
    public class DashboardController : BaseController
    {
        private readonly ApplicationContext _context;

        public DashboardController(IUserAccessor userAccessor, ApplicationContext context) : base(userAccessor)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AddDepartment(Department model)
        {
            return View();
        }
    }
}