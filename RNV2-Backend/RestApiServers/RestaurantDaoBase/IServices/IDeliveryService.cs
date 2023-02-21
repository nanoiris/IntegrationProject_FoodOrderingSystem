using RestaurantDaoBase.Enums;
using RestaurantDaoBase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantDaoBase.IServices
{
    public interface IDeliveryService
    {

        public void AddDelivery(Delivery delivery);
        public void UpdateDeliveryStatus(string deliveryId, DeliveryStatusEnum newStatus);
        public List<Delivery> ListDeliveriesByStatus(string deliveryManId, StatusEnum status);
        public List<Delivery> ListDeliveriesByStatus(StatusEnum status);
        public List<Delivery> ListActiveDeliveries();
        public List<Delivery> SearchDelivery(string restaurantId, string email, StatusEnum status);
    }
}
