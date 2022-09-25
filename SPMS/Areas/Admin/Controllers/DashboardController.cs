using CPMS.Helpers;

using Microsoft.AspNetCore.Mvc;

namespace CPMS.Areas.Admin.Controllers
{
    [CustomAuthorize(Roles = "Admin")]
    [Area("Admin")]
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
