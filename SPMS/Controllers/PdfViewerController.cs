using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

using Syncfusion.EJ2.PdfViewer;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Interactive;
using Syncfusion.Pdf.Parsing;

using System.Net;

namespace SPMS.Controllers
{
    [Route("api/[controller]")]
    public class PdfViewerController : Controller
    {
        #region Private Variables
        private readonly IWebHostEnvironment webHost;
        private readonly IMemoryCache cache;
        #endregion

        #region Constructors
        public PdfViewerController(IWebHostEnvironment webHost, IMemoryCache cache)
        {
            this.webHost = webHost;
            this.cache = cache;
        }
        #endregion

        #region Private Routines
        private string getDocumentPath(string document)
        {
            string documentPath = string.Empty;

            if (!System.IO.File.Exists(document))
            {
                var dataPath = Path.Combine(webHost.WebRootPath, "Output");

                if (System.IO.File.Exists(Path.Combine(dataPath, document)))
                    documentPath = Path.Combine(dataPath, document);
            }
            else
            {
                documentPath = document;
            }

            return documentPath;
        }
        private IActionResult json(object response)
        {
            return Json(response, new Newtonsoft.Json.JsonSerializerSettings
            {
                ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver()
            });
        }
        #endregion

        #region Public API
        [AcceptVerbs("Post")]
        [HttpPost]
        [Route("Load")]
        public IActionResult Load([FromBody] Dictionary<string, string> jsonObject)
        {
            var stream = new MemoryStream();

            if (jsonObject != null && jsonObject.ContainsKey("document"))
            {
                if (bool.Parse(jsonObject["isFileName"]))
                {
                    var documentPath = getDocumentPath(jsonObject["document"]);

                    if (!string.IsNullOrEmpty(documentPath))
                    {
                        stream = new MemoryStream(System.IO.File.ReadAllBytes(documentPath));
                    }
                    else
                    {
                        var fileName = jsonObject["document"].Split("://")[0];

                        if (fileName == "http" || fileName == "https")
                        {
                            stream = new MemoryStream(new WebClient().DownloadData(jsonObject["document"]));
                        }
                        else
                        {
                            return this.Content($"{jsonObject["document"]} is not found");
                        }
                    }
                }
                else
                {
                    stream = new MemoryStream(Convert.FromBase64String(jsonObject["document"]));
                }
            }

            return json(new PdfRenderer(cache).Load(stream, jsonObject));
        }
        [AcceptVerbs("Post")]
        [HttpPost]
        [Route("RenderPdfPages")]
        public IActionResult RenderPdfPages([FromBody] Dictionary<string, string> jsonObject) => json(new PdfRenderer(cache).GetPage(jsonObject));
        [AcceptVerbs("Post")]
        [HttpPost]
        [Route("RenderAnnotationComments")]
        public IActionResult RenderAnnotationComments([FromBody] Dictionary<string, string> jsonObject) => json(new PdfRenderer(cache).GetAnnotationComments(jsonObject));
        [AcceptVerbs("Post")]
        [HttpPost]
        [Route("RenderThumbnailImages")]
        public IActionResult RenderThumbnailImages([FromBody] Dictionary<string, string> jsonObject) => json(new PdfRenderer(cache).GetThumbnailImages(jsonObject));
        [AcceptVerbs("Post")]
        [HttpPost]
        [Route("Bookmarks")]
        public IActionResult Bookmarks([FromBody] Dictionary<string, string> jsonObject) => json(new PdfRenderer(cache).GetBookmarks(jsonObject));
        [AcceptVerbs("Post")]
        [HttpPost]
        [Route("ExportAnnotations")]
        public IActionResult ExportAnnotations([FromBody] Dictionary<string, string> jsonObject) => Content(new PdfRenderer(cache).ExportAnnotation(jsonObject));
        [AcceptVerbs("Post")]
        [HttpPost]
        [Route("ImportAnnotations")]
        public IActionResult ImportAnnotations([FromBody] Dictionary<string, string> jsonObject)
        {
            var pdfviewer = new PdfRenderer(cache);

            if (jsonObject != null && jsonObject.ContainsKey("fileName"))
            {
                var documentPath = getDocumentPath(jsonObject["fileName"]);

                return (!string.IsNullOrEmpty(documentPath)) switch
                {
                    true => Content(System.IO.File.ReadAllText(documentPath)),
                    _ => Content($"{jsonObject["document"]} is not found")
                };
            }
            else
            {
                if (Path.GetExtension(jsonObject["importedData"]) != ".xfdf")
                {
                    return json(pdfviewer.ImportAnnotation(jsonObject));
                }
                else
                {
                    var documentPath = getDocumentPath(jsonObject["importedData"]);

                    if (!string.IsNullOrEmpty(documentPath))
                    {
                        jsonObject["importedData"] = Convert.ToBase64String(System.IO.File.ReadAllBytes(documentPath));

                        return json(pdfviewer.ImportAnnotation(jsonObject));
                    }
                    else
                    {
                        return Content($"{jsonObject["document"]} is not found");
                    }
                }
            }
        }
        [AcceptVerbs("Post")]
        [HttpPost]
        [Route("ImportFormFields")]
        public IActionResult ImportFormFields([FromBody] Dictionary<string, string> jsonObject) => json(new PdfRenderer(cache).ImportFormFields(jsonObject));
        [AcceptVerbs("Post")]
        [HttpPost]
        [Route("ExportFormFields")]
        public IActionResult ExportFormFields([FromBody] Dictionary<string, string> jsonObject) => Content(new PdfRenderer(cache).ExportFormFields(jsonObject));
        [AcceptVerbs("Post")]
        [HttpPost]
        [Route("Download")]
        public IActionResult Download([FromBody] Dictionary<string, string> jsonObject)
        {
            string fileName = jsonObject["documentId"].ToString();
            var base64String = new PdfRenderer(cache).GetDocumentAsBase64(jsonObject).Split(new string[] { "data:application/pdf;base64," }, StringSplitOptions.None)[1];

            if (base64String != null || base64String != string.Empty)
            {
                var ms = new MemoryStream();
                var ldoc = new PdfLoadedDocument(Convert.FromBase64String(base64String));

                if (ldoc.Form == null)
                    ldoc.CreateForm();

                for (int i = 0; i < ldoc.PageCount; i++)
                {
                    var annotations = (ldoc.Pages[i] as PdfLoadedPage).Annotations;

                    if (annotations != null)
                    {
                        for (int j = 0; j < annotations.Count; j++)
                        {
                            if (annotations[j] is PdfLoadedRubberStampAnnotation)
                            {
                                var stamp = annotations[j] as PdfLoadedRubberStampAnnotation;

                                if (stamp.Subject == "Sign Here")
                                {
                                    annotations.RemoveAt(j);
                                    ldoc.Form.Fields.Add(new PdfSignatureField((ldoc.Pages[i] as PdfLoadedPage), "Signature")
                                    {
                                        Bounds = stamp.Bounds,
                                        ToolTip = "Signature"
                                    });
                                    ldoc.Save(ms);
                                }
                            }
                        }
                    }
                }

                ldoc.Save(ms);
                var path = $"\\output\\{fileName}";
                System.IO.File.WriteAllBytes(webHost.WebRootPath + path, ms.ToArray());
            }

            return Ok();
        }





