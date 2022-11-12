using Domain.Entities;
using Domain.Interfaces;

using Microsoft.AspNetCore.Mvc;

namespace CPMS.Areas.Admin.Controllers
{
    public class DashboardController : BaseController
    {

        public DashboardController(IUserAccessor userAccessor, IUnitOfWork context) : base(userAccessor, context)
        {
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