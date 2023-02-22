using Microsoft.EntityFrameworkCore;
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
    public class OrderService : IOrderService
    {
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
        public Task<bool> DecreaseDishQty(string email, string orderItemId, string orderId)
        {
            using (var ctx = new OrderContext())
            {
                Order order = ctx.Orders.FirstOrDefault(x => x.Id == orderItemId);
            }
        }
            public Task<bool> IncreaseDishQty(string email, string orderItemId, string orderId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Order>?> ListActiveOrder()
        {
            throw new NotImplementedException();
        }

        public Task<List<Order>?> ListOrderByStatus(string restaurantId, StatusEnum status)
        {
            throw new NotImplementedException();
        }

        public async Task<Dictionary<string, Order>> ListOrderByUser(string email)
        {
            throw new NotImplementedException();
            /*
            using (var ctx = new OrderContext())
            {
                var rows = await ctx.Orders.Where(x => x.UserName == email).ToListAsync();


            }
            */
        } 
        public Task<List<Order>?> ListOrderByUserAndStatus(string email, StatusEnum status)
        {
            throw new NotImplementedException();
        }

        public Task<bool> PayCart(string email, string orderId, PayCard card)
        {
            throw new NotImplementedException();
        }

        public Task<List<Order>?> SearchOrder(string restaurantId, string email, StatusEnum status)
        {
            throw new NotImplementedException();
        }

        public Task<string> UpdateCart(string email, string menuId, string restaurantId, int orderId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateOrderStatus(string restaurantId, string orderId, StatusEnum newStatus)
        {
            throw new NotImplementedException();
        }
    }
}
