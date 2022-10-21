﻿using Domain.Entities;
using Domain.Interfaces;

using Infrastructure;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CPMS.Controllers
{
	public class CascadeHelpController : BaseController
	{
		private readonly ApplicationContext _context;
		public CascadeHelpController(IUserAccessor userAccessor, ApplicationContext context) : base(userAccessor)
		{
			_context = context;
		}

		public JsonResult getSupervisors(int Id)
		{
			List<Supervisor> list = new();
			list = _context.Supervisors.Where(x => x.DepartmentId.Equals(Id)).ToList();
			list.Insert(0, new Supervisor { SupervisorId = 0, Surname = " Please Select " });
			return Json(new SelectList(list, "SupervisorId", "FullName"));
		}

		public JsonResult readFile(string file)
		{
			var read = System.IO.File.ReadAllText(file);
			return Json(read);
		}
	}

}