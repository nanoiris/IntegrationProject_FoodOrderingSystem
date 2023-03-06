using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantNetowrkApp.Data.model
{
    public class UserModel
    {
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Role { get; set; }
        public string Logo { get; set; }
        public string Token { get; set; }
        public string PhoneNumber { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string PostCode { get; set; }
        public string RestaurantId { get; set; }
        public string RestaurantName { get; set; }
        public IBrowserFile? UploadImg { get; set; }

    }
}
