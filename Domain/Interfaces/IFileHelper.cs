using Microsoft.AspNetCore.Http;

namespace Domain.Interfaces
{
    public interface IFileHelper
    {
        void DeleteFile(string imageUrl);

        string UploadFile(IFormFile file);
        string ReadFile(string file);

    }
}