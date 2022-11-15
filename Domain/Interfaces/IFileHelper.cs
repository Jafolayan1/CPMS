using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Domain.Interfaces
{
    public interface IFileHelper
    {
        void DeleteFile(string imageUrl);

        string UploadFile(IFormFile file);

        string ReadFile(string file);
        FileResult ReadPdfFile(string file);
    }
}