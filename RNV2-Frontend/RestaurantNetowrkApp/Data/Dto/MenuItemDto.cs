using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantNetowrkApp.Data.Dto
{
    public class MenuItemDto
    {
        public string? Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public bool IsFeatured { get; set; }
        public decimal Price { get; set; }
        public string? Logo { get; set; }
        public string? CategoryId { get; set; }
    }
}
