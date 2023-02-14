using RestaurantDao;
using RestaurantDao.IServices;
using RestaurantDao.Models;

namespace EndUserPortal.Models.Mock
{
    public class MockRestaurantService : IRestarantService
    {
        private List<Restaurant> _restaurants;
        private List<RestCategory> _restCategories;
        private List<Restaurant> _restWeeklyTrends;
        private List<MenuCategory> _menuCategories;

        public MockRestaurantService() {
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

                },
                 new Restaurant
                {
                    Id = 125,
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
                    Id = 126,
                    Name = "Pizza Kingdom",
                    Description = "Best pizza in world!",
                    Logo = "../img/trending1.png",
                    Street = "12 Saint Laurent",
                    City = "Montreal",
                    State = "QC",
                    Country = "Canada",
                    Zip = "H3H 3K5",
                    PhoneNo = "514-123-1234",
                    Rating = 5,
                    Category = _restCategories[2]

                },
                 new Restaurant
                {
                    Id = 127,
                    Name = "Burger Kingdom",
                    Description = "Best Burger in world!",
                    Logo = "../img/trending1.png",
                    Street = "12 Saint Laurent",
                    City = "Montreal",
                    State = "QC",
                    Country = "Canada",
                    Zip = "H3H 3K5",
                    PhoneNo = "514-123-1234",
                    Rating = 2,
                    Category = _restCategories[0]

                },
                new Restaurant
                {
                    Id = 128,
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

                },
                 new Restaurant
                {
                    Id = 129,
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
                    Id = 130,
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
            _restWeeklyTrends = new List<Restaurant>()
            {
                 new Restaurant
                {
                    Id = 131,
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
                    Id = 132,
                    Name = "Pizza Kingdom",
                    Description = "Best pizza in world!",
                    Logo = "../img/trending1.png",
                    Street = "12 Saint Laurent",
                    City = "Montreal",
                    State = "QC",
                    Country = "Canada",
                    Zip = "H3H 3K5",
                    PhoneNo = "514-123-1234",
                    Rating = 4,
                    Category = _restCategories[3]

                },
                 new Restaurant
                {
                    Id = 133,
                    Name = "Sandwich Kingdom",
                    Description = "Best Sandwich in world!",
                    Logo = "../img/trending1.png",
                    Street = "12 Saint Laurent",
                    City = "Montreal",
                    State = "QC",
                    Country = "Canada",
                    Zip = "H3H 3K5",
                    PhoneNo = "514-123-1234",
                    Rating = 4,
                    Category = _restCategories[6]

                },
                new Restaurant
                {
                    Id = 134,
                    Name = "Pizza Kingdom",
                    Description = "Best pizza in world!",
                    Logo = "../img/trending1.png",
                    Street = "12 Saint Laurent",
                    City = "Montreal",
                    State = "QC",
                    Country = "Canada",
                    Zip = "H3H 3K5",
                    PhoneNo = "514-123-1234",
                    Rating = 4,
                    Category = _restCategories[3]

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
            List<Rating> ratingList = new List<Rating>()
            {
                new Rating
                {
                    Id = 1,
                    Value = 5,
                },
                new Rating
                {
                    Id = 2,
                    Value = 4,
                },
                new Rating
                {
                    Id = 3,
                    Value = 4,
                },
                new Rating
                {
                    Id = 4,
                    Value = 3,
                },
                new Rating
                {
                    Id = 5,
                    Value = 3,
                },
                new Rating
                {
                    Id = 6,
                    Value = 3,
                },
                new Rating
                {
                    Id = 7,
                    Value = 5,
                },
                new Rating
                {
                    Id = 8,
                    Value = 4,
                },
                new Rating
                {
                    Id = 9,
                    Value = 2,
                },
            };
            _restaurants[0].MenuCategories= _menuCategories;
            _restaurants[0].Ratings = ratingList;
        }
        

        public List<RestCategory> ListCategory()
        {
            return _restCategories;
        }

        public List<Restaurant> ListWeeklyTrends()
        {
            return _restWeeklyTrends;
        }

        public List<Restaurant> ListWithLimit(int limit)
        {
            return _restaurants.GetRange(0, limit);
        }

        public int CountAll()
        {
            throw new NotImplementedException();
        }

        public List<Restaurant> ListPage(int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }

        public List<Restaurant> ListPageAfterLastId(int pageIndex, string LastRestId)
        {
            throw new NotImplementedException();
        }

        public List<Restaurant> Search(string searchKey, string category, string sortField)
        {
         
            List<Restaurant> restaurantsRes = _restaurants.Where(x => x.Name.Contains(searchKey) && x.Category.Name == category).ToList();

            return restaurantsRes;
        }

        public Restaurant GetRestById(int id)
        {
            return _restaurants.FirstOrDefault(x => x.Id == id);
        }

        public List<Restaurant> ListPage(int pageIndex)
        {
            throw new NotImplementedException();
        }
        public List<MenuItem> GetFeaturedMenus(int id)
        {
            throw new NotImplementedException();
        }
        public string[] CalculateRatings(int restaurantId)
        {
            throw new NotImplementedException();
        }

        public int CalculateTotalRatings(int restaurantId)
        {
            throw new NotImplementedException();
        }

    }
}
