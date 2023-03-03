using RestaurantDaoBase.Models;
using Microsoft.AspNetCore.Http;
using System.Drawing.Imaging;
using System.IO;

namespace RestaurantServer.Services
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
            }
            catch (Exception ex)
            {
                return new AppResult
                {
                    Message = ex.Message,
                    IsSuccess = false,
                };
            }
            return new AppResult
            {
                Message = "File is deleted successfully",
                IsSuccess = true,
            };
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
