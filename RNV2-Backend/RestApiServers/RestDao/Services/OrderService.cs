using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Cosmos.Query.Internal;
using Microsoft.Extensions.Logging;
using RestaurantDao.Contexts;
using RestaurantDaoBase.Enums;
using RestaurantDaoBase.IServices;
using RestaurantDaoBase.Models;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace RestaurantDao.Services
{
    public class OrderService : IOrderService
    {
        private readonly ILogger<OrderService> logger;
        public OrderService(ILogger<OrderService> logger)
        {
            this.logger = logger;
        }
        public async Task<bool> AddDishToCart(string orderId, MenuItem menuItem)
        {
            using (var ctx = new OrderContext())
            {
                var order = await ctx.Orders.FindAsync(orderId);
                if (order == null)
                    return false;
                
                var itemList = order.ItemList;
                if(itemList == null)
                    itemList = new List<OrderItem>();

                decimal subtotal = order.SubTotal ?? 0;

                var orderItem = itemList.Find(x => x.Item?.Id == menuItem.Id);
                if(orderItem == null)
                {
                    orderItem = new OrderItem
                    {
                        Id = Guid.NewGuid().ToString("N"),
                        Qty = 1,
                        Status = StatusEnum.Cart,
                        Item = menuItem,
                        OrderId = orderId
                    };
                    itemList.Add(orderItem);
                }
                else
                {
                    orderItem.Qty = orderItem.Qty + 1;
                }
                subtotal += orderItem.Item!.Price;
                order.SubTotal = subtotal;
                order.PayTotal = subtotal * 1.15m;
                var result = await ctx.SaveChangesAsync();
                return result == 1 ? true : false; 
            }
        }

        public async Task<bool> CancelOrder(string orderId)
        {
            using (var ctx = new OrderContext())
            {
                Order? order = await ctx.Orders.FindAsync(orderId);
                if (order == null)
                    return false;
                 order!.Status = StatusEnum.Canceled;
                var result = await ctx.SaveChangesAsync();
                return result == 1 ? true : false;
            }
        }

        public async Task<bool> CreateCart(string email, MenuItem menuItem, string restaurantId,string restaurantName)
        {
            Order newOrder = new Order()
            {
                Id = Guid.NewGuid().ToString("N"),
                Status = StatusEnum.Cart,
                UserName = email,
                CreateTime = DateTime.Now,
                RestaurantId = restaurantId,
                RestaurantName = restaurantName,
                SubTotal = menuItem.Price,
                PayTotal = menuItem.Price * 1.15m
            };
            using (var ctx = new OrderContext())
            {
                OrderItem orderItem = new OrderItem
                {
                    Id = Guid.NewGuid().ToString("N"),
                    Qty = 1,
                    Status = StatusEnum.Cart,
                    Item = menuItem,
                    OrderId = newOrder.Id,
                };
                newOrder.ItemList = new List<OrderItem> { orderItem };

                ctx.Orders.Add(newOrder);
                var result = await ctx.SaveChangesAsync();
                return result == 1 ? true : false;
            } 
        }

        public Task<Order> FindOrderById(string orderId)
        {
            using (var ctx = new OrderContext())
            {
                return ctx.Orders.Where(x => x.Id == orderId).FirstAsync();
            }
        }
        public async Task<bool> DecreaseDishQty(string orderItemId, string orderId)
        {
            using (var ctx = new OrderContext())
            {
                Order order = await ctx.Orders.Where(x => x.Id == orderId).FirstAsync();
                if (order == null)
                    return false;
                var orderItem = order.ItemList?.Where(x => x.Id == orderItemId).First();
                if(orderItem == null) 
                    return false;
                if (orderItem.Qty > 1)
                {
                    orderItem.Qty -= 1;
                }
                else
                {
                    order.ItemList?.Remove(orderItem);
                }
                order.SubTotal -= orderItem.Item?.Price;
                order.PayTotal = order.SubTotal * 1.15m;
                var result = await ctx.SaveChangesAsync();
                return result == 1 ? true : false;
            }
            throw new NotImplementedException();
        }
        public async Task<bool> IncreaseDishQty(string orderItemId, string orderId)
        {
            using (var ctx = new OrderContext())
            {
                Order order = await ctx.Orders.Where(x => x.Id == orderId).FirstAsync();
                if (order == null)
                    return false;

                var orderItem = order.ItemList?.Where(x => x.Id == orderItemId).First();
                if (orderItem == null)
                    return false;

                orderItem.Qty += 1;
                order.SubTotal += orderItem.Item?.Price;
                order.PayTotal = order.SubTotal * 1.15m;
                
                var result = await ctx.SaveChangesAsync();
                return result == 1 ? true : false;
            }
        }

        public Task<List<Order>> ListActiveOrder()
        {
            using (var ctx = new OrderContext())
            {
                return ctx.Orders.Where(x => x.Status != StatusEnum.Cart
                           && x.Status != StatusEnum.Completed
                           && x.Status != StatusEnum.Canceled)
                    .OrderByDescending(x => x.CreateTime)
                    .ToListAsync();
            }
        }
        public Task<List<Order>> ListOrderByStatus(string restaurantId, StatusEnum status)
        {
            using (var ctx = new OrderContext())
            {
                return ctx.Orders.Where(x => x.RestaurantId == restaurantId && x.Status == status)
                    .OrderByDescending(x => x.CreateTime)
                    .ToListAsync();
            }
        }  
        public async Task<Dictionary<string, List<Order>>> ListOrderByUser(string email)
        {
            using (var ctx = new OrderContext())
            {
                Dictionary<string, List<Order>> dict = new Dictionary<string, List<Order>>();
                foreach(StatusEnum status in Enum.GetValues(typeof(StatusEnum)))
                {
                    var rows = await ctx.Orders.Where(x => x.UserName.Contains(email) && x.Status == status).ToListAsync();
                    if (rows != null)
                        dict.Add(status.ToString(), rows);
                }
                return dict;
            }
        } 
        public Task<List<Order>> ListOrderByUserAndStatus(string email, StatusEnum status)
        {
            using (var ctx = new OrderContext())
            {
                return ctx.Orders.Where(x => x.UserName.Contains(email) && x.Status == status).ToListAsync();
            }
        }

        public async Task<bool> PayCart(string orderId, PayCard card)
        {
            using (var ctx = new OrderContext())
            {
                var order = await ctx.Orders.FindAsync(orderId);
                if (order == null)
                    return false;
                order.Card = card;
                order.PayTime = DateTime.Now;
                var result = await ctx.SaveChangesAsync();
                return result == 1 ? true : false;
            }
        }
        public Task<List<Order>> SearchOrder(string restaurantId, string email, StatusEnum status)
        {
            if(string.IsNullOrEmpty(email))
                return ListOrderByStatus(restaurantId,status);

            using (var ctx = new OrderContext())
            {
                if (string.IsNullOrEmpty(restaurantId))
                    return ctx.Orders.Where(x => x.UserName.Contains(email) && x.Status == status).ToListAsync();
                else
                    return ctx.Orders
                      .Where(x => x.RestaurantId == restaurantId && x.UserName.Contains(email) && x.Status == status)
                      .ToListAsync();
            }
        }

        public Task<List<Order>> SearchOrder(string restaurantId, string email)
        {
            using (var ctx = new OrderContext())
            {
                if (string.IsNullOrEmpty(restaurantId))
                {
                    if (string.IsNullOrEmpty(email))
                        return ctx.Orders.Take(20).ToListAsync();
                    return ctx.Orders.Take(20).Where(x => x.UserName.Contains(email))
                        .ToListAsync();
                }
                if (string.IsNullOrEmpty(email))
                    return ctx.Orders.Take(20).Where(x => x.RestaurantId == restaurantId)
                        .ToListAsync();
                else
                    return ctx.Orders.Take(20).Where(x => x.RestaurantId == restaurantId && x.UserName.Contains(email))
                        .ToListAsync();
            }
        }
        public async Task<bool> UpdateOrderStatus(string orderId, StatusEnum newStatus)
        {
            using (var ctx = new OrderContext())
            {
                var order = await ctx.Orders.FindAsync(orderId);
                if (order == null)
                    return false;
                order.Status = newStatus;
                var result = await ctx.SaveChangesAsync();
                return result == 1 ? true : false;
            }
        }
    }
}
