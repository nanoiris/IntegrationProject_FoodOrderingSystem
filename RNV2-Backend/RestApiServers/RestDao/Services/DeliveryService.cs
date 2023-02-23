using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RestaurantDao.Contexts;
using RestaurantDaoBase.Enums;
using RestaurantDaoBase.IServices;
using RestaurantDaoBase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantDao.Services
{
    public class DeliveryService : IDeliveryService
    {
        private readonly ILogger<DeliveryService> logger;
        public DeliveryService(ILogger<DeliveryService> logger)
        {
            this.logger = logger;
        }
        public async Task<bool> AddDelivery(Delivery delivery)
        {
            using (var ctx = new DeliveryContext())
            {
                delivery.Id = Guid.NewGuid().ToString("N");
                delivery.Status = DeliveryStatusEnum.Pending;
                delivery.CreateTime = DateTime.Now;
                ctx.Deliveries.Add(delivery);
                var result = await ctx.SaveChangesAsync();
                return result == 1 ? true : false;
            }
        }

        public Task<Delivery> FindDelivery(string id)
        {
            using (var ctx = new DeliveryContext())
            {
                return ctx.Deliveries.FirstAsync(x => x.Id == id);
            }
        }
        public Task<List<Delivery>> ListActiveDeliveries()
        {
            using (var ctx = new DeliveryContext())
            {
                return ctx.Deliveries.Where(x => x.Status == DeliveryStatusEnum.Pending)
                    .ToListAsync();
            }
        }

        public Task<List<Delivery>> ListDeliveriesByStatus(string deliveryMan, DeliveryStatusEnum status)
        {
            using (var ctx = new DeliveryContext())
            {
                return ctx.Deliveries.Where(x => x.DeliveryMan == deliveryMan && x.Status == status)
                    .ToListAsync();
            }
        }

        public Task<List<Delivery>> ListDeliveriesByStatus(DeliveryStatusEnum status)
        {
            using (var ctx = new DeliveryContext())
            {
                return ctx.Deliveries.Where(x => x.Status == status)
                    .ToListAsync();
            }
        }

        public Task<List<Delivery>> Search(string restaurantId, string email, DeliveryStatusEnum status)
        {
            using (var ctx = new DeliveryContext())
            {
                if (string.IsNullOrEmpty(restaurantId))
                {
                    if (string.IsNullOrEmpty(email))
                        return ctx.Deliveries.Take(20).Where(x => x.Status == status).ToListAsync();
                    return ctx.Deliveries.Take(20).Where(x => x.Status == status && x.CustomerName.Contains(email))
                        .ToListAsync();
                }
                if (string.IsNullOrEmpty(email))
                    return ctx.Deliveries.Take(20).Where(x => x.Status == status && x.Restaurant.Id == restaurantId)
                        .ToListAsync();
                else
                    return ctx.Deliveries.Take(20).Where(x => x.Status == status && x.Restaurant.Id == restaurantId && x.CustomerName.Contains(email))
                        .ToListAsync();
            }
        }
        public Task<List<Delivery>> Search(string restaurantId, string email)
        {
            using (var ctx = new DeliveryContext())
            {
                if (string.IsNullOrEmpty(restaurantId))
                {
                    if (string.IsNullOrEmpty(email))
                        return ctx.Deliveries.Take(20).ToListAsync();
                    return ctx.Deliveries.Take(20).Where(x => x.CustomerName.Contains(email))
                        .ToListAsync();
                }
                if (string.IsNullOrEmpty(email))
                    return ctx.Deliveries.Take(20).Where(x => x.Restaurant.Id == restaurantId)
                        .ToListAsync();
                else
                    return ctx.Deliveries.Take(20).Where(x => x.Restaurant.Id == restaurantId && x.CustomerName.Contains(email))
                        .ToListAsync();
            }
        } 
        public async Task<bool> UpdateDeliveryStatus(string deliveryId, DeliveryStatusEnum newStatus)
        {
            using (var ctx = new DeliveryContext())
            {
                var delivery = await ctx.Deliveries.FindAsync(deliveryId);
                if (delivery == null)
                    return false;
                delivery.Status = newStatus;
                var result = await ctx.SaveChangesAsync();
                return result == 1 ? true : false;
            }
        }

        public async Task<bool> Accept(string deliveryId, string deliveryMan)
        {
            using (var ctx = new DeliveryContext())
            {
                var row = await ctx.Deliveries.FindAsync(deliveryId);
                if(row == null) return false;
                if (row.Status != DeliveryStatusEnum.Pending)
                    return false;

                row.DeliveryMan= deliveryMan;
                row.AcceptTime = DateTime.Now;
                row.Status = DeliveryStatusEnum.Accept;
                var result = await ctx.SaveChangesAsync();
                return result == 1 ? true : false;
            }
        }
        public async Task<bool> Reject(string deliveryId, string deliveryMan)
        {
            using (var ctx = new DeliveryContext())
            {
                var row = await ctx.Deliveries.FindAsync(deliveryId);
                if (row == null) return false;
                if (row.Status != DeliveryStatusEnum.Assigned)
                    return false;

                row.Status = DeliveryStatusEnum.Reject;
                var result = await ctx.SaveChangesAsync();
                return result == 1 ? true : false;
            }
        }
        public async Task<bool> Complete(string deliveryId)
        {
            using (var ctx = new DeliveryContext())
            {
                var row = await ctx.Deliveries.FindAsync(deliveryId);
                if (row == null) return false;
                if (row.Status != DeliveryStatusEnum.Accept && row.Status != DeliveryStatusEnum.Assigned)
                    return false;

                row.Status = DeliveryStatusEnum.Completed;
                row.DeliveryTime= DateTime.Now;
                var result = await ctx.SaveChangesAsync();
                return result == 1 ? true : false;
            }
        }

        public async Task<bool> Assign(AssignForm form)
        {
            using (var ctx = new DeliveryContext())
            {
                var row = await ctx.Deliveries.FindAsync(form.Id);
                if (row == null) return false;
                if (row.Status != DeliveryStatusEnum.Pending)
                    return false;
                row.Status = DeliveryStatusEnum.Assigned;
                row.DeliveryMan = form.DeliveryMan;
                row.EstimateTime = form.EstimateTime;
                var result = await ctx.SaveChangesAsync();
                return result == 1 ? true : false;
            }
        }

        public async Task<bool> Pending(string deliveryId)
        {
            using (var ctx = new DeliveryContext())
            {
                var row = await ctx.Deliveries.FindAsync(deliveryId);
                if (row == null) return false;
                if (row.Status != DeliveryStatusEnum.Reject)
                    return false;
                row.Status = DeliveryStatusEnum.Pending;
                var result = await ctx.SaveChangesAsync();
                return result == 1 ? true : false;
            }
        }
    }
}
