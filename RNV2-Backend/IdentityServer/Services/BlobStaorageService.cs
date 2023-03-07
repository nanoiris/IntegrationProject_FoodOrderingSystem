using IdentityServer.Models;
using Azure.Storage.Blobs;
using System;

namespace IdentityServer.Services
{
    public class BlobStorageService : IFileService
    {
        private readonly string connectionString;
        private readonly string containerName;
        public BlobStorageService(string connectionString,string containerName)
        {
            this.connectionString = connectionString;
            this.containerName = containerName;
        }
        private BlobContainerClient getContainerClient()
        {
            BlobContainerClient containerClient = null;
            try
            {
                Console.WriteLine(connectionString);
                var blobServiceClient = new BlobServiceClient(connectionString);
                containerClient = blobServiceClient.GetBlobContainerClient(containerName);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return containerClient;
        }


        public AppResult DeleteFile(string fileName)
        {
            try
            {
                var containerClient = getContainerClient();
                BlobClient blobClient = containerClient.GetBlobClient(fileName);
                blobClient.DeleteAsync().GetAwaiter().GetResult();
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
            var containerClient = getContainerClient();
            if (containerClient == null)
                return null;

            string uid = Guid.NewGuid().ToString("N");
            string uploadFileName = formFile.FileName;
            string imgType = uploadFileName.Substring(uploadFileName.LastIndexOf("."));
            string logoName = $"{uid}{imgType}";

            BlobClient blobClient = containerClient.GetBlobClient(logoName);
            using (Stream inputStream = formFile.OpenReadStream())
            {
                blobClient.UploadAsync(inputStream).GetAwaiter().GetResult();
            }
            return logoName;
        }
    }
}
