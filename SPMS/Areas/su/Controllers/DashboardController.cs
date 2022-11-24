using AutoMapper;

using Domain.Entities;
using Domain.Interfaces;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CPMS.Areas.su.Controllers
{
    public class DashboardController : BaseController
    {
        private readonly IFileHelper _file;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;

        public DashboardController(IUserAccessor userAccessor, IUnitOfWork context, IFileHelper file, IMailService mail, IMapper mapper, UserManager<User> userManager) : base(userAccessor, context, mail)
        {
            _file = file;
            _mapper = mapper;
            _userManager = userManager;
        }

        [Route("dashboard/index")]
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [Route("profile")]
        [HttpGet]
        public IActionResult Profile()
        {
            var supervisor = _context.Supervisors.GetById(CurrentUser.UserName);
            ViewBag.Departments = _context.Departments.GetAll();
            ViewData["supervisor"] = supervisor;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ProfileUpdate(Supervisor model)
        {
            try
            {
                if (model.File != null)
                {
                    _file.DeleteFile(model.ImageUrl);
                    model.ImageUrl = _file.UploadFile(model.File);
                }

                User user = await _userManager.FindByIdAsync(CurrentUser.Id.ToString());
                user.Surname = model.Surname;
                user.OtherNames = model.OtherNames;
                user.Email = model.Email;
                user.PhoneNumber = model.PhoneNumber;
                user.ImageUrl = model.ImageUrl;

                var supervisor = _context.Supervisors.GetById(model.SupervisorId);
                var supervisorEntity = _mapper.Map(model, supervisor);
                await _userManager.UpdateAsync(user);
                _context.Supervisors.Update(supervisorEntity);
                await _context.SaveAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                TempData["error"] = "One or more errors occured.";
                return View(nameof(Profile));
            }
        }
    }
}