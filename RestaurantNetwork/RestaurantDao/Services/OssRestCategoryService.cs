using Azure.Storage.Blobs;
using RestaurantDao.Enums;
using RestaurantDao.IServices;
using RestaurantDao.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantDao.Services
{
    public partial class OssService : IOssService
    {
        public void AddRestaurantCategory(RestCategory category, Stream logo)
        {
            uid = Guid.NewGuid().ToString("N");
            saveCategoryLogo(category, logo);
            try
            {
                using (var db = new AppDbContext())
                {
                    db.RestCategoris.Add(category);
                    db.SaveChangesAsync().GetAwaiter().GetResult();
                }
            }
            catch (Exception ex)
            {
                deleteCategoryLogo(category);
                throw new SystemException("OssService.AddRestaurantCategory : cannot add the restaurant category");
            }
        }

        private void saveCategoryLogo(RestCategory category, Stream logo)
        {
            var containerClient = AppDbContext.GetBlobContainerClient();

            if (logo != null)
            {
                string imgType = category.Logo.Substring(category.Logo.LastIndexOf("."));
                category.Logo = $"restCate_{uid}{imgType}";

                BlobClient blobClient = containerClient.GetBlobClient(category.Logo);
                using (logo)
                {
                    blobClient.UploadAsync(logo).GetAwaiter().GetResult();
                }
            }
        }

        private void deleteCategoryLogo(RestCategory category)
        {
            if(category.Logo != null)
            {
                try
                {
                    var containerClient = AppDbContext.GetBlobContainerClient();
                    BlobClient blobClient = containerClient.GetBlobClient(category.Logo);
                    blobClient.DeleteAsync().GetAwaiter().GetResult();
                }catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public void DeleteCategory(int categoryId)
        {
            using (var db = new AppDbContext())
            {
                RestCategory? row = db.RestCategoris.FindAsync(categoryId)
                    .GetAwaiter().GetResult();
                if (row != null)
                {
                    deleteCategoryLogo(row);
                    db.Remove(row);
                    db.SaveChangesAsync().GetAwaiter().GetResult();
                }
            }
        }   
    }
}
