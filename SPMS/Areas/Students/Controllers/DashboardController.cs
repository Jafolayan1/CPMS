﻿using AutoMapper;

using Domain.Entities;
using Domain.Interfaces;

using Infrastructure;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CPMS.Areas.Students.Controllers
{
    public class DashboardController : BaseController
    {
        private readonly IAuthenticationService _auth;
        private readonly UserManager<User> _userManager;
        private readonly IMailService _mailService;
        private readonly IUnitOfWork _context;
        private readonly IMapper _mapper;
        private readonly IFileHelper _file;
        private readonly ApplicationContext _repo;

        public DashboardController(IAuthenticationService auth, UserManager<User> userManager, IMailService mailService, IUnitOfWork context, IMapper mapper, IUserAccessor o, IFileHelper file, ApplicationContext repo) : base(o)
        {
            _auth = auth;
            _userManager = userManager;
            _mailService = mailService;
            _context = context;
            _mapper = mapper;
            _file = file;
            _repo = repo;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Profile()
        {
            var student = _context.Students.GetById(CurrentUser.UserName);
            ViewBag.Departments = _context.Departments.GetAll();
            ViewData["student"] = student;

            return View();
        }



        [HttpPost]
        public async Task<IActionResult> ProfileUpdate(Student model)
        {
            try
            {
                if (model.File != null)
                {
                    _file.DeleteFile(model.ImageUrl);
                    model.ImageUrl = _file.UploadFile(model.File);
                }

                User user = await _userManager.FindByIdAsync(CurrentUser.Id.ToString());
                user.Surname = model.Surname;
                user.OtherNames = model.OtherNames;
                user.Email = model.Email;
                user.PhoneNumber = model.PhoneNumber;
                user.ImageUrl = model.ImageUrl;

                var student = _context.Students.GetById(model.StudentId);
                var studentEntity = _mapper.Map(model, student);
                await _userManager.UpdateAsync(user);
                _context.Students.Update(studentEntity);
                await _context.SaveAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                TempData["error"] = "One or more errors occured.";
                return View(nameof(Profile));
            }
        }
    }
}