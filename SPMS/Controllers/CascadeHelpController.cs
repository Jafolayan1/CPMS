using CPMS.Models;

using Domain.Entities;
using Domain.Interfaces;

using Infrastructure;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;

using System.Diagnostics;

namespace CPMS.Controllers
{
    public class CascadeHelpController : BaseController
    {
        private readonly ApplicationContext _context;
        private readonly IMailService _mail;
        private readonly IWebHostEnvironment _env;


        public CascadeHelpController(IUserAccessor userAccessor, ApplicationContext context, IMailService mail, IWebHostEnvironment env) : base(userAccessor)
        {
            _context = context;
            _mail = mail;
            _env = env;
        }

        public JsonResult getSupervisors(int Id)
        {
            List<Supervisor> list = new();
            list = _context.Supervisors.Where(x => x.DepartmentId.Equals(Id)).ToList();
            list.Insert(0, new Supervisor { SupervisorId = 0, Surname = " Please Select Supervisor" });
            return Json(new SelectList(list, "SupervisorId", "FullName"));
        }

        //[HttpPost]
        //public IActionResult OnPost(string FileName)
        //{
        //    int pageCount = 0;
        //    string imageFilesFolder = Path.Combine(outputPath, Path.GetFileName(FileName).Replace(".", "_"));
        //    if (!Directory.Exists(imageFilesFolder))
        //    {
        //        Directory.CreateDirectory(imageFilesFolder);
        //    }
        //    string imageFilesPath = Path.Combine(imageFilesFolder, "page-{0}.png");
        //    using (Viewer viewer = new Viewer(Path.Combine(storagePath, FileName)))
        //    {
        //        ViewInfo info = viewer.GetViewInfo(ViewInfoOptions.ForPngView(false));
        //        pageCount = info.Pages.Count;

        //        PngViewOptions options = new PngViewOptions(imageFilesPath);
        //        viewer.View(options);
        //    }
        //    return new JsonResult(pageCount);
        //}

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        //public void changeStatus(int Id, string Item)
        //{
        //    try
        //    {
        //        var project = _context.Projects.FirstOrDefault(x => x.ProjectId.Equals(Id));
        //        project.Status = Item;
        //        _context.Projects.Update(project);
        //        _context.SaveChanges();
        //        SendMail();
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Error: {ex}");
        //    }
        //}
    }
}