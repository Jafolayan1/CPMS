using AspNetCoreHero.ToastNotification.Abstractions;

using AutoMapper;

using CPMS.Hubs;

using Domain.Interfaces;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.CodeAnalysis;

namespace CPMS.Areas.Staff.Controllers
{
	public class ProjectController : BaseController
	{
		private readonly IMapper _mapper;
		private readonly INotyfService _notyf;
		private readonly IHubContext<ChatHub> _hubContext;

		public ProjectController(IUserAccessor userAccessor, IUnitOfWork context, IMapper mapper, IMailService mail, INotyfService notyf, IHubContext<ChatHub> hubContext) : base(userAccessor, context, mail)
		{
			_mapper = mapper;
			_notyf = notyf;
			_hubContext = hubContext;
		}

		[Route("pstudent")]
		[HttpGet]
		public IActionResult PStudent()
		{
			var supervisor = _context.Supervisors.GetByFileNo(CurrentUser.UserName);
			ViewData["supervisor"] = supervisor;
			return View();
		}

		[Route("proposal")]
		[HttpGet]
		public IActionResult Proposal()
		{
			var supervisor = _context.Supervisors.GetByFileNo(CurrentUser.UserName);
			var lstProposal = _context.Projects.Find(x => x.SupervisorId.Equals(supervisor.SupervisorId), false).Where(s => s.Status.Equals("Pending"));
			ViewData["projectProposal"] = lstProposal;

			return View();
		}

		[Route("milestone")]
		[HttpGet]
		public IActionResult Milestone()
		{
			var supervisor = _context.Supervisors.GetByFileNo(CurrentUser.UserName);
			var lstChapters = _context.Chapters.Find(x => x.SupervisorId.Equals(supervisor.SupervisorId), false).Where(s => s.Status.Equals("Pending"));
			ViewData["chapters"] = lstChapters;

			return View();
		}

		[Route("projectarchive")]
		[HttpGet]
		public IActionResult ProjectArchive()
		{
			var lstProjects = _context.ProjectArchive.GetAll();
			return View(lstProjects);
		}

		[Route("details")]
		[HttpGet]
		public IActionResult Details(int projectId)
		{
			var project = _context.Projects.GetById(projectId);
			ViewData["project"] = project;
			ViewData["Noti"] = GetNoti();
			return View();
		}

		[Route("cdetails")]
		[HttpGet]
		public IActionResult CDetails(int chapterId)
		{
			var chapter = _context.Chapters.GetById(chapterId);
			ViewData["chapter"] = chapter;
			ViewData["Noti"] = GetNoti();
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Remark(IFormCollection data, int projectId, string? item)
		{
			try
			{
				string? remark = data["Remark"].ToString();
				string? status = data["Stat"].ToString();

				var prjt = _context.Projects.GetById(projectId);
				prjt.Status = status;
				prjt.Remark = remark;
				_context.Projects.Update(prjt);
				await _context.SaveAsync();
				foreach (var i in prjt.Student)
				{
					SendMail($"<p> Hello , {i.FullName.Split(' ')[0]}. <br> You have a new notification on the file you submitted</p>", i.Email);
				}
				return RedirectToAction(nameof(Proposal));
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error: {ex}");
				return RedirectToAction(nameof(Proposal));
			}
		}

		[HttpPost]
		public async Task<IActionResult> CRemark(IFormCollection data, int chapterId, string? item)
		{
			try
			{
				string? remark = data["Remark"].ToString();
				string? status = data["Stat"].ToString();

				var prjt = _context.Chapters.GetById(chapterId);
				prjt.Status = status;
				prjt.Remark = remark;
				_context.Chapters.Update(prjt);
				await _context.SaveAsync();
				foreach (var i in prjt.Project.Student)
				{
					SendMail($"<p> Hello , {i.FullName.Split(' ')[0]}. <br> You have a new notification on the file you submitted</p>", i.Email);
				}

				return RedirectToAction(nameof(Milestone));
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error: {ex}");
				return RedirectToAction(nameof(Milestone));
			}
		}

		public async Task<IActionResult> Status(string status, int projectId)
		{
			try
			{
				var project = _context.Projects.GetById(projectId);
				project.Status = status;
				_context.Projects.Update(project);
				await _context.SaveAsync();
				foreach (var item in project.Student)
				{
					SendMail($"<p> Hello , {item.FullName.Split(' ')[0]}. <br> You have a new notification on the file you submitted</p>", item.Email);
				}
				return RedirectToAction(nameof(Proposal));
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error: {ex}");
				return RedirectToAction(nameof(Proposal));
			}
		}

		public async Task<IActionResult> CStatus(string status, int projectId)
		{
			try
			{
				var project = _context.Projects.GetById(projectId);
				project.Status = status;
				_context.Projects.Update(project);
				await _context.SaveAsync();
				foreach (var item in project.Student)
				{
					SendMail($"<p> Hello , {item.FullName.Split(' ')[0]}. <br> You have a new notification on the file you submitted</p>", item.Email);
				}
				return RedirectToAction(nameof(Milestone));
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error: {ex}");
				return RedirectToAction(nameof(Milestone));
			}
		}
	}
}