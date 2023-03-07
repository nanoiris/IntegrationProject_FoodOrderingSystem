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

        public Task<bool> AddDelivery(Delivery delivery);
        public Task<Delivery> FindDelivery(string id);
        public Task<Delivery?> FindByOrderId(string orderId);
        public Task<bool> UpdateDeliveryStatus(string deliveryId, DeliveryStatusEnum newStatus);
        public Task<List<Delivery>> ListDeliveriesByStatus(string deliveryMan, DeliveryStatusEnum status);
        public Task<List<Delivery>> ListDeliveriesByStatus(DeliveryStatusEnum status);
        public Task<List<Delivery>> ListActiveDeliveries();
        public Task<List<Delivery>> Search(string restaurantId, string email, DeliveryStatusEnum status);
        public Task<List<Delivery>> Search(string restaurantId, string email);

        public Task<bool> Accept(string deliveryId, string deliveryMan);
        public Task<bool> Reject(string deliveryId, string deliveryMan);
        public Task<bool> Pending(string deliveryId);
        public Task<bool> Complete(string deliveryId);
        public Task<bool> Pickup(string deliveryId);
        public Task<bool> Assign(AssignForm form);
    }
}
