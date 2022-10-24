using Domain.Interfaces;

using Microsoft.AspNetCore.Mvc;

namespace CPMS.Areas.Supervisors.Controllers
{
	public class ProjectController : BaseController
	{
		private readonly IUnitOfWork _context;

		public ProjectController(IUserAccessor userAccessor, IUnitOfWork context) : base(userAccessor)
		{
			_context = context;
		}

		[HttpGet]
		public IActionResult PStudent()
		{
			var supervisor = _context.Supervisors.GetById(CurrentUser.UserName);
			//var sup = _context.Projects.Find(x => x.StudentId.Equals(supervisor.ProjectStudents), false);
			ViewData["supervisor"] = supervisor;
			return View();
		}

		[HttpGet]
		public IActionResult Proposal()
		{
			var supervisor = _context.Supervisors.GetById(CurrentUser.UserName);
			var lstProposal = _context.Projects.Find(x => x.SupervisorId.Equals(supervisor.SupervisorId), false).Where(s => s.Status.Equals("Pending"));
			ViewData["projectProposal"] = lstProposal;

			foreach (var item in lstProposal)
			{
			}
			return View();
		}


	}
}
