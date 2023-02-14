using RestaurantDao.Enums;
using RestaurantDao.Models;
using RestaurantDao.ViewModel;
using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantDao.IServices
{
    public interface IOrderService
    {
        public List<Order> ListOrderByUserAndStatus(string email,StatusEnum status);
        public Dictionary<string,Order> ListOrderByUser(string email);
        public Order FindOrderByRestaurantAndUser(int restaurantId, string email);
        public Order FindCartByRestaurantAndUser(int restaurantId, string email);
        public void CancelCart(int orderId,string email);
        public void AddDishToCart(Order order,int menuId);
        public void CreateCart(string email, int menuId, int restaurantId);
        public string UpdateCart(string email, int menuId, int restaurantId,int orderId);

        public void IncreaseDishQty(string email, int orderItemId,int orderId);
        public void DecreaseDishQty(string email, int orderItemId,int orderId);

        public void PayCart(string email,int orderId,CardViewModel cardViewModel);

        public void Reorder(string email, int orderId);

        public Order GetOrderById(int orderId);
        public List<Order> getOrdersByUserName(string userName);

        public void UpdateOrderStatus(int restaurantId, int orderId, StatusEnum newStatus);

    }
}
