using Domain.Entities;
using Domain.Interfaces;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using Newtonsoft.Json.Linq;

using SPMS.Models;

using System.Diagnostics;

//using PdfDocument = iText.Kernel.Pdf.PdfDocument;

namespace SPMS.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IUnitOfWork _context;
        private readonly UserManager<User> _userManager;
        private readonly IWebHostEnvironment _env;


        public HomeController(IUserAccessor o, IUnitOfWork context, UserManager<User> userManager, IWebHostEnvironment env) : base(o)
        {
            _context = context;
            _userManager = userManager;
            _env = env;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create(Message model)
        {
            if (ModelState.IsValid)
            {
                model.Username = CurrentUser.UserName;
                var sender = _userManager.GetUserAsync(User);
                model.UserId = sender.Id;
                _context.Messages.Add(model);
                _context.SaveChanges();
                return Ok();
            }
            return Error();
        }


        [HttpPost]
        public IActionResult SaveEdits([FromBody] JObject data)
        {
            try
            {
                // Get PDF data from request body
                string pdfData = (string)data["pdfData"];

                // Convert base64 string to byte array
                byte[] pdfBytes = Convert.FromBase64String(pdfData);

                // Save PDF to server
                string filePath = Path.Combine(_env.WebRootPath, "sample_edited.pdf");
                System.IO.File.WriteAllBytes(filePath, pdfBytes);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}