using System;
using System.ComponentModel.DataAnnotations;
using RestaurantDaoBase.Models;
using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Components.Forms;

namespace RmsApp.Dtos
{
    public class RestaurantDto
    {
        public string? Id { get; set; }
        public string Name { get; set; }
        public bool IsFeatured { get; set; }
        public string Description { get; set; }
        public string? Logo { get; set; }
        public Address Address { get; set; }
        public string? PhoneNo { get; set; }
        public string Email { get; set; }
        public string CategoryId { get; set; }

        public IBrowserFile? UploadImg { get; set; }
    }

}