using CPMS.Helpers;

using Microsoft.AspNetCore.Mvc;

namespace CPMS.Areas.Supervisors.Controllers
{
    [CustomAuthorize(Roles = "Supervisor")]
    [Area("Supervisors")]
    public class DashboardController : Controller
    {
        public DashboardController()
        {
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}