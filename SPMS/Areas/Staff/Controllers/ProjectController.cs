using AspNetCoreHero.ToastNotification.Abstractions;

using AutoMapper;

using Domain.Interfaces;

using LovePdf.Core;
using LovePdf.Model.Task;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

using Service.Configuration;

using SPMS.Hubs;

namespace SPMS.Areas.Staff.Controllers
{
    public class ProjectController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly INotyfService _notyf;
        private readonly IHubContext<ChatHub> _hubContext;
        private readonly ILovePdfSettings _pdf;
        private readonly IWebHostEnvironment _env;
        private readonly IFileHelper _file;
        private IMemoryCache _cache;

        public ProjectController(IUserAccessor userAccessor, IUnitOfWork context, IMapper mapper, IMailService mail, INotyfService notyf, IHubContext<ChatHub> hubContext, IWebHostEnvironment env, IOptions<ILovePdfSettings> pdf, IFileHelper file, IMemoryCache cache) : base(userAccessor, context, mail)
        {
            _mapper = mapper;
            _notyf = notyf;
            _hubContext = hubContext;
            _env = env;
            _pdf = pdf.Value;
            _file = file;
            _cache = cache;
        }

        [Route("pstudent")]
        [HttpGet]
        public IActionResult PStudent()
        {
            var currentUser = CurrentUser.UserName;
            var supervisor = _context.Supervisors.GetByFileNo(currentUser);
            ViewData["supervisor"] = supervisor;
            ViewData["Noti"] = GetNoti();

            return View();
        }

        //[Route("proposal")]
        [HttpGet]
        public IActionResult Proposal()
        {
            var fileNo = CurrentUser.UserName;
            var supervisor = _context.Supervisors.GetByFileNo(fileNo);
            var lstProposal = _context.Projects.Find(x => x.SupervisorId.Equals(supervisor.SupervisorId), false).Where(s => s.Status.Equals("Pending"));
            ViewData["projectProposal"] = lstProposal;
            ViewData["Noti"] = GetNoti();
            return View();
        }

        [Route("milestone")]
        [HttpGet]
        public IActionResult Milestone()
        {
            var fileNo = CurrentUser.UserName;
            var supervisor = _context.Supervisors.GetByFileNo(fileNo);
            var lstChapters = _context.Chapters.Find(x => x.SupervisorId.Equals(supervisor.SupervisorId), false).Where(s => s.Status.Equals("Pending"));
            ViewData["chapters"] = lstChapters;
            ViewData["Noti"] = GetNoti();

            return View();
        }

        [Route("projectarchive")]
        [HttpGet]
        public IActionResult ProjectArchive()
        {
            var lstProjects = _context.ProjectArchive.GetAll();
            ViewData["projectsarchive"] = lstProjects;
            ViewData["Noti"] = GetNoti();
            return View(lstProjects);
        }

        [Route("details")]
        [HttpGet]
        public IActionResult Details(int projectId)
        {
            var project = _context.Projects.GetById(projectId);
            if (project is not null)
            {
                try
                {
                    var fileName = project.FileUrl;
                    string output = Path.Combine(_env.WebRootPath, "output");
                    bool exist = Directory.Exists(output);
                    if (!exist)
                        Directory.CreateDirectory(output);
                    var manipulateFile = ManipulateFileUrl(fileName);
                    var fileExist = _file.FileExist(Path.Combine(output, manipulateFile));
                    if (fileExist)
                    {
                        ViewBag.fileName = manipulateFile;
                    }
                    else
                    {
                        var api = new LovePdfApi(_pdf.Key, _pdf.Secret);
                        var task = api.CreateTask<OfficeToPdfTask>();
                        task.AddFile($"{_env.WebRootPath}{fileName}");
                        task.Process();
                        task.DownloadFile(output);

                        string[] strname = fileName.Split('.');
                        var strfileName = $"{strname[0]}.pdf";
                        ViewBag.fileName = strfileName;
                    }
                }
                catch (Exception ex)
                {
                    TempData["Msg"] = ex.Message;
                }
            }
            ViewData["project"] = project;
            ViewData["Noti"] = GetNoti();
            return View();
        }



