using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantDaoBase.Models
{
    public class MenuCategory
    {
        public string? Id { get; set; }
        //public string? PartionKey { get; set; }
        public string Name { get; set; }
        public string RestaurantId { get; set; }
        public List<MenuItem>? MenuItemList { get; set; }
    }
}
