using Microsoft.AspNetCore.Identity;
using RestaurantDao.Models;
using RestaurantDao.Enums;
using Microsoft.AspNetCore.Http.HttpResults;

namespace RMS.Models.Order
{
    public class ListViewModel
    {
        public string? Message { get; set; }
        public RoutePath[] Paths { get; set; }
        public string[] TabHeaders { get; set; }
        public ListViewModel()
        {
            Paths = new RoutePath[]
            {
               new RoutePath { ControllerName = "Home", ActionName = "Index", Titile = "Home" },
               new RoutePath { ControllerName = "Order", ActionName = "Index",Titile = "Order"}
            };
            TabHeaders = new string[] { "OrderNo.", "Customer", "Total ($)", "Created Time", "Pickup Time", "Status", "", "" };
        }
        public List<RestaurantDao.Models.Order>? Rows { get; set; }
        public string? SearchKey { get; set; }
        public StatusEnum SearchStatus { get; set; }
        public RoutePath[] EditBtns { get; set; }

    }
}
