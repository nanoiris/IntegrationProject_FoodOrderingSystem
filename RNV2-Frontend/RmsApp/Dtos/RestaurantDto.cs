using System;
using System.ComponentModel.DataAnnotations;
// using RestaurantDaoBase.Models;
using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Components.Forms;

namespace RmsApp.Dtos
{
    public class RestaurantDto
    {
        public string? Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [MaxLength(100, ErrorMessage = "Name cannot be longer than 100 characters.")]
        public string Name { get; set; }

        public bool IsFeatured { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        public string Description { get; set; }
        public string? Logo { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        public Address Address { get; set; }

        [Required(ErrorMessage = "Phone is required.")]
        public string PhoneNo { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }

        public string CategoryId { get; set; }

        public IBrowserFile? UploadImg { get; set; }
    }

}