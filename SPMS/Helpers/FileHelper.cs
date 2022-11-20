using Domain.Interfaces;

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
                fileStream.Flush();
            }

            return "/uploads/" + fileName;
        }

        public void ReadPdfFile(string file)
        {

            //var filePath = _env.WebRootPath + file;
            //using var PDF = ChromePdfRenderer.StaticRenderUrlAsPdf(new Uri("https://en.wikipedia.org"));
            //var doc = File(PDF.BinaryData, "application/pdf", "Wiki.Pdf");
        }

        public string ReadFile(string file)
        {
            throw new NotImplementedException();
        }

        string IFileHelper.ReadPdfFile(string file)
        {
            throw new NotImplementedException();
        }
    }

}