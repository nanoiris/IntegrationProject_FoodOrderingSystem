using System;
using System.ComponentModel.DataAnnotations;
using RestaurantDaoBase.Models;
namespace RmsApp.Dtos
{
    public class MenuItemDto
    {
        public string? Id { get; set; }
        [Required, MaxLength(20)]
        public string Name { get; set; }
        [Required, MaxLength(100)]
        public string? Description { get; set; }
        public bool IsFeatured { get; set; }
        [Required]
        public decimal Price { get; set; }
        public string? Logo { get; set; }
        public string? CategoryId { get; set; }
    }
}
