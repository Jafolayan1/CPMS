using AspNetCoreHero.ToastNotification.Abstractions;

using Domain.Entities;
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

        public IActionResult Profile()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Department()
        {
            ViewData["Departments"] = _context.Departments.GetAll();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddDepartment(Department model)
        {
            var chkdpt = _context.Departments.Find(x => x.Name.Equals(model.Name), true);
            if (chkdpt != null)
            {
                TempData["error"] = "Department already exists";
                return RedirectToAction(nameof(Department));
            }
            var dpt = new Department()
            {
                Name = model.Name
            };
            _context.Departments.Add(dpt);
            await _context.SaveAsync();
            return RedirectToAction(nameof(Department));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Department model)
        {
            var dpt = _context.Departments.GetById(model.DepartmentId);
            dpt.DepartmentId = model.DepartmentId;
            dpt.Name = model.Name;
            _context.Departments.Update(dpt);
            await _context.SaveAsync();
            return RedirectToAction(nameof(Department));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var dpt = _context.Departments.GetById(id);
            _context.Departments.Remove(dpt);
            await _context.SaveAsync();
            return RedirectToAction(nameof(Department));
        }
    }
}