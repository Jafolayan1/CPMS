using AspNetCoreHero.ToastNotification.Abstractions;

using Domain.Interfaces;

using Microsoft.AspNetCore.Mvc;

namespace SPMS.Areas.Admin.Controllers
{
    public class DashboardController : BaseController
    {
        private readonly INotyfService _notyf;

        public DashboardController(IUserAccessor userAccessor, IUnitOfWork context, INotyfService notyf) : base(userAccessor, context)
        {
            _notyf = notyf;
        }

        [HttpGet]
        [Route("index")]
        public IActionResult Index()
        {
            return View();
        }
    }
}