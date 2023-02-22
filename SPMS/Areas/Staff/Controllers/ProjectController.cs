using AspNetCoreHero.ToastNotification.Abstractions;

using AutoMapper;

using Domain.Interfaces;

using GroupDocs.Viewer.Options;
using GroupDocs.Viewer;

using Spire.Doc;
using Spire.Doc.Documents;


using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.CodeAnalysis;

using SPMS.Hubs;

namespace SPMS.Areas.Staff.Controllers
{
	public class ProjectController : BaseController
	{
		private readonly IMapper _mapper;
		private readonly INotyfService _notyf;
		private readonly IHubContext<ChatHub> _hubContext;
		private readonly IWebHostEnvironment _env;
		public ProjectController(IUserAccessor userAccessor, IUnitOfWork context, IMapper mapper, IMailService mail, INotyfService notyf, IHubContext<ChatHub> hubContext, IWebHostEnvironment env) : base(userAccessor, context, mail)
		{
			_mapper = mapper;
			_notyf = notyf;
			_hubContext = hubContext;
			_env = env;
		}

	[Route("pstudent")]
		[HttpGet]
		public IActionResult PStudent()
		{
			var currentUser = CurrentUser.UserName;
			var supervisor = _context.Supervisors.GetByFileNo(currentUser);
			ViewData["supervisor"] = supervisor;
			return View();
		}

		[Route("proposal")]
		[HttpGet]
		public IActionResult Proposal()
		{
			var fileNo = CurrentUser.UserName;
			var supervisor = _context.Supervisors.GetByFileNo(fileNo);
			var lstProposal = _context.Projects.Find(x => x.SupervisorId.Equals(supervisor.SupervisorId), false).Where(s => s.Status.Equals("Pending"));
			ViewData["projectProposal"] = lstProposal;

			return View();
		}

		[Route("milestone")]
		[HttpGet]
		public IActionResult Milestone()
		{
			var fileNo = CurrentUser.UserName;
			var supervisor = _context.Supervisors.GetByFileNo(fileNo);
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
			if (project is not null)
			{
				var fileName = ManipulateFileUrl(project.FileUrl);
				string output = Path.Combine(_env.WebRootPath, "output");
				string outputFilePth = Path.Combine(output, fileName);
				using (var viewer = new Viewer(_env.WebRootPath + project.FileUrl))
				{
					var viewOptions = new PdfViewOptions(outputFilePth);
					viewer.View(viewOptions);
				}

				ViewBag.fileName = fileName;
			}
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
		public IActionResult Remark(IFormCollection data, int projectId, string? item)
		{
			try
			{
				string? remark = data["Remark"].ToString();
				string? status = data["Stat"].ToString();

				var prjt = _context.Projects.GetById(projectId);
				prjt.Status = status;
				prjt.Remark = remark;
				_context.Projects.Update(prjt);
				_context.SaveChanges();
				foreach (var i in prjt.Students)
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
		public IActionResult CRemark(IFormCollection data, int chapterId, string? item)
		{
			try
			{
				string? remark = data["Remark"].ToString();
				string? status = data["Stat"].ToString();

				var prjt = _context.Chapters.GetById(chapterId);
				prjt.Status = status;
				prjt.Remark = remark;
				_context.Chapters.Update(prjt);
				_context.SaveChanges();
				foreach (var i in prjt.Project.Students)
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

		public IActionResult Status(string status, int projectId)
		{
			try
			{
				var project = _context.Projects.GetById(projectId);
				project.Status = status;
				_context.Projects.Update(project);
				_context.SaveChanges();
				foreach (var item in project.Students)
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

		public IActionResult CStatus(string status, int projectId)
		{
			try
			{
				var project = _context.Projects.GetById(projectId);
				project.Status = status;
				_context.Projects.Update(project);
				_context.SaveChanges();
				foreach (var item in project.Students)
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