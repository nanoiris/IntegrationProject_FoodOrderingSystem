using IdentityServer.Models;
using Microsoft.AspNetCore.Http;
using System.Drawing.Imaging;
using System.IO;

namespace IdentityServer.Services
{
    public class LocalFileService : IFileService
    {
        public AppResult DeleteFile(string fileName, IConfiguration Configuration)
        {
            string dirPath = Configuration["LogoRoorPath"];
            string fullFilePath = $"{dirPath}\\{fileName}";

            File.Delete(fullFilePath);
            return new AppResult
            {
                Message = "File is deleted successfully",
                IsSuccess = true,
            };
        }
        public string SaveFile(IFormFile formFile, IConfiguration Configuration)
        {
            string dirPath = Configuration["LogoRootPath"];

            string uid = Guid.NewGuid().ToString("N");
            string uploadFileName = formFile.FileName;
            string imgType = uploadFileName.Substring(uploadFileName.LastIndexOf("."));

            string fullFilePath = $"{dirPath}\\{uid}{imgType}";
            using (Stream inputStream = formFile.OpenReadStream())
            {
                using (Stream fileStream = File.Create(fullFilePath))
                {
                    inputStream.CopyToAsync(fileStream).GetAwaiter().GetResult();
                    return $"{uid}{imgType}";
                }
            }
        }
    }
}
