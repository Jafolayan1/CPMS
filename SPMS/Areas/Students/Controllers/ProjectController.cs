using AutoMapper;

using Domain.Entities;
using Domain.Interfaces;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CPMS.Areas.Students.Controllers
{
    public class ProjectController : BaseController
    {
        private readonly IUnitOfWork _context;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly IFileHelper _file;


        public ProjectController(IUserAccessor userAccessor, IUnitOfWork context, IMapper mapper, UserManager<User> userManager, IFileHelper file) : base(userAccessor)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
            _file = file;
        }

        [HttpGet]
        public IActionResult ProjectTopic()
        {
            var topics = _context.Projects.Find(x => x.Matric.Equals(CurrentUser.UserName), false);
            return View();
        }

        [HttpPost]
        public IActionResult ProjectTopic(Project model)
        {
            if (ModelState.IsValid)
            {
                _context.Projects.Add(model);
                _context.SaveAsync();
            }
            return View();
        }

        [HttpPut]
        public IActionResult UpdateProjectTopic(string id)
        {
            var pTopic = _context.Projects.GetById(id);
            _context.Projects.Update(pTopic);
            _context.SaveAsync();

            return View();
        }

        [HttpDelete]
        public IActionResult DeleteProjectTopic(Project model)
        {
            _context.Projects.Update(model);
            _context.SaveAsync();
            return View();
        }

    }
}
