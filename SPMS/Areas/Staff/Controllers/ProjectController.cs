﻿using AspNetCoreHero.ToastNotification.Abstractions;

using AutoMapper;

using Domain.Interfaces;

using LovePdf.Core;
using LovePdf.Model.Task;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

using Service.Configuration;

using SPMS.Hubs;

namespace SPMS.Areas.Staff.Controllers
{
	public class ProjectController : BaseController
	{
		private readonly IMapper _mapper;
		private readonly INotyfService _notyf;
		private readonly IHubContext<ChatHub> _hubContext;
		private readonly ILovePdfSettings _pdf;
		private readonly IWebHostEnvironment _env;
		private readonly IFileHelper _file;
		private IMemoryCache _cache;

		public ProjectController(IUserAccessor userAccessor, IUnitOfWork context, IMapper mapper, IMailService mail, INotyfService notyf, IHubContext<ChatHub> hubContext, IWebHostEnvironment env, IOptions<ILovePdfSettings> pdf, IFileHelper file, IMemoryCache cache) : base(userAccessor, context, mail)
		{
			_mapper = mapper;
			_notyf = notyf;
			_hubContext = hubContext;
			_env = env;
			_pdf = pdf.Value;
			_file = file;
			_cache = cache;
		}

		[HttpGet]
		[Route("pstudent")]
		public IActionResult PStudent()
		{
			var currentUser = CurrentUser.UserName;
			var supervisor = _context.Supervisors.GetByFileNo(currentUser);
			ViewData["supervisor"] = supervisor;
			ViewData["Noti"] = GetNoti();

			return View();
		}

		//[Route("proposal")]
		[HttpGet]
		public IActionResult Proposal()
		{
			var fileNo = CurrentUser.UserName;
			var supervisor = _context.Supervisors.GetByFileNo(fileNo);
			var lstProposal = _context.Projects.Find(x => x.SupervisorId.Equals(supervisor.SupervisorId), false).Where(s => s.Status.Equals("Pending"));
			ViewData["projectProposal"] = lstProposal;
			ViewData["Noti"] = GetNoti();
			return View();
		}

		[HttpGet]
		[Route("milestone")]
		public IActionResult Milestone()
		{
			var fileNo = CurrentUser.UserName;
			var supervisor = _context.Supervisors.GetByFileNo(fileNo);
			var lstChapters = _context.Chapters.Find(x => x.SupervisorId.Equals(supervisor.SupervisorId), false).Where(s => s.Status.Equals("Pending"));
			ViewData["chapters"] = lstChapters;
			ViewData["Noti"] = GetNoti();

			return View();
		}

		[HttpGet]
		[Route("projectarchive")]
		public IActionResult ProjectArchive()
		{
			var lstProjects = _context.ProjectArchive.GetAll().ToList();
			ViewData["projectsarchive"] = lstProjects;
			ViewData["Noti"] = GetNoti();
			return View(lstProjects);
		}

		[HttpGet]
		[Route("details")]
		public IActionResult Details(int projectId)
		{
			var project = _context.Projects.GetById(projectId);
			if (project is null)
			{
				return NotFound();
			}

			string cacheKey = $"PdfFile-{projectId}";
			if (_cache.TryGetValue<string>(cacheKey, out string pdfFileName))
			{
				ViewBag.fileName = pdfFileName;
			}
			else
			{
				try
				{
					var fileName = project.FileUrl;
					string outputDirectory = Path.Combine(_env.WebRootPath, "output");
					string outputFile = Path.Combine(outputDirectory, Path.GetFileNameWithoutExtension(fileName) + ".pdf");

					if (_file.FileExist(outputFile))
					{
						ViewBag.fileName = Path.GetFileName(outputFile);
					}
					else
					{
						Directory.CreateDirectory(outputDirectory);
						var api = new LovePdfApi(_pdf.Key, _pdf.Secret);
						var task = api.CreateTask<OfficeToPdfTask>();
						task.AddFile($"{_env.WebRootPath}{fileName}");
						task.Process();
						task.DownloadFile(outputDirectory);

						ViewBag.fileName = Path.GetFileName(outputFile); ;
					}
				}
				catch (Exception ex)
				{

					TempData["Msg"] = ex.Message;
				}
			}
			ViewData["project"] = project;
			ViewData["Noti"] = GetNoti();
			return View();

		}





		[HttpGet]
		[Route("cdetails")]
		public IActionResult CDetails(int chapterId)
		{
			var chapter = _context.Chapters.GetById(chapterId);
			if (chapter is null)
			{
				return NotFound();
			}
			string cacheKey = $"PdfFile-{chapterId}";
			if (_cache.TryGetValue<string>(cacheKey, out string pdfFileName))
			{
				ViewBag.fileName = pdfFileName;
			}
			else
			{
				try
				{
					var fileName = chapter.FileUrl;
					string outputDirectory = Path.Combine(_env.WebRootPath, "output");
					string outputFile = Path.Combine(outputDirectory, Path.GetFileNameWithoutExtension(fileName) + ".pdf");

					if (_file.FileExist(outputFile))
					{
						ViewBag.fileName = Path.GetFileName(outputFile);
					}
					else
					{
						Directory.CreateDirectory(outputDirectory);
						var api = new LovePdfApi(_pdf.Key, _pdf.Secret);
						var task = api.CreateTask<OfficeToPdfTask>();
						task.AddFile($"{_env.WebRootPath}{fileName}");
						task.Process();
						task.DownloadFile(outputDirectory);

						ViewBag.fileName = Path.GetFileName(outputFile); ;
					}
				}
				catch (Exception ex)
				{

					TempData["Msg"] = ex.Message;
				}
			}

			ViewData["chapter"] = chapter;
			ViewData["Noti"] = GetNoti();
			return View();
		}

		[HttpPost]
		public IActionResult Remark(IFormCollection data, int projectId, string remark, string status)
		{
			try
			{
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
				TempData["Msg"] = ex.Message;
				return RedirectToAction(nameof(Proposal));
			}
		}

		[HttpPost]
		public IActionResult CRemark(IFormCollection data, int chapterId, string remark, string status)
		{
			try
			{
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
			catch (Exception)
			{
				TempData["Msg"] = "One or more errors occured, unable to update.";
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
			catch (Exception)
			{
				TempData["Msg"] = "One or more errors occured, unable to update.";
				return RedirectToAction(nameof(Proposal));
			}
		}

		public IActionResult CStatus(string status, int chapterId)
		{
			try
			{
				var chapter = _context.Chapters.GetById(chapterId);
				chapter.Status = status;
				_context.Chapters.Update(chapter);
				_context.SaveChanges();
				foreach (var item in chapter.Project.Students)
				{
					SendMail($"<p> Hello , {item.FullName.Split(' ')[0]}. <br> You have a new notification on the file you submitted</p>", item.Email);
				}
				return RedirectToAction(nameof(Milestone));
			}
			catch (Exception) { TempData["Msg"] = "One or more errors occured, unable to update."; return RedirectToAction(nameof(Milestone)); }
		}
	}

}