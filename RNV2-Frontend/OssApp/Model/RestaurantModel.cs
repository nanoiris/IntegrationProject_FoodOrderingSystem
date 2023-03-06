using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OssApp.Model
{
    public class RestaurantModel : LogoModel
    {
        public string? Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public override string? Logo { get; set; }
        public string? Street { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? PostalCode { get; set; }
        public string? Country { get; set; }
        public string? PhoneNo { get; set; }
        public string? Email { get; set; }
        public bool IsFeatured { get; set; }
        public string? CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public RestaurantModel() { }
        public RestaurantModel(RestaurantModel row)
        {
            Id = row.Id;
            Name = row.Name;
            Description = row.Description;
            Logo = row.Logo;
            Street = row.Street;
            City = row.City;
            State = row.State;
            PostalCode = row.PostalCode;
            Country = row.Country;
            PhoneNo = row.PhoneNo;
            Email = row.Email;
            IsFeatured = row.IsFeatured;
            CategoryId = row.CategoryId;
            CategoryName = row.CategoryName;
        }
        public void CopyFrom(RestaurantModel row)
        {
            Name = row.Name;
            Description = row.Description;
            Logo = row.Logo;
            Street = row.Street;
            City = row.City;
            State = row.State;
            PostalCode = row.PostalCode;
            Country = row.Country;
            PhoneNo = row.PhoneNo;
            Email = row.Email;
            IsFeatured = row.IsFeatured;
            CategoryId = row.CategoryId;
            CategoryName = row.CategoryName;
        }
    }
}
