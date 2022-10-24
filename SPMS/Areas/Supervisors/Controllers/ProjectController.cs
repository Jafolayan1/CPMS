using AspNetCoreHero.ToastNotification.Abstractions;

using AutoMapper;

using Domain.Interfaces;

using Microsoft.AspNetCore.Mvc;

namespace CPMS.Areas.Supervisors.Controllers
{
	public class ProjectController : BaseController
	{
		private readonly IUnitOfWork _context;
		private readonly IMapper _mapper;
		private readonly INotyfService _notyf;

		public ProjectController(IUserAccessor userAccessor, IUnitOfWork context, IMapper mapper, INotyfService notyf) : base(userAccessor)
		{
			_context = context;
			_mapper = mapper;
			_notyf = notyf;
		}

		[HttpGet]
		public IActionResult PStudent()
		{
			var supervisor = _context.Supervisors.GetById(CurrentUser.UserName);
			ViewData["supervisor"] = supervisor;
			return View();
		}

		[HttpGet]
		public IActionResult Proposal()
		{
			var supervisor = _context.Supervisors.GetById(CurrentUser.UserName);
			var lstProposal = _context.Projects.Find(x => x.SupervisorId.Equals(supervisor.SupervisorId), false).Where(s => s.Status.Equals("Pending"));
			ViewData["projectProposal"] = lstProposal;
			return View();
		}

		[HttpGet]
		public async Task<IActionResult> Status(int ProjectId, string item)
		{
			var project = _context.Projects.GetById(ProjectId);
			if (project != null)
			{
				project.Status = item;
				_context.Projects.Update(project);
				await _context.SaveAsync();
				return RedirectToAction(nameof(Proposal));
			}

			_notyf.Error("Error");
			return RedirectToAction(nameof(Proposal));
		}

	}
}