        [Route("cdetails")]
        [HttpGet]
        public IActionResult CDetails(int chapterId)
        {
            var chapter = _context.Chapters.GetById(chapterId);
            if (chapter is not null)
            {
                try
                {
                    var fileName = chapter.FileUrl;
                    string output = Path.Combine(_env.WebRootPath, "output");
                    bool exist = Directory.Exists(output);
                    if (!exist)
                        Directory.CreateDirectory(output);
                    var manipulateFile = ManipulateFileUrl(fileName);
                    var fileExist = _file.FileExist(Path.Combine(output, manipulateFile));
                    if (fileExist)
                    {
                        ViewBag.fileName = manipulateFile;
                    }
                    else
                    {
                        var api = new LovePdfApi(_pdf.Key, _pdf.Secret);
                        var task = api.CreateTask<OfficeToPdfTask>();
                        task.AddFile($"{_env.WebRootPath}{fileName}");
                        task.Process();
                        task.DownloadFile(output);

                        string[] strname = fileName.Split('.');
                        var strfileName = $"{strname[0]}.pdf";
                        ViewBag.fileName = strfileName;
                    }
                }
                catch (Exception ex)
                {
                    TempData["Msg"] = ex.Message;
                }
            }
            ViewData["chapter"] = chapter;
            ViewData["Noti"] = GetNoti();
            return View();
        }

        [HttpPost]
        public IActionResult Remark(IFormCollection data, int projectId, string? item)
        {
            try
            {
                string? remark = data["Remark"].ToString();
                string? status = data["Stat"].ToString();

                var prjt = _context.Projects.GetById(projectId);
                prjt.Status = status;
                prjt.Remark = remark;
                _context.Projects.Update(prjt);
                _context.SaveChanges();
                foreach (var i in prjt.Students)
                {
                    SendMail($"<p> Hello , {i.FullName.Split(' ')[0]}. <br> You have a new notification on the file you submitted</p>", i.Email);
                }
                return RedirectToAction(nameof(Proposal));
            }
            catch (Exception ex)
            {
                TempData["Msg"] = ex.Message;
                return RedirectToAction(nameof(Proposal));
            }
        }

        [HttpPost]
        public IActionResult CRemark(IFormCollection data, int chapterId, string? item)
        {
            try
            {
                string? remark = data["CRemark"].ToString();
                string? status = data["Stat"].ToString();

                var prjt = _context.Chapters.GetById(chapterId);
                prjt.Status = status;
                prjt.Remark = remark;
                _context.Chapters.Update(prjt);
                _context.SaveChanges();
                foreach (var i in prjt.Project.Students)
                {
                    SendMail($"<p> Hello , {i.FullName.Split(' ')[0]}. <br> You have a new notification on the file you submitted</p>", i.Email);
                }

                return RedirectToAction(nameof(Milestone));
            }
            catch (Exception)
            {
                TempData["Msg"] = "One or more errors occured, unable to update.";
                return RedirectToAction(nameof(Milestone));
            }
        }

        public IActionResult Status(string status, int projectId)
        {
            try
            {
                var project = _context.Projects.GetById(projectId);
                project.Status = status;
                _context.Projects.Update(project);
                _context.SaveChanges();
                foreach (var item in project.Students)
                {
                    SendMail($"<p> Hello , {item.FullName.Split(' ')[0]}. <br> You have a new notification on the file you submitted</p>", item.Email);
                }
                return RedirectToAction(nameof(Proposal));
            }
            catch (Exception)
            {
                TempData["Msg"] = "One or more errors occured, unable to update.";
                return RedirectToAction(nameof(Proposal));
            }
        }

        public IActionResult CStatus(string status, int projectId)
        {
            try
            {
                var chapter = _context.Chapters.GetById(projectId);
                chapter.Status = status;
                _context.Chapters.Update(chapter);
                _context.SaveChanges();
                foreach (var item in chapter.Project.Students)
                {
                    SendMail($"<p> Hello , {item.FullName.Split(' ')[0]}. <br> You have a new notification on the file you submitted</p>", item.Email);
                }
                return RedirectToAction(nameof(Milestone));
            }
            catch (Exception) { TempData["Msg"] = "One or more errors occured, unable to update."; return RedirectToAction(nameof(Milestone)); }
        }



