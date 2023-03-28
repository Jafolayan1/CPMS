using AspNetCoreHero.ToastNotification.Abstractions;

using AutoMapper;

using Domain.Entities;
using Domain.Interfaces;

using Htmx;

using Infrastructure;

using LovePdf.Core;
using LovePdf.Model.Task;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
//using Microsoft.CodeAnalysis;
using Microsoft.Extensions.Options;

using Service.Configuration;

using SPMS.Hubs;

using System.Diagnostics.CodeAnalysis;

using Project = Domain.Entities.Project;

namespace SPMS.Areas.Graduate.Controllers
{
    public class ProjectController : BaseController
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly IFileHelper _file;
        private readonly IHubContext<ChatHub> _messgaeHub;
        private readonly IWebHostEnvironment _env;
        private readonly ILovePdfSettings _pdf;
        private readonly INotyfService _notyf;
        private readonly ApplicationContext _applicationContext;




        public ProjectController(IUserAccessor userAccessor, IUnitOfWork context, IMapper mapper, UserManager<User> userManager, IFileHelper file, IHubContext<ChatHub> messgaeHub, IMailService mail, IWebHostEnvironment env, INotyfService notyf, IOptions<ILovePdfSettings> pdf, ApplicationContext applicationContext) : base(userAccessor, context, mail)
        {
            _mapper = mapper;
            _userManager = userManager;
            _file = file;
            _messgaeHub = messgaeHub;
            _env = env;
            _notyf = notyf;
            _pdf = pdf.Value;
            _applicationContext = applicationContext;
        }

        [Route("project/index")]
        [HttpGet]
        public IActionResult Index()
        {
            dynamic lstProposal = _context.Projects.Find(x => x.SupervisorId.Equals(CurrentStudent.SupervisorId), false);

            ViewBag.Students = _context.Students.GetAll();
            ViewData["projectProposal"] = lstProposal;
            ViewData["Noti"] = GetNoti();
            return View();
        }

        [Route("project/details")]
        [HttpGet]
        public IActionResult Details(int chapterId)
        {
            var matric = CurrentStudent.SupervisorId;
            var project = _context.Projects.GetByMatric(matric);
            var chapter = _context.Chapters.GetById(chapterId);
            string fileName;
            dynamic manipulateFile;
            bool fileExist;
            var output = Path.Combine(_env.WebRootPath, "output");
            bool exist = Directory.Exists(output);
            if (!exist)
                Directory.CreateDirectory(output);

            if (project is not null)
            {
                try
                {
                    fileName = project.FileUrl;
                    manipulateFile = ManipulateFileUrl(fileName);
                    fileExist = _file.FileExist(Path.Combine(output, manipulateFile));
                    if (fileExist)
                    {
                        ViewBag.fileName = manipulateFile;
                    }
                    else
                    {
                        var api = new LovePdfApi(_pdf.Key, _pdf.Secret);
                        var task = api.CreateTask<OfficeToPdfTask>();
                        task.AddFile($"{_env.WebRootPath}{fileName}");
                        task.Process();
                        task.DownloadFile(output);

                        string[] strname = fileName.Split('.');
                        var strfileName = $"{strname[0]}.pdf";
                        ViewBag.fileName = strfileName;
                    }
                }
                catch (Exception)
                {
                    TempData["Msg"] = "One or more errors occured, unable to complete request.";
                }
            }
            else if (chapter is not null)
            {
                fileName = chapter.FileUrl;
                manipulateFile = ManipulateFileUrl(fileName);
                fileExist = _file.FileExist(Path.Combine(output, manipulateFile));
                if (fileExist)
                {
                    ViewBag.fileName = manipulateFile;
                }

            }
            ViewData["project"] = project;
            ViewData["Noti"] = GetNoti();
            return View();
        }

