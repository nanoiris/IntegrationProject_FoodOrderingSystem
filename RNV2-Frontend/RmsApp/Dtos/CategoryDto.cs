using System;
using System.ComponentModel.DataAnnotations;
namespace RmsApp.Dtos
{
    public class CategoryDto
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public string RestaurantId { get; set; }

        public List<MenuItemDto>? MenuItemList { get; set; }

    }
}