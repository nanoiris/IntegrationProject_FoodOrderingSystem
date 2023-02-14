using Azure.Storage.Blobs;
using Microsoft.EntityFrameworkCore;
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
    public partial class RmsService : IRmsService
    {
        private string uid;
        public void AddMenu(int restaurantId, MenuItem menuItem, Stream logo)
        {
            uid = Guid.NewGuid().ToString("N");
            saveLogos(menuItem, logo);
            try
            {
                using (var db = new AppDbContext())
                {
                    Restaurant owner = db.Restaurants.FindAsync(restaurantId).GetAwaiter().GetResult();
                    menuItem.Owner = owner;
                
                    menuItem.Category = db.MenuCategories.Find(menuItem.Category.Id);
                    db.Menus.Add(menuItem);
                    db.SaveChangesAsync().GetAwaiter().GetResult();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                deleteLogos(menuItem);
                throw new SystemException("RmsService.AddRestaurant : cannot add the menu");
            }
        }
        private void saveLogos(MenuItem menuItem, Stream logo)
        {
            var containerClient = AppDbContext.GetBlobContainerClient();

            if (logo != null && menuItem.Logo != null)
            {
                string imgType = menuItem.Logo.Substring(menuItem.Logo.LastIndexOf("."));
                menuItem.Logo = $"menu_{uid}{imgType}";
                BlobClient blobClient = containerClient.GetBlobClient(menuItem.Logo);
                using (logo)
                {
                    blobClient.UploadAsync(logo).GetAwaiter().GetResult();
                }
            }
        }
        private void deleteLogos(MenuItem menuItem)
        {
            try
            {
                var containerClient = AppDbContext.GetBlobContainerClient();
                BlobClient blobClient = containerClient.GetBlobClient(menuItem.Logo);
                blobClient.DeleteAsync().GetAwaiter().GetResult();
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        }
        public void DeleteMenu(int restaurantId, int menuId)
        {
            using (var db = new AppDbContext())
            {
                db.Menus.Where(x => x.Owner.Id == restaurantId && x.Id == menuId)
                    .ExecuteDeleteAsync()
                    .GetAwaiter().GetResult();
            }
        }
        public MenuItem? FindMenu(int restaurantId, int id)
        {
            using (var db = new AppDbContext())
            {
                return db.Menus.Include("Category").Where(x => x.Owner.Id == restaurantId && x.Id == id)
                    .FirstAsync().GetAwaiter().GetResult();
            }
        }
        public List<MenuItem>? ListMenu(int restaurantId)
        {
            using (var db = new AppDbContext())
            {
                return db.Menus.Include("Category").Where(x => x.Owner.Id == restaurantId).OrderBy(x => x.Id)
                    .ToListAsync().GetAwaiter().GetResult();
            }
        }
        public List<MenuItem>? SearchMenu(int restaurantId, string name)
        {
            using (var db = new AppDbContext())
            {
                return db.Menus.Include("Category").Where(x => x.Owner.Id == restaurantId && x.Name.Contains(name))
                    .ToListAsync().GetAwaiter().GetResult();
            }
        }
        public void UpdateMenu(int restaurantId, MenuItem menuItem, Stream logo)
        {
            uid = Guid.NewGuid().ToString("N");
            saveLogos(menuItem, logo);
            try
            {
                using (var db = new AppDbContext())
                {
                    MenuItem? updatedMenu = db.Menus.Include("Owner").FirstOrDefaultAsync(x => x.Owner.Id == restaurantId && x.Id == menuItem.Id)
                        .GetAwaiter().GetResult();

                    if (updatedMenu != null && updatedMenu.Logo != null)
                    {
                        deleteLogos(updatedMenu);
                        updatedMenu.Logo = null;
                    }
                    if (updatedMenu != null)
                    {
                        updatedMenu.Name = menuItem.Name;
                        updatedMenu.Description = menuItem.Description;
                        updatedMenu.Price = menuItem.Price;
                        updatedMenu.Discount = menuItem.Discount;
                        updatedMenu.Category = db.MenuCategories.Find(menuItem.Category.Id);
                        updatedMenu.Featured = menuItem.Featured;
                        updatedMenu.Logo = menuItem.Logo;
                        db.SaveChangesAsync().GetAwaiter().GetResult();
                    }
                }
            }
            catch (Exception ex)
            {
                deleteLogos(menuItem);
                throw new SystemException("RmsService.AddRestaurant : cannot add the restaurant");
            }
        }

        public List<MenuItem>? ListFeaturedMenu(int restaurantId)
        {
            using (var db = new AppDbContext())
            {
                return db.Menus.Include("Category").Where(x => x.Owner.Id == restaurantId && x.Featured == FeaturedEnum.Yes)
                    .OrderBy(x => x.Id).ToListAsync()
                    .GetAwaiter().GetResult();
            }
        }
        public MenuItem? FindFeaturedMenuitem(int restaurantId, int id)
        {
            using (var db = new AppDbContext())
            {
                return db.Menus.Where(x => x.Owner.Id == restaurantId && x.Id == id)
                    .FirstAsync()
                    .GetAwaiter().GetResult();
            }
        }
    }
}
