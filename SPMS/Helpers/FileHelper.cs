using Domain.Interfaces;

using Microsoft.Extensions.Options;

using Service.Configuration;

using Spire.Doc;

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
            //string strFileName = $"{fileName.Replace(",", "").Replace("-", "")}";
            //var rnd = new Random();
            var r = RandomString(8);
            string[] strName = fileName.Split('.');
            var strFileName = $"{r}.{strName[1]}";

            //string strFileName = $"{Guid.NewGuid()}.{strName[1]}";
            return strFileName;
        }


        public bool FileExist(string fileUrl)
        {
            if ((File.Exists(fileUrl)))
                return true;

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

        public async Task<string> UploadFile(IFormFile file)
        {
            var uploads = Path.Combine(_env.WebRootPath, "uploads");
            bool exist = Directory.Exists(uploads);
            if (!exist)
                Directory.CreateDirectory(uploads);

            var fileName = file.FileName;

            if (fileName.EndsWith(".docx") || fileName.EndsWith(".doc"))
            {
                if (file == null || file.Length == 0)
                    return "null, Please select a file to upload";

                if (Path.GetExtension(file.FileName) != ".doc" && Path.GetExtension(file.FileName) != ".docx")
                    return "null, Invalid file format. Only Word documents are allowed.";

                byte[] fileBytes;
                using (var memoryStream = new MemoryStream())
                {
                    await file.CopyToAsync(memoryStream);
                    fileBytes = memoryStream.ToArray();

                    var filePath = Path.Combine(uploads, file.FileName);
                    File.WriteAllBytes(filePath, fileBytes);
                    var pp = Path.Combine(uploads, fileName);
                    var count = GetPageCount(pp);

                    if (count > 25)
                    {
                        DeleteFile(fileName);
                        return "null, Kindly upload a file with less than 25 pages and try again. ( evaluation liecence )";
                    }

                }
            }
            else
            {
                using (var fileStream = new FileStream(Path.Combine(uploads, fileName), FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                    fileStream.Flush();
                }
            }
            return "/uploads/" + fileName;
        }

        public async Task<string> Upload(IFormFile file)
        {
            var uploads = Path.Combine(_env.WebRootPath, "projects");
            bool exist = Directory.Exists(uploads);
            if (!exist)
                Directory.CreateDirectory(uploads);

            var fileName = file.FileName;

            if (fileName.EndsWith(".docx") || fileName.EndsWith(".doc"))
            {
                if (file == null || file.Length == 0)
                    return "null, Please select a file to upload";

                if (Path.GetExtension(file.FileName) != ".doc" && Path.GetExtension(file.FileName) != ".docx")
                    return "null, Invalid file format. Only Word documents are allowed.";

                byte[] fileBytes;
                using (var memoryStream = new MemoryStream())
                {
                    await file.CopyToAsync(memoryStream);
                    fileBytes = memoryStream.ToArray();
                }

                var filePath = Path.Combine(uploads, file.FileName);
                File.WriteAllBytes(filePath, fileBytes);
            }
            else
            {
                using (var fileStream = new FileStream(Path.Combine(uploads, fileName), FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                    fileStream.Flush();
                }
            }
            return "/projects/" + fileName;
        }


        static int GetPageCount(string filePath)
        {
            using (Document document = new(filePath))
            {
                return document.PageCount;
            }
        }

        internal static Random _rnd = new();

        internal static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[_rnd.Next(s.Length)]).ToArray());
        }
    }
}