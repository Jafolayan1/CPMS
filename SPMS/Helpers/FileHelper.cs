using Domain.Interfaces;

using Microsoft.AspNetCore.Mvc;

namespace CPMS.Helpers
{
    public class FileHelper : IFileHelper
    {
        private readonly IWebHostEnvironment _env;

        public FileHelper(IWebHostEnvironment env)
        {
            _env = env;
        }

        private static string GenerateFileName(string fileName)
        {
            string[] strName = fileName.Split('.');

            string strFileName = $"{DateTime.Now.ToUniversalTime():yyyyMMdd\\THHmmssfff}.{strName[^1]}";

            return strFileName;
        }

        public void DeleteFile(string fileUrl)
        {
            if (File.Exists(_env.WebRootPath + fileUrl))
            {
                File.Delete(_env.WebRootPath + fileUrl);
            }
        }

        public string UploadFile(IFormFile file)
        {
            var uploads = Path.Combine(_env.WebRootPath, "uploads");
            bool exist = Directory.Exists(uploads);
            if (!exist)
                Directory.CreateDirectory(uploads);

            var fileName = GenerateFileName(file.FileName);
            using (var fileStream = new FileStream(Path.Combine(uploads, fileName), FileMode.Create))
            {
                file.CopyToAsync(fileStream);
            }

            return "/uploads/" + fileName;
        }

        public FileResult ReadPdfFile(string file)
        {
            byte[] abc = File.ReadAllBytes(_env.WebRootPath + file);
            File.WriteAllBytes(file, abc);
            var ms = new MemoryStream(abc);
            return new FileStreamResult(ms, "application/pdf");
            //else if (file.Contains(".doc"))
            //{
            //    //StringBuilder text = new StringBuilder();
            //    //Microsoft.Office.Interop.Word.Application word = new Microsoft.Office.Interop.Word.Application();
            //    //object miss = System.Reflection.Missing.Value;
            //    //object path = @"D:\Articles2.docx";
            //    //object readOnly = true;
            //    //Microsoft.Office.Interop.Word.Document docs = word.Documents.Open(ref path, ref miss, ref readOnly, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss, ref miss);

            //    //for (int i = 0; i < docs.Paragraphs.Count; i++)
            //    //{
            //    //    text.Append(" \r\n " + docs.Paragraphs[i + 1].Range.Text.ToString());
            //    //}

            //    //return text.ToString();
            //}
            //else if (file.Contains(".docs"))
            //{

            //}
            ////return File.ReadAllText(_env.WebRootPath + file);
        }

        string IFileHelper.ReadFile(string file)
        {
            throw new NotImplementedException();
        }
    }
}