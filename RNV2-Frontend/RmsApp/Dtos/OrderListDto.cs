using System;
using System.ComponentModel.DataAnnotations;
using RestaurantDaoBase.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Components.Forms;

namespace RmsApp.Dtos
{
    public class OrderListDto
    {
        public string RestaurantId { get; set; }
        public string id { get; set; }
        public int Status { get; set; }

    }

}