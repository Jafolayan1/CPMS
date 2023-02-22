using Domain.Interfaces;

using Microsoft.Extensions.Options;

using Service.Configuration;
using GroupDocs.Viewer;
using GroupDocs.Viewer.Options;
using Microsoft.AspNetCore.Mvc;
using LovePdf.Core;
using LovePdf.Model.Task;
using System.Drawing;
using System.Security.Cryptography;
using Syncfusion.DocIO.DLS;
using Syncfusion.DocIO;
using Syncfusion.DocIORenderer;
using Syncfusion.Pdf;
using Spire.Doc;
using Microsoft.CodeAnalysis;

namespace SPMS.Helpers
{
    public class FileHelper : IFileHelper
    {
        private readonly IWebHostEnvironment _env;
        private readonly ILovePdfSettings _pdf;

        public FileHelper(IWebHostEnvironment env, IOptions<ILovePdfSettings> pdf)
        {
            _env = env;
            _pdf = pdf.Value;
        }

        private static string GenerateFileName(string fileName)
        {
            //{ DateTime.Now.ToUniversalTime():yyyyMMdd}
            string strFileName = $"{fileName.Replace(",", "").Replace("-", "")}";
            //var rnd = new Random();

            //string[] strName = fileName.Split('.');
            //var strFileName = $"{rnd.Next(5000)}.{strName[1]}";

            //string strFileName = $"{Guid.NewGuid()}.{strName[1]}";
            return strFileName;
        }

        public bool FileExist(string fileUrl)
        {
            if ((File.Exists(_env.WebRootPath + fileUrl)))
            {
                return true;
            }
            return false;
        }

        public int GetNum()
        {
            var num = 0000;
            for (int i = 0; i < num; i++)
            {
            }
            num += 1;
            return ++num;
        }
        int Gen()
        {
            Random rnd = new();
            int num = rnd.Next(1, 5000);
            return num;
        }

        public void DeleteFile(string fileUrl)
        {
            if (File.Exists(_env.WebRootPath + $"/uploads/{fileUrl}"))
            {
                File.Delete(_env.WebRootPath + $"/uploads/{fileUrl}");
            }
        }
        //public IActionResult ViewFile(string file)
        //{
        //    var uploads = Path.Combine(_env.WebRootPath, "uploads");
        //    //using (var viewer = new Viewer(file))
        //    //{
        //    //    var viewOptions = new PdfViewOptions($"viewed/{Gen}.pdf");
        //    //    viewer.View(viewOptions);
        //    //}
        //    //var fileStream = new FileStream(Path.Combine(viewed, file), FileMode.Open, FileAccess.Read);
        //    //var fResult = new FileStreamResult(fileStream, "application/pdf");
        //    //return fResult;



        //    var doc = new Document();
        //    doc.LoadFromFile(Path.Combine(uploads, file));
        //    doc.SaveToFile((Path.Combine(uploads, "output.PDF")), FileFormat.PDF);
        //    System.Diagnostics.Process.Start(Path.Combine(uploads, "output.PDF"));
        //}
        public string UploadFile(IFormFile file)
        {
            //var strFileName = "";
            var uploads = Path.Combine(_env.WebRootPath, "uploads");

            bool exist = Directory.Exists(uploads);
            if (!exist)
                Directory.CreateDirectory(uploads);

            var fileName = /*GenerateFileName(*/file.FileName;
            uploadFunction(file, uploads, fileName);

            if (fileName.EndsWith(".docx") || fileName.EndsWith(".doc"))
            {
                try
                {
                    //var api = new LovePdfApi(_pdf.Key, _pdf.Secret);
                    //var task = api.CreateTask<OfficeToPdfTask>();
                    //task.AddFile($"{_env.WebRootPath}/output/{fileName}");
                    //task.Process();
                    //task.DownloadFile($"{_env.WebRootPath}/uploads");
                    ////string[] strName = fileName.Split('.');
                    ////strFileName = $"{strName[0]}.pdf";
                    ////return "/uploads/" + strFileName;
                    ///
                }
                catch (Exception)
                {
                    return null;
                }
            }
            else
            {
                uploadFunction(file, uploads, fileName);
            }
            return "/uploads/" + fileName;
        }


        public void uploadFunction(IFormFile file,string uploads, string fileName)
        {
            using var fileStream = new FileStream(Path.Combine(uploads, fileName), FileMode.Create);
            file.CopyToAsync(fileStream);
            fileStream.Flush();
        }

    }
}