        [AcceptVerbs("Post")]
        [HttpPost]
        [Route("SaveDocument")]
        public ActionResult SaveDocument([FromBody] Dictionary<string, string> jsonObject)
        {
            var base64String = new PdfRenderer(cache).GetDocumentAsBase64(jsonObject).Split(new string[] { "data:application/pdf;base64," }, StringSplitOptions.None)[1];

            if (base64String != null || base64String != string.Empty)
            {
                var ms = new MemoryStream();
                var ldoc = new PdfLoadedDocument(Convert.FromBase64String(base64String));

                if (ldoc.Form == null)
                    ldoc.CreateForm();

                for (int i = 0; i < ldoc.PageCount; i++)
                {
                    var annotations = (ldoc.Pages[i] as PdfLoadedPage).Annotations;

                    if (annotations != null)
                    {
                        for (int j = 0; j < annotations.Count; j++)
                        {
                            if (annotations[j] is PdfLoadedRubberStampAnnotation)
                            {
                                var stamp = annotations[j] as PdfLoadedRubberStampAnnotation;

                                if (stamp.Subject == "Sign Here")
                                {
                                    annotations.RemoveAt(j);
                                    ldoc.Form.Fields.Add(new PdfSignatureField((ldoc.Pages[i] as PdfLoadedPage), "Signature")
                                    {
                                        Bounds = stamp.Bounds,
                                        ToolTip = "Signature"
                                    });
                                    ldoc.Save(ms);
                                }
                            }
                        }
                    }
                }

                ldoc.Save(ms);
                System.IO.File.WriteAllBytes(webHost.WebRootPath + "\\Data\\output.pdf", ms.ToArray());
            }

            return Ok();
        }
        [AcceptVerbs("Post")]
        [HttpPost]
        [Route("PrintImages")]
        public IActionResult PrintImages([FromBody] Dictionary<string, string> jsonObject) => json(new PdfRenderer(cache).GetPrintImage(jsonObject));
        [AcceptVerbs("Post")]
        [HttpPost]
        [Route("RenderPdfTexts")]
        public IActionResult RenderPdfTexts([FromBody] Dictionary<string, string> jsonObject) => json(new PdfRenderer(cache).GetDocumentText(jsonObject));
        [AcceptVerbs("Post")]
        [HttpPost]
        [Route("Unload")]
        public IActionResult Unload([FromBody] Dictionary<string, string> jsonObject)
        {
            new PdfRenderer(cache).ClearCache(jsonObject);
            return Content("Document cache is cleared");
        }
        #endregion
    }
}




//public IActionResult Download([FromBody] Dictionary<string, string> jsonObject)
//{
//    var ms = new MemoryStream();
//    var base64String = new PdfRenderer(cache).GetDocumentAsBase64(jsonObject).Split(new string[] { "data:application/pdf;base64," }, StringSplitOptions.None)[1];

//    if (base64String != null || base64String != string.Empty)
//    {
//        var ldoc = new PdfLoadedDocument(Convert.FromBase64String(base64String));

//        if (ldoc.Form == null)
//            ldoc.CreateForm();

//        for (int i = 0; i < ldoc.PageCount; i++)
//        {
//            var annotations = (ldoc.Pages[i] as PdfLoadedPage).Annotations;

//            if (annotations != null)
//            {
//                for (int j = 0; j < annotations.Count; j++)
//                {
//                    if (annotations[j] is PdfLoadedRubberStampAnnotation)
//                    {
//                        var stamp = annotations[j] as PdfLoadedRubberStampAnnotation;

//                        if (stamp.Subject == "Sign Here")
//                        {
//                            annotations.RemoveAt(j);
//                            ldoc.Form.Fields.Add(new PdfSignatureField((ldoc.Pages[i] as PdfLoadedPage), "Signature")
//                            {
//                                Bounds = stamp.Bounds,
//                                ToolTip = "Signature"
//                            });
//                            ldoc.Save(ms);
//                        }
//                    }
//                }
//            }
//        }

//        ldoc.Save(ms);
//    }

//    return Content($"data:application/pdf;base64,{Convert.ToBase64String(ms.ToArray())}");
//}
