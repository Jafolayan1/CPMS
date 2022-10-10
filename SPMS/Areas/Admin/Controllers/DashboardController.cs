using CPMS.Helpers;

using Infrastructure;

using Microsoft.AspNetCore.Mvc;

namespace CPMS.Areas.Admin.Controllers
{
    [CustomAuthorize(Roles = "Admin")]
    [Area("Admins")]
    public class DashboardController : Controller
    {
        private readonly ApplicationContext _context;

        public DashboardController(ApplicationContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {

            return View();
        }
    }
}
