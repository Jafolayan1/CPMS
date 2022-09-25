using CPMS.Helpers;

using Microsoft.AspNetCore.Mvc;

namespace CPMS.Areas.Supervisor.Controllers
{

    [CustomAuthorize(Roles = "Supervisor")]
    [Area("Supervisor")]
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
