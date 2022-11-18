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

            var fileName = file.FileName;
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

        }

        public string ReadFile(string file)
        {
            return File.ReadAllBytes(file).ToString();
        }
    }
}