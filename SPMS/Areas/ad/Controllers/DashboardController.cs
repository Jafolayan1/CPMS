using AspNetCoreHero.ToastNotification.Abstractions;

using Domain.Interfaces;

using Microsoft.AspNetCore.Mvc;

namespace CPMS.Areas.ad.Controllers
{
    public class DashboardController : BaseController
    {
        private readonly INotyfService _notyf;

        public DashboardController(IUserAccessor userAccessor, IUnitOfWork context, INotyfService notyf) : base(userAccessor, context)
        {
            _notyf = notyf;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }


        [Route("dashboard/profile")]
        public IActionResult Profile()
        {
            return View();
        }

    }
}