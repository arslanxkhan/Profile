using Microsoft.AspNetCore.Http;

namespace Data.Services.Contract
{
    public interface IFileService
    {
        Task<string> UploadFileAsync(IFormFile file, string serverPath);
        void Delete(string path);
    }
}
