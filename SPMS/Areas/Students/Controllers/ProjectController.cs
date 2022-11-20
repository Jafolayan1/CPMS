using AutoMapper;

using CPMS.Hubs;

using Domain.Entities;
using Domain.Interfaces;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace CPMS.Areas.students.Controllers
{
    public class ProjectController : BaseController
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly IFileHelper _file;
        private readonly IHubContext<MessageHub> _messgaeHub;

        public ProjectController(IUserAccessor userAccessor, IUnitOfWork context, IMapper mapper, UserManager<User> userManager, IFileHelper file, IHubContext<MessageHub> messgaeHub, IMailService mail) : base(userAccessor, context, mail)
        {
            _mapper = mapper;
            _userManager = userManager;
            _file = file;
            _messgaeHub = messgaeHub;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var lstProposal = _context.Projects.Find(x => x.Matric.Equals(CurrentUser.UserName), false);
            ViewData["projectProposal"] = lstProposal;
            ViewData["Noti"] = GetNoti();
            return View();
        }

        [HttpGet]
        public IActionResult Details()
        {
            var prjt = _context.Projects.GetByMatric(CurrentUser.UserName);
            ViewData["project"] = prjt;
            ViewData["Noti"] = GetNoti();
            return View();
        }

        [HttpGet]
        public IActionResult Milestone()
        {
            var lstPrjts = _context.Projects.Find(x => x.Matric.Equals(CurrentUser.UserName), false);
            var lstChapts = _context.Chapters.Find(x => x.Matric.Equals(CurrentUser.UserName), false);
            ViewData["projects"] = lstPrjts;
            ViewData["chapters"] = lstChapts;
            ViewData["Noti"] = GetNoti();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddEditProject(Project model)
        {
            try
            {
                var stu = _context.Students.GetById(CurrentUser.UserName);
                model.StudentId = stu.StudentId;
                model.SupervisorId = stu.SupervisorId;

                if (model.File != null)
                {
                    _file.DeleteFile(model.FileUrl);
                    model.FileUrl = _file.UploadFile(model.File);
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
                    sendMail($"<p>You have a new file sbmited by {stu.FullName} with Matric No :{stu.MatricNo}</p>", stu.Supervisor.Email);
                }

                await _context.SaveAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                TempData["Error"] = "One or more errors occured, Failed to add";
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddEditChapter(Chapter model)
        {
            try
            {
                var prjtId = _context.Projects.GetByMatric(CurrentUser.UserName);

                if (model.File != null)
                {
                    _file.DeleteFile(model.FileUrl);
                    model.FileUrl = _file.UploadFile(model.File);
                }

                if (model.ChapterId > 0)
                {
                    var chap = _context.Chapters.GetById(model.ChapterId);
                    var chapEntity = _mapper.Map(model, chap);
                    _context.Chapters.Update(chapEntity);
                }
                else
                {
                    model.ProjectId = prjtId.ProjectId;
                    var chaEntity = _mapper.Map<Chapter>(model);
                    _context.Chapters.Add(chaEntity);
                    sendMail($"<p>You have a new file sbmited by {prjtId.Student.FullName} with Matric No :{prjtId.Student.MatricNo}</p>", prjtId.Supervisor.Email);

                }
                await _context.SaveAsync();
                return RedirectToAction(nameof(Milestone));
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<IActionResult> Delete(int id)
        {
            var prjt = _context.Projects.GetById(id);
            var chap = _context.Chapters.GetById(id);

            if (prjt != null)
            {
                _context.Projects.Remove(prjt);
            }
            else
            {
                _context.Chapters.Remove(null);
            }
            await _context.SaveAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}