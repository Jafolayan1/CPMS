using CsvHelper;
using CsvHelper.Configuration;

using Domain.Entities;
using Domain.Interfaces;

using Microsoft.AspNetCore.Mvc;

using System.Globalization;

namespace CPMS.Areas.ad.Controllers
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
        public IActionResult Index(List<Account> accounts = null)
        {
            accounts ??= new List<Account>();

            return View(accounts);
        }


        [HttpPost]
        public IActionResult Index(IFormFile file)
        {
            if (file is null)
            {
                ViewBag.Error = "Please selcet a file to upload";
                return RedirectToAction("Index");
            }

            try
            {
                _file.UploadFile(file);

                var accounts = GetAccountList(file.FileName);
                return Index(accounts);

            }
            catch (Exception)
            {
                ViewBag.Error = "File is not valid, please check file type and try again";
                return RedirectToAction(nameof(Index));
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
            List<Account> accounts = new();

            try
            {
                var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    MissingFieldFound = null
                    //BadDataFound = null
                };

                //I changed "\" to "/" because i was getting file not found exception on linux container.
                //Read Csv
                var path = Directory.GetCurrentDirectory() + "/wwwroot/uploads//" + fileName;
                using (var reader = new StreamReader(path))
                using (var csv = new CsvReader(reader, config))
                {
                    csv.Read();
                    csv.ReadHeader();

                    while (csv.Read())
                    {
                        var account = csv.GetRecord<Account>();
                        accounts.Add(account);

                        //var SerialNumber = account.SerialNo;
                        //var fullName = account.FullName;
                        //var phoneNo = account.PhoneNo;
                        //var fileNo = account.FileNo;
                    }
                }

                //Create Csv

                path = Directory.GetCurrentDirectory() + "/wwwroot/uploads";
                using (var write = new StreamWriter(path + "//NewFile.csv"))
                using (var csv = new CsvWriter(write, CultureInfo.InvariantCulture))
                {
                    csv.WriteRecords(accounts);
                }
            }
            catch (BadDataException ex)
            {
                ModelState.AddModelError("", ex.Message);

                ViewBag.Error = "Something went wrong. Check uploaded file and fields";

            }

            return accounts;
        }

    }

}