        [Route("project/milestone")]
        [HttpGet]
        public IActionResult Milestone()
        {
            var matric = CurrentStudent.MatricNo;
            var prjt = _context.Projects.GetAll().Where(x => x.Students.Any(s => s.MatricNo.Equals(matric))).Where(st => st.Status == "Approved");
            if (prjt.Count() > 1)
            {
                TempData["Msg"] = "You have more than 1 approved project, Kindly select One(1)";
            }
            var lstChapts = _context.Chapters.Find(x => x.ProjectStudentId == CurrentStudent.StudentId.ToString(), false);
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
        public async Task<IActionResult> ProjectComp(ProjectArchive model, IFormCollection data)
        {
            try
            {
                var id = data["Students"].Select(int.Parse).ToList();
                var stud = new List<Student> { };
                foreach (var item in id)
                {
                    var student = _context.Students.GetById(item);
                    stud.Add(student);
                }
                model.Students = stud;
                model.FileUrl = await _file.Upload(model.File);

                if (model.FileUrl.Contains("null"))
                {
                    TempData["Msg"] = model.FileUrl;
                    return RedirectToAction(nameof(CompleteProject));
                }
                _context.ProjectArchive.Add(model);
                _context.SaveChanges();
                return RedirectToAction(nameof(CompleteProject));

            }
            catch (Exception)
            {
                TempData["Msg"] = "One or more erroes occured.";
                return RedirectToAction(nameof(CompleteProject));
            }
        }


        [Route("project/archive")]
        [HttpGet]
        public IActionResult ProjectArchive()
        {
            var lstProjects = _context.ProjectArchive.GetAll();
            ViewData["projectsarchive"] = lstProjects;
            ViewData["Noti"] = GetNoti();
            return View(lstProjects);
        }

        [HttpPost]
        public async Task<IActionResult> AddEditProject(Project model, IFormCollection data)
        {
            var supervisorEmail = CurrentStudent.Supervisor.Email;
            var supervisorId = CurrentStudent.SupervisorId;
            try
            {
                var id = data["Student"].Select(int.Parse).ToList();
                if (id.Count <= 0)
                {
                    id = data["Student1"].Select(int.Parse).ToList();
                }

                var stud = new List<Student> { };
                foreach (var item in id)
                {
                    var student = _context.Students.GetById(item);
                    stud.Add(student);
                }
                model.Students = stud;
                model.SupervisorId = supervisorId;

                model.FileUrl = await _file.UploadFile(model.File);
                if (model.FileUrl.Contains("null"))
                {
                    TempData["Msg"] = model.FileUrl;
                    return RedirectToAction(nameof(Index));
                }

                if (model.ProjectId > 0)
                {
                    var project = _context.Projects.GetById(model.ProjectId);
                    project.Topic = model.Topic;
                    project.Status = model.Status;
                    project.Students = model.Students;
                    project.FileUrl = model.FileUrl;

                    _context.Projects.Update(project);

                }
                else
                {

                    var projectEntity = _mapper.Map<Project>(model);
                    _context.Projects.Add(projectEntity);
                    foreach (var item in stud)
                    {
                        _name += $"{item.FullName} : ";
                        //_matric += $"{item.MatricNo}, ";
                    }
                    SendMail($"<p>You have a new file (PROPOSAL) submited by {_name}</p>", supervisorEmail);
                    var noti = new Domain.Entities.Notification()
                    {
                        Content = $"PROPOSAL submitted by {_name}",
                        IsRead = false,
                        SupervisorId = (int)supervisorId
                    };
                    AddNoti(noti);
                }

                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                TempData["Msg"] = "One or more errors occured, Failed to add";
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddEditChapter(Chapter model, [MaybeNull] string name)
        {
            var supervisorEmail = CurrentStudent.Supervisor.Email;
            var supervisorId = CurrentStudent.SupervisorId;
            try
            {
                model.FileUrl = await _file.UploadFile(model.File);
                if (model.FileUrl.Contains("null"))
                {
                    TempData["Msg"] = model.FileUrl;
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
                    var chap = _context.Projects.GetByMatric(supervisorId);
                    foreach (var item in chap.Students)
                    {
                        _name += $"{item.FullName} : ";
                        //_matric += $"{item.MatricNo}, ";
                        SendMail($"<p>You have a new file ({model.ChapterName}) submited by {_name}</p>", supervisorEmail);
                    }
                    var noti = new Domain.Entities.Notification()
                    {
                        Content = $"CHAPTER submitted by {_name}",
                        IsRead = false,
                        SupervisorId = (int)supervisorId
                    };
                    AddNoti(noti);
                }

                _context.SaveChanges();
                return RedirectToAction(nameof(Milestone));
            }
            catch (Exception)
            {
                TempData["Msg"] = "One or more errors occured, Failed to add";
                return RedirectToAction(nameof(Milestone));
            }
        }

        [HttpDelete]
        public void Delete(int id)
        {
            var chap = _context.Chapters.GetById(id);
            var proposal = _context.Projects.GetById(id);
            if (chap != null)
                _context.Chapters.Remove(chap);
            else if (proposal != null)
                _context.Projects.Remove(proposal);

            _context.SaveChanges();

            Response.Htmx(htmx =>
            {
                htmx.Refresh();
            });
        }
    }
}