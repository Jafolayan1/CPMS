using Domain.Interfaces;

using LovePdf.Core;
using LovePdf.Model.Task;

using Microsoft.Extensions.Options;

using Service.Configuration;

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
			string strFileName = $"{DateTime.Now.ToUniversalTime():yyyyMMdd}{fileName.Replace(",", "").Replace("-", "")}";

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

		public void DeleteFile(string fileUrl)
		{
			if (File.Exists(_env.WebRootPath + fileUrl))
			{
				File.Delete(_env.WebRootPath + fileUrl);
			}
		}

		public string UploadFile(IFormFile file)
		{
			var strFileName = "";
			var uploads = Path.Combine(_env.WebRootPath, "uploads");
			bool exist = Directory.Exists(uploads);
			if (!exist)
				Directory.CreateDirectory(uploads);

			var fileName = GenerateFileName(file.FileName);
			using (var fileStream = new FileStream(Path.Combine(uploads, fileName), FileMode.Create))
			{
				file.CopyToAsync(fileStream);
				fileStream.Flush();
			}

			if (fileName.EndsWith(".docx") || fileName.EndsWith(".doc"))
			{
				try
				{
					var api = new LovePdfApi(_pdf.Key, _pdf.Secret);
					var task = api.CreateTask<OfficeToPdfTask>();
					task.AddFile($"{_env.WebRootPath}/uploads/{fileName}");
					task.Process();
					task.DownloadFile($"{_env.WebRootPath}/uploads");
					string[] strName = fileName.Split('.');
					strFileName = $"{strName[0]}.pdf";
					return "/uploads/" + strFileName;
				}
				catch (Exception)
				{
					return null;
				}
			}
			return "/uploads/" + fileName;
		}
	}
}