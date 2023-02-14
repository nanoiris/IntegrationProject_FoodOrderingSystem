using Microsoft.EntityFrameworkCore;
using RestaurantDao.Enums;
using RestaurantDao.IServices;
using RestaurantDao.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantDao.Services
{
    public partial class RmsService : IRmsService
    {
       
        public void UpdateOrderStatus(int restaurantId,Order order)
        {
            throw new NotImplementedException();
        }

        public List<Order> ListAllOrders(int restaurantId)
        {
            using (var db = new AppDbContext())
            {
                return db.Orders
                    .Where(x => x.Provider.Id == restaurantId &&
                                x.Status != Enums.StatusEnum.Cart)
                    .OrderBy(x => x.CreateTime)
                    .Take(10)
                    .ToList();
            }
        }

        public List<Order> ListOrderByStatus(int restaurantId,StatusEnum status)
        {
            using (var db = new AppDbContext())
            {
                return db.Orders.Include("Provider")
                    .Where(x => x.Provider.Id == restaurantId &&
                                x.Status == status)
                    .OrderByDescending(x => x.CreateTime)
                    .Take(10)
                    .ToList();
            }
        }
        public List<Order> SearchOrder(int restaurantId, string email, StatusEnum status)
        {
            if(email== null || email.Length == 0)
                return ListOrderByStatus(restaurantId, status);
            
            using (var db = new AppDbContext())
            {
                return db.Orders
                    .Where(x => x.Provider.Id == restaurantId &&
                                x.Status == status && x.UserName.Contains(email))
                    .OrderByDescending(x => x.CreateTime)
                    .Take(10)
                    .ToList();
            }
        }

        //public void CancelOrder(int restaurantId, int orderId)
        //{
        //    using (var db = new AppDbContext())
        //    {
        //        db.Database.BeginTransaction();
        //        try
        //        {
        //            db.OrderItems
        //                .Where(x => x.orderId == orderId)
        //                .ToList()
        //                .ForEach(x => x.Status = Enums.OrderStatusEnum.Canceled);

        //            db.Orders
        //                .Where(x => x.Provider.Id == restaurantId && x.Id == orderId)
        //                .ToList()
        //                .ForEach(x => x.Status = Enums.OrderStatusEnum.Canceled);

        //            db.SaveChanges();
        //            db.Database.CommitTransaction();
        //        }
        //        catch (Exception ex)
        //        {
        //            Console.WriteLine(ex.ToString());
        //            db.Database.RollbackTransaction();
        //        }
        //    }
        //}

        public void CancelOrder(int restaurantId, int orderId)
        {
            using (var db = new AppDbContext())
            {
               db.Orders.Where(x => x.Provider.Id == restaurantId && x.Id == orderId)
                   .ExecuteUpdate(x => x.SetProperty(r => r.Status, r => Enums.StatusEnum.Canceled));
            }
        }

        public Order FindOrderById(int restaurantId, int orderId)
        {
            using (var db = new AppDbContext())
            {
                return db.Orders.Include("Provider").Include(x => x.OrderItems).ThenInclude(x => x.Item)
                    .Where(x => x.Provider.Id == restaurantId && x.Id == orderId)
                    .FirstOrDefault();
            }
        }

        public List<Order> SearchOrderByEmail(int restaurantId, string email)
        {
            using (var db = new AppDbContext())
            {
                return db.Orders.Where(x => x.Provider.Id == restaurantId).Take(20)
                    .Where(x => x.UserName.Contains(email))
                    .OrderBy(x => x.CreateTime)
                    .ToList();
            }
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
