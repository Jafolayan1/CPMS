using Microsoft.AspNetCore.Http;

namespace Domain.Interfaces
{
    public interface IFileHelper
    {
        void DeleteFile(string imageUrl);

        bool FileExist(string imageUrl);
        string ManipulateFile(string fileUrl, string outputExtension);


        Task<string> UploadFile(IFormFile file);
        Task<string> Upload(IFormFile file);
    }
}