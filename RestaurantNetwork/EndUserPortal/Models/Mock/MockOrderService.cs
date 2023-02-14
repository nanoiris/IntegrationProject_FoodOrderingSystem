using RestaurantDao;
using RestaurantDao.Enums;
using RestaurantDao.IServices;
using RestaurantDao.Models;
using RestaurantDao.ViewModel;

namespace EndUserPortal.Models.Mock
{
    public class MockOrderService : IOrderService
    {
        private List<Order> _orders;
        private List<MenuItem> _menuItems;
        private List<Restaurant> _restaurants;
        private List<RestCategory> _restCategories;
        private List<MenuCategory> _menuCategories;
        private int orderItemCount = 0;
        private int orderCount = 3;

        public MockOrderService()
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


            _menuItems = new List<MenuItem>
            {
               new MenuItem
                {
                    Id = 1,
                    Name = "Beef Burger",
                    Price = 6.99m,
                    Logo = "../img/beefburger.png"
                },
                new MenuItem
                {
                    Id = 2,
                    Name = "Chicken Burger",
                    Price = 5.99m,
                    Logo = "../img/beefburger.png"
                },
                new MenuItem
                {
                    Id = 3,
                    Name = "Coke",
                    Price = 1.9m,
                    Logo = "../img/beefburger.png"
                },
                new MenuItem
                {
                    Id = 4,
                    Name = "Apple juice",
                    Price = 2.9m,
                    Logo = "../img/beefburger.png"
                },
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
                        _menuItems[0],
                        _menuItems[1]
                    }
                },
                new MenuCategory
                {
                    Id = 2,
                    Name = "Drink",
                    Owner = _restaurants[0],
                    Menus =  new List<MenuItem>()
                    {
                        _menuItems[2],
                        _menuItems[3]
                    }
                }
            };
            _orders = new List<Order>
            {
                new Order
                {
                    Id = 0,
                    Provider = _restaurants[0],
                    Status = (StatusEnum)3,
                    UserName = "anna@gmail.com",
                    OrderItems = new List<OrderItem>
                    {
                        new OrderItem
                        {
                            Item = _menuItems[1],
                            Qty= 2,
                            Status= (StatusEnum)3,
                        }
                    },
                    SubTotal = 11.99m,
                    PayTotal = 12.99m,
                },
                new Order
                {
                    Id = 1,
                    Provider = _restaurants[0],
                    Status = (StatusEnum)2,
                    UserName = "anna@gmail.com",
                    OrderItems = new List<OrderItem>
                    {
                        new OrderItem
                        {
                            Item = _menuItems[1],
                            Qty= 2,
                            Status= (StatusEnum)2,
                        }
                    },
                    SubTotal = 11.99m,
                    PayTotal = 12.99m,
                },
                new Order
                {
                    Id = 2,
                    Provider = _restaurants[0],
                    Status = (StatusEnum)1,
                    UserName = "anna@gmail.com",
                    OrderItems = new List<OrderItem>
                    {
                        new OrderItem
                        {
                            Item = _menuItems[1],
                            Qty= 2,
                            Status= (StatusEnum)1,
                        }
                    },
                    SubTotal = 11.99m,
                    PayTotal = 12.99m,
                },
            };

        }
        /*
        public void AddDishToCart(int orderId, string email, int menuItemId)
        {
            Order order = _orders.FirstOrDefault(o => o.Id == orderId);

            if (order.OrderItems.Find(x => x.Item.Id == menuItemId) == null)
            {
                MenuItem menuItem = _menuItems.FirstOrDefault(x => x.Id == menuItemId);
                order.OrderItems.Add(new OrderItem {Id = orderItemCount++, Item = menuItem, Qty = 1 });
                order.SubTotal = GetSubtotal(order);
            }
        }
        */
        public void AddDishToCart(Order order, int menuId)
        {

        }

        public void CancelCart(int orderId, string email)
        {
            _orders.RemoveAll(o => o.Id == orderId);
        }

        public void CreateCart(string email, int menuId, int restaurantId)
        {
            throw new NotImplementedException();
        }
        /*
        public void CreateCart(string email, int menuId, Restaurant restaurant)
        {

            Order newOrder = new Order()
            {
                Id = orderCount++,
                Provider = restaurant,
                Status = 0,
                UserName = "anna@gmail.com",
                CreateTime = DateTime.Now,
                OrderItems = new List<OrderItem>(),

            };
            OrderItem orderItem = new OrderItem()
            {
                Id = orderItemCount++,
                Item = _menuItems.Find(x => x.Id == menuId),
                Qty = 1,
                Status = 0,
            };
            newOrder.OrderItems.Add(orderItem);
            newOrder.SubTotal = GetSubtotal(newOrder);
            _orders.Add(newOrder);
                
        }
        */

        public void DecreaseDishQty(string email, int orderItemId, int orderId)
        {
            Order order = _orders.Find(x => x.Id == orderId);
            OrderItem orderItem = order.OrderItems.Find(x => x.Id == orderItemId);
            orderItem.Qty --;
            order.SubTotal = GetSubtotal(order);
            if (orderItem.Qty == 0)
            {
                order.OrderItems.Remove(orderItem);
            }
            if (order.OrderItems.Count == 0)
            {
                _orders.Remove(order);
            }
        }

        public Order FindCartByRestaurantAndUser(int restaurantId, string email)
        {
            if (_orders == null) return null;
            return _orders.Find(x => x.Provider.Id == restaurantId && x.Status == 0);
        }

        public Order FindOrderByRestaurantAndUser(int restaurantId, string email)
        {
            throw new NotImplementedException();
        }

        public Order GetOrderById(int orderId)
        {
            return _orders.FirstOrDefault(x =>x.Id == orderId);
        }

        public decimal GetSubtotal(Order order)
        {
            decimal subTotal = 0;
            foreach (OrderItem orderItem in order.OrderItems)
            {
                subTotal = subTotal + orderItem.Item.Price * orderItem.Qty;
            }
            order.PayTotal= subTotal * 1.15m;
            return subTotal;
        }

        public void IncreaseDishQty(string email, int orderItemId, int orderId)
        {
            Order order = _orders.Find(x => x.Id == orderId);
            OrderItem orderItem = order.OrderItems.Find(x => x.Id == orderItemId);
            orderItem.Qty++;
            order.SubTotal = (decimal?)GetSubtotal(order);
        }

        public Dictionary<string, Order> ListOrderByUser(string email)
        {
            throw new NotImplementedException();
        }

        public List<Order> ListOrderByUserAndStatus(string email, StatusEnum status)
        {
            throw new NotImplementedException();
        }

        public void PayCart(string email, int orderId, CardViewModel cardViewModel)
        {
            throw new NotImplementedException();
        }

        public void Reorder(string email, int orderId)
        {
            throw new NotImplementedException();
        }

        public string UpdateCart(string email, int menuId, int restaurantId, int orderId)
        {
            throw new NotImplementedException();
        }

        public List<Order> getOrdersByUserName(string userName)
        {
            return _orders.Where(x => x.UserName == userName).ToList();
        }

        public void UpdateOrderStatus(int restaurantId, int orderId, StatusEnum newStatus)
        {
            throw new NotImplementedException();
        }
    }
}
