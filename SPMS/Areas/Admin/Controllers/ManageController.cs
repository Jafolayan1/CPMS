using CsvHelper;
using CsvHelper.Configuration;

using Domain.Entities;
using Domain.Interfaces;

using Microsoft.AspNetCore.Mvc;

using System.Globalization;

namespace SPMS.Areas.Admin.Controllers
{
	public class ManageController : BaseController
	{
		private readonly IFileHelper _file;

		public ManageController(IUserAccessor userAccessor, IUnitOfWork context, IFileHelper file) : base(userAccessor, context)
		{
			_file = file;
		}

		[Route("manage/supervisor")]
		[HttpGet]
		public IActionResult Supervisor()
		{
			ViewData["Supervisors"] = _context.Supervisors.GetAll();
			return View();
		}

		[Route("manage/student")]
		[HttpGet]
		public IActionResult Student()
		{
			ViewData["Students"] = _context.Students.GetAll();
			return View();
		}

		[HttpPost]
		public IActionResult AddSupervisor()
		{
			return View();
		}

		[HttpGet]
		public IActionResult Create(List<Account> accounts = null)
		{
			accounts ??= new List<Account>();

			return View(accounts);
		}

		[HttpPost]
		public IActionResult Create(IFormFile file)
		{
			try
			{
				_file.UploadFile(file);

				var accounts = GetAccountList($"{DateTime.Now.ToUniversalTime():yyyyMMdd}{file.FileName}");
				return View(accounts);
			}
			catch (Exception)
			{
				TempData["Error"] = "File is not valid, please check file type and try again";
				return RedirectToAction(nameof(Create));
			}
		}

		[Route("manage/department")]
		[HttpGet]
		public IActionResult Department()
		{
			ViewData["Departments"] = _context.Departments.GetAll();
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> AddDepartment(Department model)
		{
			var chkdpt = _context.Departments.Find(x => x.Name.Equals(model.Name), true);
			if (chkdpt is null)
			{
				TempData["error"] = "Department already exists";
				return RedirectToAction(nameof(Department));
			}
			var dpt = new Department()
			{
				Name = model.Name
			};
			_context.Departments.Add(dpt);
			await _context.SaveAsync();
			return RedirectToAction(nameof(Department));
		}

		[HttpPost]
		public async Task<IActionResult> Edit(Department model)
		{
			var dpt = _context.Departments.GetById(model.DepartmentId);
			dpt.DepartmentId = model.DepartmentId;
			dpt.Name = model.Name;
			_context.Departments.Update(dpt);
			await _context.SaveAsync();
			return RedirectToAction(nameof(Department));
		}

		public async Task<IActionResult> Delete(int id)
		{
			var dpt = _context.Departments.GetById(id);
			_context.Departments.Remove(dpt);
			await _context.SaveAsync();
			return RedirectToAction(nameof(Department));
		}

		private List<Account> GetAccountList(string fileName)
		{
			try
			{
				var config = new CsvConfiguration(CultureInfo.InvariantCulture)
				{
					MissingFieldFound = null
				};

				var path = Directory.GetCurrentDirectory() + "/wwwroot/uploads//" + fileName;
				using (var reader = new StreamReader(path))
				using (var csv = new CsvReader(reader, config))
				{
					csv.Read();
					csv.ReadHeader();

					while (csv.Read())
					{
						var account = csv.GetRecord<Account>();
						_accounts.Add(account);
					}
				}

				path = Directory.GetCurrentDirectory() + "/wwwroot/uploads";
				using (var write = new StreamWriter(path + "//NewFile.csv"))
				using (var csv = new CsvWriter(write, CultureInfo.InvariantCulture))
				{
					csv.WriteRecords(_accounts);
				}
			}
			catch (BadDataException ex)
			{
				ModelState.AddModelError("", ex.Message);

				TempData["Error"] = "Something went wrong. Check uploaded file and fields";
			}
			return _accounts;
		}

		[Route("manage/add")]
		public async Task<IActionResult> AddAll(string save)
		{
			if (!string.IsNullOrEmpty(save))
			{
				try
				{
					List<Supervisor> sups = new();
					foreach (var item in _accounts)
					{
						var dpt = _context.Departments.GetById(item.Department);
						var request = _context.Supervisors.Find(x => x.FileNo.Equals(item.FileNo.Replace("/", string.Empty)), false);
						if (!request.Any())
						{
							sups.Add(new Supervisor { FullName = item.FullName, DepartmentId = dpt.DepartmentId, PhoneNumber = item.PhoneNo, FileNo = item.FileNo.Replace("/", string.Empty), ImageUrl = "https://cdn-icons-png.flaticon.com/512/3135/3135755.png", Email = item.Email });
						}
						else
						{
							TempData["Error"] = "Duplicate User found";
							return RedirectToAction(nameof(Create));
						}
					}
					_context.Supervisors.AddRange(sups);
					await _context.SaveAsync();
					return RedirectToAction(nameof(Supervisor));
				}
				catch (Exception ex)
				{
					ViewBag.Error = ex.Message;
					return RedirectToAction(nameof(Create));
				}
			}
			return RedirectToAction(nameof(Create));
		}
	}
}