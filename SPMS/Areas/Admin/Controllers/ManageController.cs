using AspNetCoreHero.ToastNotification.Abstractions;

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
        private readonly INotyfService _notyf;


        public ManageController(IUserAccessor userAccessor, IUnitOfWork context, IFileHelper file, INotyfService notyf) : base(userAccessor, context)
        {
            _file = file;
            _notyf = notyf;
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

        [Route("manage/createstudents")]
        [HttpGet]
        public IActionResult CreateStudents(List<Students> accounts = null)
        {
            accounts ??= new List<Students>();

            return View(accounts);
        }

        [Route("manage/createstudents")]
        [HttpPost]
        public async Task<IActionResult> CreateStudents(IFormFile file)
        {
            try
            {
                await _file.UploadFile(file);

                var accounts = GetStudentList(file.FileName);
                return View(accounts);
            }
            catch (Exception)
            {
                _notyf.Error("File is not valid, please check file type and try again");
                return View(nameof(CreateStudents));
            }
        }



        [Route("manage/createlectures")]
        [HttpGet]
        public IActionResult CreateLectures(List<Lecturers> accounts = null)
        {
            accounts ??= new List<Lecturers>();
            return View(accounts);
        }


        [Route("manage/createlectures")]
        [HttpPost]
        public async Task<IActionResult> CreateLectures(IFormFile file)
        {
            try
            {
                await _file.UploadFile(file);

                var accounts = GetLecturerList($"{file.FileName}");
                return View(accounts);
            }
            catch (Exception)
            {
                _notyf.Error("File is not valid, please check file type and try again");
                return View(nameof(CreateLectures));
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
        public IActionResult AddDepartment(Department model)
        {
            var chkdpt = _context.Departments.Find(x => x.Name.Equals(model.Name), true);
            if (chkdpt is null)
            {
                TempData["error"] = "Department already exists";
                _notyf.Error("Department already exists");
                return View(nameof(Department));
            }
            var dpt = new Department()
            {
                Name = model.Name
            };
            _context.Departments.Add(dpt);
            _context.SaveChanges();
            return RedirectToAction(nameof(Department));
        }

        [HttpPost]
        public IActionResult Edit(Department model)
        {
            var dpt = _context.Departments.GetById(model.DepartmentId);
            dpt.DepartmentId = model.DepartmentId;
            dpt.Name = model.Name;
            _context.Departments.Update(dpt);
            _context.SaveChanges();
            return RedirectToAction(nameof(Department));
        }

        public IActionResult Delete(int id)
        {
            var dpt = _context.Departments.GetById(id);
            _context.Departments.Remove(dpt);
            _context.SaveChanges();
            return RedirectToAction(nameof(Department));
        }

        private List<Lecturers> GetLecturerList(string fileName)
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
                        var lectures = csv.GetRecord<Lecturers>();
                        _accounts.Add(lectures);
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
                _notyf.Error("Something went wrong. Check uploaded file and fields");
            }
            return _accounts;
        }

        private List<Students> GetStudentList(string fileName)
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
                        var students = csv.GetRecord<Students>();
                        _studentAccounts.Add(students);
                    }
                }

                path = Directory.GetCurrentDirectory() + "/wwwroot/uploads";
                using (var write = new StreamWriter(path + "//NewFile.csv"))
                using (var csv = new CsvWriter(write, CultureInfo.InvariantCulture))
                {
                    csv.WriteRecords(_studentAccounts);
                }
            }
            catch (BadDataException ex)
            {
                ModelState.AddModelError("", ex.Message);
                _notyf.Error("Something went wrong. Check uploaded file and fields");
            }
            return _studentAccounts;
        }
        [Route("manage/add")]
        public ActionResult AddAll(string save)
        {
            if (!string.IsNullOrEmpty(save))
            {
                try
                {
                    List<Supervisor> sups = new();
                    List<Student> students = new();
                    if (_accounts.Count > 0)
                    {
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
                                _notyf.Warning("Duplicate User Found");
                                return View(nameof(CreateLectures));
                            }
                        }

                        _context.Supervisors.AddRange(sups);
                        _context.SaveChanges();
                        return RedirectToAction(nameof(Supervisor));
                    }
                    else
                    {
                        foreach (var item in _studentAccounts)
                        {
                            var dpt = _context.Departments.GetById(item.Department);
                            var request = _context.Students.Find(x => x.MatricNo.Equals(item.MatricNo), false);
                            if (!request.Any())
                            {
                                students.Add(new Student { FullName = item.Names, DepartmentId = dpt.DepartmentId, Level = item.Level, MatricNo = item.MatricNo, ImageUrl = "https://cdn-icons-png.flaticon.com/512/3135/3135755.png" });
                            }
                            else
                            {
                                _notyf.Warning("Duplicate User Found");
                                return View(nameof(CreateStudents));
                            }
                        }

                        _context.Students.AddRange(students);
                        _context.SaveChanges();
                        return RedirectToAction(nameof(Student));
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Error = ex.Message;
                    return RedirectToAction(nameof(CreateStudents));
                }
            }
            return RedirectToAction(nameof(CreateStudents));
        }
    }
}