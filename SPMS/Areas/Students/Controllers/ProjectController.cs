using AutoMapper;

using CPMS.Hubs;

using Domain.Entities;
using Domain.Interfaces;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace CPMS.Areas.Students.Controllers
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
            var prjt = _context.Projects.GetById(CurrentUser.UserName);
            ViewData["project"] = prjt;
            ViewData["Noti"] = GetNoti();
            return View();
        }

        [HttpGet]
        public IActionResult Milestone()
        {
            var lstProposal = _context.Projects.Find(x => x.Matric.Equals(CurrentUser.UserName), false).Where(c => c.Chapter != null && c.Status.Equals("Approved"));
            ViewData["projectProposal"] = lstProposal;
            ViewData["Noti"] = GetNoti();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddProject(Project model)
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
                    sendMail();
                }

                await _context.SaveAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                TempData["Error"] = "One or more errors occured, Failed to addd";
                return RedirectToAction(nameof(Index));
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            var pTopic = _context.Projects.GetById(id);
            _context.Projects.Remove(pTopic);
            await _context.SaveAsync();
            return RedirectToAction(nameof(Index));
        }


    }
}