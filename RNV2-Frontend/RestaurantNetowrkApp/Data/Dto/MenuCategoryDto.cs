using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantNetowrkApp.Data.Dto
{
    public class MenuCategoryDto
    {
        public string? Id { get; set; }
        //public string? PartionKey { get; set; }
        public string Name { get; set; }
        public string RestaurantId { get; set; }
        public List<MenuItemDto>? MenuItemList { get; set; }
    }
}
