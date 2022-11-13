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
        public IActionResult AddDepartment()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddDepartment(Department model)
        {
            var dpt = new Department()
            {
                Name = model.Name,
            };
            _context.Departments.Add(dpt);
            await _context.SaveAsync();
            return RedirectToAction(nameof(AddDepartment));
        }
    }
}