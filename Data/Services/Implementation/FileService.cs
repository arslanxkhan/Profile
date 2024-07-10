using Data.Services.Contract;
using Microsoft.AspNetCore.Http;

namespace Data.Services.Implementation
{
    public class FileService : IFileService
    {

        public async Task<string> UploadFileAsync(IFormFile file, string serverPath)
        {
            var fileName = ($"image_{Guid.NewGuid()} {Path.GetExtension(file.FileName)}").Trim();

            CreateDirectory(serverPath);

            var filePath = Path.Combine(serverPath, fileName);
            try
            {
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return fileName;
        }



        public void Delete(string path)
        {
            if (File.Exists(path))
            { File.Delete(path); }
        }


        private void CreateDirectory(string serverPath)
        {
            if (!Directory.Exists(serverPath))
                Directory.CreateDirectory(serverPath);
        }
    }
}
