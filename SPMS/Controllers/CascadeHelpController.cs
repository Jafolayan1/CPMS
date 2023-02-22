using Domain.Entities;
using Domain.Interfaces;

using GroupDocs.Viewer.Options;

using Infrastructure;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using GroupDocs.Viewer;
//using GroupDocs.Viewer.Options;

using SPMS.Models;

using System.Diagnostics;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace SPMS.Controllers
{
	public class CascadeHelpController : BaseController
	{
		private readonly ApplicationContext _context;
		private readonly IWebHostEnvironment _env;


		public CascadeHelpController(IUserAccessor userAccessor, ApplicationContext context, IWebHostEnvironment env) : base(userAccessor)
		{
			_context = context;
			_env = env;
		}

		public JsonResult getSupervisors(int Id)
		{
			List<Supervisor> list = new();
			list = _context.Supervisors.Where(x => x.DepartmentId.Equals(Id) && x.UserId!=null).ToList();
			list.Insert(0, new Supervisor { SupervisorId = 0, FullName = " Please Select Supervisor" });
			return Json(new SelectList(list, "SupervisorId", "FullName"));
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}


		public IActionResult viewFile(string file)
		{
			var viewed = Path.Combine(_env.WebRootPath, "viewed");
			using (var viewer = new Viewer("file"))
			{
				var viewOptions = new PdfViewOptions("output.pdf");
				viewer.View(viewOptions);
			}
			var fileStream = new FileStream(Path.Combine(viewed, file), FileMode.Open, FileAccess.Read);
			var fResult = new FileStreamResult(fileStream, "application/pdf");
			return fResult;
		}
	}
}