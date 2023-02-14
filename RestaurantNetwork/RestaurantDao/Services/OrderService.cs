using Microsoft.EntityFrameworkCore;
using RestaurantDao.Enums;
using RestaurantDao.IServices;
using RestaurantDao.Models;
using RestaurantDao.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantDao.Services
{
    public class OrderService : IOrderService
    {
        public void AddDishToCart(Order order, int menuId)
        {
            using (var db = new AppDbContext())
            {
                db.Database.BeginTransaction();
                try
                {
                    order = db.Orders.Include("OrderItems").FirstOrDefault(x => x.Id == order.Id);
                    OrderItem orderItem = db.OrderItems.Include("Item").Where(x => x.order == order && x.Item.Id == menuId).FirstOrDefault();
                    decimal subtotal = order.SubTotal??0;
                    if (orderItem != null)
                    {
                        orderItem.Qty = orderItem.Qty + 1;
                        db.SaveChanges();
                        subtotal += orderItem.Item.Price;
                    }
                    else
                    {
                        OrderItem newOrderItem = new OrderItem
                        {
                            Qty = 1,
                            Status = StatusEnum.Cart,
                            order = order
                        };
                        newOrderItem.Item = db.Menus.Find(menuId);
                        db.OrderItems.Add(newOrderItem);
                        db.SaveChanges();
                        subtotal += newOrderItem.Item.Price;
                    }
                    order.SubTotal = subtotal;
                    order.PayTotal= subtotal * 1.15m;
                    db.SaveChanges();
                    db.Database.CommitTransaction();
                }
                catch(Exception ex) 
                { 
                    db.Database.RollbackTransaction();
                    throw new SystemException("OrderService.AddDishToCart: " + ex.Message);
                }
            }
        }
        public void CancelCart(int orderId, string email)
        {
            using (var db = new AppDbContext())
            {
                Order order = db.Orders.Find(orderId); 
                if (order != null)
                {
                    order.Status = StatusEnum.Canceled;
                    db.SaveChanges();
                }
            }
        }
        public void CreateCart(string email, int menuId, int restaurantId)
        {
            Order newOrder = new Order()
            {
                Status = StatusEnum.Cart,
                UserName = email,
                CreateTime = DateTime.Now,
            };

            using (var db = new AppDbContext())
            {
                MenuItem item = db.Menus.Find(menuId);
                newOrder.Provider = db.Restaurants.FirstOrDefault(x => x.Id == restaurantId);
                newOrder.SubTotal = item.Price;
                newOrder.PayTotal = newOrder.SubTotal * 1.15m;
                OrderItem orderItem = new OrderItem
                {
                    Qty = 1,
                    Status = StatusEnum.Cart,
                    Item = item,
                    order = newOrder
                };
                newOrder.OrderItems = new List<OrderItem> { orderItem };
                db.Orders.Add(newOrder);
                db.SaveChanges();
            }
        }
        public void DecreaseDishQty(string email, int orderItemId, int orderId)
        {
            using (var db = new AppDbContext())
            {
                db.Database.BeginTransaction();
                try
                {
                    Order order = db.Orders.FirstOrDefault(x => x.Id == orderId);
                    OrderItem orderItem = db.OrderItems.Include("Item").Where(x => x.Id == orderItemId).FirstOrDefault();
                    order.SubTotal -= orderItem.Item.Price;
                    order.PayTotal = order.SubTotal * 1.15m;
                    db.SaveChanges();
                    if (orderItem.Qty > 1)
                    {
                        orderItem.Qty -= 1;
                        db.SaveChanges();
                    }
                    else
                    {
                        db.OrderItems.Remove(orderItem);
                        db.SaveChanges();
                    }
                    db.Database.CommitTransaction();
                }
                catch (Exception ex)
                {
                    db.Database.RollbackTransaction();
                    throw new SystemException("OrderService.DecreaseDishQty: " + ex.Message);
                }
            } 
        }
        public void IncreaseDishQty(string email, int orderItemId, int orderId)
        {
            using (var db = new AppDbContext())
            {
                db.Database.BeginTransaction();
                try
                {
                    Order order = db.Orders.FirstOrDefault(x => x.Id == orderId);
                    OrderItem orderItem = db.OrderItems.Include("Item").Where(x => x.Id == orderItemId).FirstOrDefault();
                    order.SubTotal += orderItem.Item.Price;
                    order.PayTotal = order.SubTotal * 1.15m;
                    db.SaveChanges();
                    orderItem.Qty += 1;
                    db.SaveChanges();
                    db.Database.CommitTransaction();
                }
                catch (Exception ex)
                {
                    db.Database.RollbackTransaction();
                    throw new SystemException("OrderService.DecreaseDishQty: " + ex.Message);
                }
            }
        }

        public Order FindCartByRestaurantAndUser(int restaurantId, string email)
        {
            using(var db = new AppDbContext())
            {
                var order =  db.Orders.FirstOrDefault(
                      x => x.Provider.Id == restaurantId && x.UserName == email
                      && x.Status == StatusEnum.Cart
                    );
                if(order != null)
                {
                    List<OrderItem> orderItems = db.OrderItems.Include("Item").Where(x => x.order.Equals(order))
                    .ToList();
                    order.OrderItems = orderItems;
                }
                return order;    
            }
        }

        public Order FindOrderByRestaurantAndUser(int restaurantId, string email)
        {
            throw new NotImplementedException();
        }

        public Order GetOrderById(int orderId)
        {
            using (var db = new AppDbContext())
            {
                return db.Orders.Include("Provider").FirstOrDefault(x => x.Id == orderId);
            }
        }  
        public List<Order> getOrdersByUserName(string userName)
        {
            using (var db = new AppDbContext())
            {
                List<Order> orders =  db.Orders.Include("Provider").Include(x => x.OrderItems).ThenInclude(r => r.Item).Where(x => x.UserName == userName)
                    .OrderByDescending(x => x.CreateTime) .ToList();
               
                return orders;
            }
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
        public void UpdateOrderStatus(int restaurantId, int orderId, StatusEnum newStatus)
        {
            using (var db = new AppDbContext())
            {
                var order = db.Orders
                    .FirstOrDefault(x => x.Provider.Id == restaurantId && x.Id == orderId);

                if (order != null)
                {
                    order.Status = newStatus;
                    db.SaveChanges();
                }
            }
        }

    }
}
