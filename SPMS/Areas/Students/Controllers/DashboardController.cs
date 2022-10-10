using Domain.Interfaces;

using Microsoft.AspNetCore.Mvc;

namespace CPMS.Areas.Students.Controllers
{

    public class DashboardController : BaseController
    {
        public DashboardController(IUserAccessor userAccessor) : base(userAccessor)
        {
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