        //public IActionResult Load([FromBody] Dictionary<string, string> jsonObject)
        //{
        //    Console.WriteLine("Load called");
        //    //Initialize the PDF viewer object with memory cache object
        //    PdfRenderer pdfviewer = new PdfRenderer(_cache);
        //    MemoryStream stream = new MemoryStream();
        //    object jsonResult = new object();
        //    if (jsonObject != null && jsonObject.ContainsKey("document"))
        //    {
        //        if (bool.Parse(jsonObject["isFileName"]))
        //        {
        //            string documentPath = GetDocumentPath(jsonObject["document"]);
        //            if (!string.IsNullOrEmpty(documentPath))
        //            {
        //                byte[] bytes = System.IO.File.ReadAllBytes(documentPath);
        //                stream = new MemoryStream(bytes);
        //            }
        //            else
        //            {
        //                return this.Content(jsonObject["document"] + " is not found");
        //            }
        //        }
        //        else
        //        {
        //            byte[] bytes = Convert.FromBase64String(jsonObject["document"]);
        //            stream = new MemoryStream(bytes);
        //        }
        //    }
        //    jsonResult = pdfviewer.Load(stream, jsonObject);
        //    return Content(JsonConvert.SerializeObject(jsonResult));
        //}
        ////Post action for processing the PDF documents.
        //public IActionResult RenderPdfPages([FromBody] jsonObjects responseData)
        //{
        //    PdfRenderer pdfviewer = new PdfRenderer(_cache);
        //    var jsonObject = JsonConverterstring(responseData);
        //    object jsonResult = pdfviewer.GetPage(jsonObject);
        //    return Content(JsonConvert.SerializeObject(jsonResult));
        //}
        ////Post action for unloading and disposing the PDF document resources
        //public IActionResult Unload([FromBody] jsonObjects responseData)
        //{
        //    PdfRenderer pdfviewer = new PdfRenderer(_cache);
        //    var jsonObject = JsonConverterstring(responseData);
        //    pdfviewer.ClearCache(jsonObject);
        //    return this.Content("Document cache is cleared");
        //}
        ////Post action for rendering the ThumbnailImages
        //public IActionResult RenderThumbnailImages([FromBody] jsonObjects responseData)
        //{
        //    PdfRenderer pdfviewer = new PdfRenderer(_cache);
        //    var jsonObject = JsonConverterstring(responseData);
        //    object result = pdfviewer.GetThumbnailImages(jsonObject);
        //    return Content(JsonConvert.SerializeObject(result));
        //}
        ////Post action for processing the bookmarks from the PDF documents
        //public IActionResult Bookmarks([FromBody] jsonObjects responseData)
        //{
        //    PdfRenderer pdfviewer = new PdfRenderer(_cache);
        //    var jsonObject = JsonConverterstring(responseData);
        //    object jsonResult = pdfviewer.GetBookmarks(jsonObject);
        //    return Content(JsonConvert.SerializeObject(jsonResult));
        //}
        ////Post action for rendering the annotation comments
        //public IActionResult RenderAnnotationComments([FromBody] jsonObjects responseData)
        //{
        //    PdfRenderer pdfviewer = new PdfRenderer(_cache);
        //    var jsonObject = JsonConverterstring(responseData);
        //    object jsonResult = pdfviewer.GetAnnotationComments(jsonObject);
        //    return Content(JsonConvert.SerializeObject(jsonResult));
        //}
        ////Post action for exporting the annotations

        //public IActionResult ExportAnnotations([FromBody] jsonObjects responseData)
        //{
        //    PdfRenderer pdfviewer = new PdfRenderer(_cache);
        //    var jsonObject = JsonConverterstring(responseData);
        //    string jsonResult = pdfviewer.ExportAnnotation(jsonObject);
        //    return Content(jsonResult);
        //}
        //public Dictionary<string, string> JsonConverterstring(jsonObjects results)
        //{
        //    Dictionary<string, object> resultObjects = new Dictionary<string, object>();
        //    resultObjects = results.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public)
        //        .ToDictionary(prop => prop.Name, prop => prop.GetValue(results, null));
        //    var emptyObjects = (from kv in resultObjects
        //                        where kv.Value != null
        //                        select kv).ToDictionary(kv => kv.Key, kv => kv.Value);
        //    Dictionary<string, string> jsonResult = emptyObjects.ToDictionary(k => k.Key, k => k.Value.ToString());
        //    return jsonResult;
        //}


