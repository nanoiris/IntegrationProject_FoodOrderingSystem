using Microsoft.Build.Framework;
using Microsoft.AspNetCore.Mvc.Rendering;
using RestaurantDao.Models;
using System.ComponentModel.DataAnnotations;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;

namespace RMS.Models.Order
{
    public class EditViewModel
    {
        public string? Message { get; set; }
        public RoutePath[] Paths { get; set; }
        public EditViewModel()
        {
            Paths = new RoutePath[]
            {
               new RoutePath { ControllerName = "Home", ActionName = "Index", Titile = "Home" },
               new RoutePath { ControllerName = "Order", ActionName = "Index",Titile = "Order"},
               new RoutePath { ControllerName = "Order", ActionName = "Edit",Titile = "Edit"}
            };
        }
        public int Id { get; set; }
        public string Status { get; set; }
        [MaxLength(30)]
        public string UserName { get; set; }
        public DateTime? DesiredTime { get; set; }
        public DateTime CreateTime { get; set; }
        public double? PayTotal { get; set; }
        public List<OrderItem> OrderItems { get; set; }
    }
}
