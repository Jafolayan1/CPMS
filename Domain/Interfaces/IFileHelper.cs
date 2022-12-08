using Microsoft.AspNetCore.Http;

namespace Domain.Interfaces
{
	public interface IFileHelper
	{
		void DeleteFile(string imageUrl);

		bool FileExist(string imageUrl);

		string UploadFile(IFormFile file);
	}
}