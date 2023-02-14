using Microsoft.EntityFrameworkCore;
using RestaurantDao.Enums;
using RestaurantDao.IServices;
using RestaurantDao.Models;

namespace RestaurantDao.Services
{
    public partial class RmsService : IRmsService
    {
        public void AddCategory(int restaurantId, MenuCategory category)
        {
            using (var db = new AppDbContext())
            {
                Restaurant? owner = db.Restaurants.FindAsync(restaurantId).GetAwaiter().GetResult();
                if (owner == null)
                    throw new ArgumentException($"RmsService.AddCategory : no such a restaurant. Id={restaurantId}");

                category.Owner = owner;
                db.MenuCategories.Add(category);
                db.SaveChangesAsync().GetAwaiter().GetResult();
            }
        }

        public void DeleteCategory(int restaurantId, int categoryId)
        {
            using (var db = new AppDbContext())
            {
                int count = db.Menus.Where(x => x.Owner.Id == restaurantId && x.Category.Id == categoryId).CountAsync().GetAwaiter().GetResult();
                if (count > 0)
                {
                    throw new ArgumentException($"RmsService.DeleteCategory : the category is online, cannot delete it");
                }
                int total = db.MenuCategories.Where(x => x.Owner.Id == restaurantId && x.Id == categoryId)
                    .ExecuteDeleteAsync()
                    .GetAwaiter().GetResult();
            }
        }

        public MenuCategory? FindCategory(int restaurantId, int id)
        {
            using (var db = new AppDbContext())
            {
                return db.MenuCategories.Where(x => x.Owner.Id == restaurantId && x.Id == id).FirstOrDefaultAsync().GetAwaiter().GetResult();
            }
        }

        public List<MenuCategory> ListCategry(int restaurantId)
        {
            using (var db = new AppDbContext())
            {
                return db.MenuCategories.Where(x => x.Owner.Id == restaurantId).OrderBy(x => x.Id).ToListAsync().GetAwaiter().GetResult();
            }
        }

        public void UpdateCategory(int restaurantId, MenuCategory category)
        {
            using (var db = new AppDbContext())
            {
                Restaurant? owner = db.Restaurants.FindAsync(restaurantId).GetAwaiter().GetResult();
                if (owner == null)
                    throw new ArgumentException($"RmsService.UpdateCategory : no such a restaurant. Id={restaurantId}");

                category.Owner = owner;
                
                int total = db.MenuCategories.Where(x => x.Owner.Id == restaurantId && x.Id ==  category.Id)
                  .ExecuteUpdateAsync(x => x.SetProperty(b => b.Name,b => category.Name))
                  .GetAwaiter().GetResult();
            }
        }
    }
}