        ////Post action for importing the annotations
        //public IActionResult ImportAnnotations([FromBody] jsonObjects responseData)
        //{
        //    PdfRenderer pdfviewer = new PdfRenderer(_cache);
        //    var jsonObject = JsonConverterstring(responseData);
        //    string jsonResult = string.Empty;
        //    object JsonResult;
        //    if (jsonObject != null && jsonObject.ContainsKey("fileName"))
        //    {
        //        string documentPath = GetDocumentPath(jsonObject["fileName"]);
        //        if (!string.IsNullOrEmpty(documentPath))
        //        {
        //            jsonResult = System.IO.File.ReadAllText(documentPath);
        //        }
        //        else
        //        {
        //            return this.Content(jsonObject["document"] + " is not found");
        //        }
        //    }
        //    else
        //    {
        //        string extension = Path.GetExtension(jsonObject["importedData"]);
        //        if (extension != ".xfdf")
        //        {
        //            JsonResult = pdfviewer.ImportAnnotation(jsonObject);
        //            return Content(JsonConvert.SerializeObject(JsonResult));
        //        }
        //        else
        //        {
        //            string documentPath = GetDocumentPath(jsonObject["importedData"]);
        //            if (!string.IsNullOrEmpty(documentPath))
        //            {
        //                byte[] bytes = System.IO.File.ReadAllBytes(documentPath);
        //                jsonObject["importedData"] = Convert.ToBase64String(bytes);
        //                JsonResult = pdfviewer.ImportAnnotation(jsonObject);
        //                return Content(JsonConvert.SerializeObject(JsonResult));
        //            }
        //            else
        //            {
        //                return this.Content(jsonObject["document"] + " is not found");
        //            }
        //        }
        //    }

        //    return Content(jsonResult);
        //}
        ////Post action for downloading the PDF documents
        //public IActionResult Download([FromBody] jsonObjects responseData)
        //{
        //    PdfRenderer pdfviewer = new PdfRenderer(_cache);
        //    var jsonObject = JsonConverterstring(responseData);
        //    string documentBase = pdfviewer.GetDocumentAsBase64(jsonObject);
        //    return Content(documentBase);
        //}
        ////Post action for printing the PDF documents
        //public IActionResult PrintImages([FromBody] jsonObjects responseData)
        //{
        //    PdfRenderer pdfviewer = new PdfRenderer(_cache);
        //    var jsonObject = JsonConverterstring(responseData);
        //    object pageImage = pdfviewer.GetPrintImage(jsonObject);
        //    return Content(JsonConvert.SerializeObject(pageImage));
        //}
        //private string GetDocumentPath(string document)
        //{
        //    string documentPath = string.Empty;
        //    if (!System.IO.File.Exists(document))
        //    {
        //        string basePath = _env.WebRootPath;
        //        string dataPath = string.Empty;
        //        dataPath = basePath + "\\";
        //        if (System.IO.File.Exists(dataPath + (document)))
        //            documentPath = dataPath + document;
        //    }
        //    else
        //    {
        //        documentPath = document;
        //    }
        //    return documentPath;
        //}
        //public class jsonObjects
        //{
        //    public string document { get; set; }
        //    public string password { get; set; }
        //    public string zoomFactor { get; set; }
        //    public string isFileName { get; set; }
        //    public string xCoordinate { get; set; }
        //    public string yCoordinate { get; set; }
        //    public string pageNumber { get; set; }
        //    public string documentId { get; set; }
        //    public string hashId { get; set; }
        //    public string sizeX { get; set; }
        //    public string sizeY { get; set; }
        //    public string startPage { get; set; }
        //    public string endPage { get; set; }
        //    public string stampAnnotations { get; set; }
        //    public string textMarkupAnnotations { get; set; }
        //    public string stickyNotesAnnotation { get; set; }
        //    public string shapeAnnotations { get; set; }
        //    public string measureShapeAnnotations { get; set; }
        //    public string action { get; set; }
        //    public string pageStartIndex { get; set; }
        //    public string pageEndIndex { get; set; }
        //    public string fileName { get; set; }
        //    public string elementId { get; set; }
        //    public string pdfAnnotation { get; set; }
        //    public string importPageList { get; set; }
        //    public string uniqueId { get; set; }
        //    public string data { get; set; }
        //    public string viewPortWidth { get; set; }
        //    public string viewportHeight { get; set; }
        //    public string tilecount { get; set; }
        //    public bool isCompletePageSizeNotReceived { get; set; }
        //    public string freeTextAnnotation { get; set; }
        //    public string signatureData { get; set; }
        //    public string fieldsData { get; set; }
        //    public string FormDesigner { get; set; }
        //    public string inkSignatureData { get; set; }
        //    public bool hideEmptyDigitalSignatureFields { get; set; }
        //    public bool showDigitalSignatureAppearance { get; set; }
        //    public bool digitalSignaturePresent { get; set; }
        //    public string tileXCount { get; set; }
        //    public string tileYCount { get; set; }
        //    public string digitalSignaturePageList { get; set; }
        //    public string annotationCollection { get; set; }
        //    public string annotationsPageList { get; set; }
        //    public string formFieldsPageList { get; set; }
        //    public string isAnnotationsExist { get; set; }
        //    public string isFormFieldAnnotationsExist { get; set; }
        //    public string documentLiveCount { get; set; }
        //    public string annotationDataFormat { get; set; }
        //}
    }

}