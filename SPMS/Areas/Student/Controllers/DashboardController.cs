using CPMS.Helpers;

using Microsoft.AspNetCore.Mvc;


namespace CPMS.Areas.Student.Controllers
{
    [CustomAuthorize(Roles = "Student")]
    [Area("Student")]
    public class DashboardController : Controller
    {
        public DashboardController()
        {

        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
