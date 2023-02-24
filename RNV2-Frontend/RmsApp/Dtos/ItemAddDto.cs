using System;
using System.ComponentModel.DataAnnotations;
using RestaurantDaoBase.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Components.Forms;

namespace RmsApp.Dtos
{
    public class ItemAddDto
    {
        [Required, MaxLength(20)]
        public string Name { get; set; }
        [Required, MaxLength(100)]
        public string? Description { get; set; }
        public bool IsFeatured { get; set; }
        [Required]
        public decimal Price { get; set; }
        public string? Logo { get; set; }
        public List<CategoryDto> Categories { get; set; }
        public string? CategoryId { get; set; }
        //public IFormFile? UploadImg { get; set; }
        public IBrowserFile? UploadImg { get; set; }

    }
}