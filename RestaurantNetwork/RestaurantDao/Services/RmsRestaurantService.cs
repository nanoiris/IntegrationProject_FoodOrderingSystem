using Azure.Storage.Blobs;
using Microsoft.EntityFrameworkCore;
using RestaurantDao.IServices;
using RestaurantDao.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantDao.Services
{
    public partial class RmsService : IRmsService
    {
        private void saveLogos(Restaurant restaurant, Stream logo)
        {
            var containerClient = AppDbContext.GetBlobContainerClient();
            if (logo != null && restaurant.Logo != null)
            {
                string imgType = restaurant.Logo.Substring(restaurant.Logo.LastIndexOf("."));
                restaurant.Logo = $"rest_{uid}{imgType}";

                BlobClient blobClient = containerClient.GetBlobClient(restaurant.Logo);
                using (logo)
                {
                    blobClient.UploadAsync(logo).GetAwaiter().GetResult();
                }
            }
        }
        private void deleteLogos(Restaurant restaurant)
        {
            if (restaurant.Logo != null)
            {
                try
                {
                    var containerClient = AppDbContext.GetBlobContainerClient();
                    BlobClient blobClient = containerClient.GetBlobClient(restaurant.Logo);
                    blobClient.DeleteAsync().GetAwaiter().GetResult();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }


        public List<RestCategory> ListCategory()
        {
            using (var db = new AppDbContext())
            {
                return db.RestCategoris.ToListAsync().GetAwaiter().GetResult();
            }
        }

        public RestCategory? FindCategory(int categoryId)
        {
            using (var db = new AppDbContext())
            {
                return db.RestCategoris.FindAsync(categoryId).GetAwaiter().GetResult();
            }
        }

        public Restaurant? FindRestaurant(int restaurantId)
        {
            using (var db = new AppDbContext())
            {
                var row = db.Restaurants.Include("Category").FirstOrDefaultAsync(x => x.Id == restaurantId).GetAwaiter().GetResult();
                //row.FullLogoPath = AppDbContext.GetBlobUrlPrefix() + row.Logo;
                return row;
            }
        }
       
        public void UpdateRestaurant(Restaurant restaurant, Stream logo)
        {
            uid = Guid.NewGuid().ToString("N");
            saveLogos(restaurant, logo);
            try
            {
                using (var db = new AppDbContext())
                {
                    Restaurant originalOne = db.Restaurants.FindAsync(restaurant.Id).GetAwaiter().GetResult();
                    if (originalOne != null && originalOne.Logo != null)
                    {
                        deleteLogos(originalOne);
                        originalOne.Logo = null;
                    }

                    if (originalOne != null)
                    {
                        originalOne.Name = restaurant.Name;
                        originalOne.Street = restaurant.Street;
                        originalOne.City = restaurant.City;
                        // originalOne.Country = restaurant.Country;
                        originalOne.State = restaurant.State;
                        originalOne.Description = restaurant.Description;
                        //originalOne.CategoryId = restaurant.CategoryId;
                        originalOne.Email = restaurant.Email;
                        originalOne.PhoneNo = restaurant.PhoneNo;
                        originalOne.Zip = restaurant.Zip;
                        // originalOne.Featured = restaurant.Featured;
                        originalOne.Logo = restaurant.Logo;

                        RestCategory category = db.RestCategoris.Find(restaurant.Category.Id);
                        originalOne.Category = category;

                        db.SaveChangesAsync().GetAwaiter().GetResult();
                    }
                }
            }
            catch (Exception ex)
            {
                deleteLogos(restaurant);
                //throw new SystemException("RmsService.AddRestaurant : cannot add the restaurant");
                throw new SystemException(ex.Message);
            }
        }
    }
}
