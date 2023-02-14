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
    public partial class OssService : IOssService
    {
        public List<Order> ListActiveOrder()
        {
            using (var db = new AppDbContext())
            {
                return db.Orders.Include("Provider").Where(x => x.Status != Enums.StatusEnum.Cart
                               && x.Status != Enums.StatusEnum.Completed && x.Status != Enums.StatusEnum.Canceled)
                    .OrderBy(x => x.CreateTime).Take(10)
                    .ToList();
            }
        }
        public void CancelOrder(int orderId)
        {
            using (var db = new AppDbContext())
            {

                db.Orders.Where(x => x.Id == orderId)
                   .ExecuteUpdate(x => x.SetProperty(r => r.Status, r => Enums.StatusEnum.Canceled));
                
                //Order order = db.Orders.Find(orderId);
                //order.Status = Enums.OrderStatusEnum.Canceled;
                //db.SaveChanges();
            }
        }
        public Order FindOrderById(int orderId)
        {
            using (var db = new AppDbContext())
            {
                return db.Orders.Include("Provider").Include(x => x.OrderItems).ThenInclude(x => x.Item ).First(x => x.Id == orderId);
            }
        }
        public List<Order> SearchOrderByEmail(string email)
        {
            using (var db = new AppDbContext())
            {
                return db.Orders.Include("Provider").Take(20)
                    .Where(x => x.UserName.Contains(email) && x.Status != StatusEnum.Cart && x.Status != StatusEnum.Canceled)
                    .OrderBy(x => x.CreateTime)
                    .ToList();
            }
        }
        public void UpdateOrder(Order order)
        {
            throw new NotImplementedException();
        }
    }
}
