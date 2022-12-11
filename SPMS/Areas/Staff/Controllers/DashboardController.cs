using AutoMapper;

using Domain.Entities;
using Domain.Interfaces;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CPMS.Areas.Staff.Controllers
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

		[Route("dashboard")]
		[HttpGet]
		public IActionResult Dashboard()
		{
			return View();
		}

		[Route("account")]
		[HttpGet]
		public IActionResult Account()
		{
			var supervisor = _context.Supervisors.GetByFileNo(CurrentUser.UserName);
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
				user.FullName = model.FullName;
				user.Email = model.Email;
				user.PhoneNumber = model.PhoneNumber;
				if (model.ImageUrl != null)
				{
					user.ImageUrl = model.ImageUrl;
				}
				else
				{
					model.ImageUrl = user.ImageUrl;
				}
				await _userManager.UpdateAsync(user);

				var supervisor = _context.Supervisors.GetByFileNo(model.FileNo);
				var supervisorEntity = _mapper.Map(model, supervisor);
				_context.Supervisors.Update(supervisorEntity);
				await _context.SaveAsync();
				return RedirectToAction(nameof(Account));
			}
			catch (Exception)
			{
				TempData["Error"] = "One or more errors occured.";
				return View(nameof(Account));
			}
		}
	}
}