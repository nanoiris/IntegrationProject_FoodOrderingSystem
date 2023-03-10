using IdentityServer.Controllers;
using IdentityServer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System.Drawing.Imaging;
using System.IO;

namespace IdentityServer.Services
{
    public class LocalFileService : IFileService
    {
        private string dirPath;

        public LocalFileService(string dirPath)
        {
            this.dirPath = dirPath;
        }

        public AppResult DeleteFile(string fileName)
        {
            string fullFilePath = $"{dirPath}\\{fileName}";

            try
            {
                File.Delete(fullFilePath);
            }catch(System.IO.IOException ex)
            {
                return new AppResult(ex.Message,false);
            }

            return new AppResult("", true);
        }
        public string SaveFile(IFormFile formFile)
        {
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
