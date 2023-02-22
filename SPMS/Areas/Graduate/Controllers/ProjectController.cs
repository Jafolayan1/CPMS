using AutoMapper;

using Domain.Entities;
using Domain.Interfaces;

using GroupDocs.Viewer.Options;
using GroupDocs.Viewer;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.CodeAnalysis;

using SPMS.Hubs;

using System.Diagnostics.CodeAnalysis;

using Project = Domain.Entities.Project;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.Build.Evaluation;

namespace SPMS.Areas.Graduate.Controllers
{
	public class ProjectController : BaseController
	{
		private readonly UserManager<User> _userManager;
		private readonly IMapper _mapper;
		private readonly IFileHelper _file;
		private readonly IHubContext<ChatHub> _messgaeHub;
		private readonly IWebHostEnvironment _env;
		private readonly INotyfService _notyf;


		public ProjectController(IUserAccessor userAccessor, IUnitOfWork context, IMapper mapper, UserManager<User> userManager, IFileHelper file, IHubContext<ChatHub> messgaeHub, IMailService mail, IWebHostEnvironment env, INotyfService notyf) : base(userAccessor, context, mail)
		{
			_mapper = mapper;
			_userManager = userManager;
			_file = file;
			_messgaeHub = messgaeHub;
			_env = env;
			_notyf = notyf;
		}

		[Route("project/index")]
		[HttpGet]
		public IActionResult Index()
		{
			var lstProposal = _context.Projects.Find(x => x.SupervisorId.Equals(CurrentStudent.SupervisorId), false);
			ViewBag.Students = _context.Students.GetAll();
			ViewData["projectProposal"] = lstProposal;
			ViewData["Noti"] = GetNoti();
			return View();
		}

		[Route("project/details")]
		[HttpGet]
		public IActionResult Details()
		{
			var matric = CurrentStudent.SupervisorId;
			var prjt = _context.Projects.GetByMatric(matric);
			if (prjt is not null)
			{
				var fileName = ManipulateFileUrl(prjt.FileUrl);
				string output = Path.Combine(_env.WebRootPath, "output");
				string outputFilePth = Path.Combine(output, fileName);
				using (var viewer = new Viewer(_env.WebRootPath + prjt.FileUrl))
				{
					var viewOptions = new PdfViewOptions(outputFilePth);
					viewer.View(viewOptions);
				}

				ViewBag.fileName = fileName;
			}
			ViewData["project"] = prjt;
			ViewData["Noti"] = GetNoti();
			return View();
		}

		[Route("project/milestone")]
		[HttpGet]
		public IActionResult Milestone()
		{
			var prjt = _context.Projects.GetAll().Where(x => x.Students.Any(s => s.MatricNo.Equals(CurrentStudent.MatricNo))).Where(st => st.Status == "Approved");
			var lstChapts = _context.Chapters.Find(x => x.SupervisorId.Equals(CurrentStudent.SupervisorId), false);
			ViewData["project"] = prjt;
			ViewData["chapters"] = lstChapts;
			ViewData["Noti"] = GetNoti();
			return View();
		}

		[Route("project/cproject")]
		[HttpGet]
		public IActionResult CompleteProject()
		{
			ViewBag.Departments = _context.Departments.GetAll();
			ViewBag.Students = _context.Students.GetAll();
			ViewBag.Supervisors = _context.Supervisors.GetAll();
			ViewData["Noti"] = GetNoti();
			return View();
		}

		[HttpPost]
		public IActionResult projectcomp(ProjectArchive model)
		{
			try
			{
				if (model.File != null)
				{
					_file.DeleteFile(model.FileUrl);
					model.FileUrl = _file.UploadFile(model.File);
					if (model.FileUrl is null)
					{
						TempData["Error"] = "Bad file, Chcek file data, rename and try again.";
						return RedirectToAction(nameof(CompleteProject));
					}
				}

				_context.ProjectArchive.Add(model);
				_context.SaveChanges();
				return RedirectToAction(nameof(CompleteProject));
			}
			catch (Exception)
			{
				return RedirectToAction(nameof(CompleteProject));
			}
		}

		[Route("project/archive")]
		[HttpGet]
		public IActionResult ProjectArchive()
		{
			var lstProjects = _context.ProjectArchive.GetAll();
			return View(lstProjects);
		}

		[HttpPost]
		public IActionResult AddEditProject(Project model, IFormCollection data)
		{
			var supervisorEmail = CurrentStudent.Supervisor.Email;
			var supervisorId = CurrentStudent.SupervisorId;
			try
			{
				var id = data["Student"].Select(int.Parse).ToList();
				var stud = new List<Student> { };
				foreach (var item in id)
				{
					var student = _context.Students.GetById(item);
					stud.Add(student);
				}
				model.Students = stud;
				model.SupervisorId = supervisorId;


				_file.DeleteFile(model.FileUrl);
				model.FileUrl = _file.UploadFile(model.File);
				if (model.FileUrl is null)
				{
					_notyf.Error("Damaged File, rename and try again.");
					return RedirectToAction(nameof(Index));
				}

				if (model.ProjectId > 0)
				{
					var p = _context.Projects.GetById(model.ProjectId);
					var pEntity = _mapper.Map(model, p);
					_context.Projects.Update(pEntity);
				}
				else
				{
					var projectEntity = _mapper.Map<Project>(model);
					_context.Projects.Add(projectEntity);
					foreach (var item in stud)
					{
						_name += $"{item.FullName} : ";
						_name += $"{item.MatricNo}, ";
					}
					SendMail($"<p>You have a new file (PROPOSAL) submited by {_name}</p>", supervisorEmail);
				}

				_context.SaveChanges();
				return RedirectToAction(nameof(Index));
			}
			catch (Exception)
			{
				TempData["Error"] = "One or more errors occured, Failed to add";
				return RedirectToAction(nameof(Index));
			}
		}

		[HttpPost]
		public IActionResult AddEditChapter(Chapter model, [MaybeNull] string name)
		{
			try
			{
				model.FileUrl = _file.UploadFile(model.File);
				if (model.FileUrl is null)
				{
					_notyf.Error("Bad file, Chcek file data, rename and try again.");
					return RedirectToAction(nameof(Milestone));
				}

				if (model.ChapterId > 0)
				{
					var chap = _context.Chapters.GetById(model.ChapterId);
					var chapEntity = _mapper.Map(model, chap);
					_context.Chapters.Update(chapEntity);
				}
				else
				{
					var chaEntity = _mapper.Map<Chapter>(model);
					_context.Chapters.Add(chaEntity);
					//var chap = _context.Projects.GetAll().Where(x => x.SupervisorId.Equals(CurrentStudent.SupervisorId));

					//foreach (var item in chap))
					//{
					//	_name += $"{item.FullName} : ";
					//	_name += $"{item.MatricNo}, ";
					//sendMail($"<p>You have a new file{model.ChapterName} submited by {_name}</p>", CurrentStudent.Supervisor.Email);
					//}
				}
				_context.SaveChanges();
				return RedirectToAction(nameof(Milestone));
			}
			catch (Exception)
			{
				TempData["Error"] = "One or more errors occured, Failed to add";
				return RedirectToAction(nameof(Milestone));
			}
		}

		[Route("project/delete")]
		public IActionResult Delete(int id)
		{
			var chap = _context.Chapters.GetById(id);
			var proposal = _context.Projects.GetById(id);
			if (chap != null)
				_context.Chapters.Remove(chap);
			else if (proposal != null)
				_context.Projects.Remove(proposal);

			_context.SaveChanges();
			return RedirectToAction(nameof(Index));
		}
	}
}