using RestaurantDao.Enums;
using RestaurantDao.IServices;
using RestaurantDao.Models;

namespace EndUserPortal.Models.Mock
{
    public class MenuService : IMenuService
    {
        private List<Restaurant> _restaurants;
        private List<RestCategory> _restCategories;
        private List<Restaurant> _restWeeklyTrends;
        private List<MenuCategory> _menuCategories;
        private List<MenuItem> _menuItems;

        public MenuService()
        {
            _restCategories = new List<RestCategory>(){
                new RestCategory { Id = 1, Name = "Hamberger", Logo = "../img/icons/Burger.png" },
                new RestCategory { Id = 2, Name = "Breakfast", Logo = "../img/icons/Breakfast.png" },
                new RestCategory { Id = 3, Name = "Coffee", Logo = "../img/icons/Coffee.png" },
                new RestCategory { Id = 4, Name = "Pizza", Logo = "../img/icons/Pizza.png" },
                new RestCategory { Id = 5, Name = "Fries", Logo = "../img/icons/Fries.png" },
                new RestCategory { Id = 6, Name = "Steak", Logo = "../img/icons/Steak.png" },
                new RestCategory { Id = 7, Name = "Sandwich", Logo = "../img/icons/Sandwich.png" },
                new RestCategory { Id = 8, Name = "Salad", Logo = "../img/icons/Salad.png" },
                new RestCategory { Id = 9, Name = "Fries", Logo = "../img/icons/Fries.png" },
                new RestCategory { Id = 10, Name = "Sushi", Logo = "../img/icons/sushi.png" },
            };
            _restaurants = new List<Restaurant>(){
                new Restaurant
                {
                    Id = 123,
                    Name = "Burger Kingdom",
                    Description = "Best Burger in world!",
                    Logo = "../img/trending1.png",
                    Street = "12 Saint Laurent",
                    City = "Montreal",
                    State = "QC",
                    Country = "Canada",
                    Zip = "H3H 3K5",
                    PhoneNo = "514-123-1234",
                    Rating = 4,
                    Category = _restCategories[0]

                },
                new Restaurant
                {
                    Id = 124,
                    Name = "Pizza Kingdom",
                    Description = "Best pizza in world!",
                    Logo = "../img/trending1.png",
                    Street = "12 Saint Laurent",
                    City = "Montreal",
                    State = "QC",
                    Country = "Canada",
                    Zip = "H3H 3K5",
                    PhoneNo = "514-123-1234",
                    Rating = 3,
                    Category = _restCategories[2]

                }
            };

            _menuCategories = new List<MenuCategory>()
            {
                new MenuCategory
                {
                    Id = 1,
                    Name = "Burger",
                    Owner = _restaurants[0],
                    Menus =  new List<MenuItem>()
                    {
                        new MenuItem
                        {
                            Id = 1,
                            Name = "Beef Burger",
                            Price = 5.99m,
                            Logo = "../img/beefburger.png"
                        },
                        new MenuItem
                        {
                            Id = 2,
                            Name = "Beef Burger",
                            Price = 5.99m,
                            Logo = "../img/beefburger.png"
                        },
                    }
                },
                new MenuCategory
                {
                    Id = 2,
                    Name = "Drink",
                    Owner = _restaurants[0],
                    Menus =  new List<MenuItem>()
                    {
                        new MenuItem
                        {
                            Id = 3,
                            Name = "Beef Burger",
                            Price = 1.9m,
                            Logo = "../img/beefburger.png"
                        },
                        new MenuItem
                        {
                            Id = 4,
                            Name = "Beef Burger",
                            Price = 2.9m,
                            Logo = "../img/beefburger.png"
                        },
                    }
                }
            };
            _menuItems = new List<MenuItem>
            {
               new MenuItem
                {
                    Id = 1,
                    Name = "Beef Burger",
                    Price = 5.99m,
                    Logo = "../img/beefburger.png",
                    Owner= _restaurants[0],

                },
                new MenuItem
                {
                    Id = 2,
                    Name = "Beef Burger",
                    Price = 5.99m,
                    Logo = "../img/beefburger.png",
                    Owner= _restaurants[0],
                },
                new MenuItem
                {
                    Id = 3,
                    Name = "Beef Burger",
                    Price = 1.9m,
                    Logo = "../img/beefburger.png",
                    Owner= _restaurants[0],
                },
                new MenuItem
                {
                    Id = 4,
                    Name = "Beef Burger",
                    Price = 2.9m,
                    Logo = "../img/beefburger.png",
                    Owner= _restaurants[0],
                },
            };
        }
        
        public Restaurant FindRestByMenuItem(int menuItemID)
        {
            MenuItem menuItem =  _menuItems.Where(x =>x.Id == menuItemID).FirstOrDefault();
            return menuItem.Owner;
        }

        public MenuItem GetMenuItem(int menuItemID)
        {
            return _menuItems.Find(x => x.Id == menuItemID);
        }

        public void Rate(string email, int restaurantId,int score)
        {
            throw new NotImplementedException();
        }
    }
}
