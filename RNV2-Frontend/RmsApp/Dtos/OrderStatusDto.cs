using System;
using System.ComponentModel.DataAnnotations;
using RestaurantDaoBase.Models;
using RestaurantDaoBase.Enums;
using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Components.Forms;

namespace RmsApp.Dtos
{
    public class OrderStatusDto
    {
        public string OrderId { get; set; }

        public int Status { get; set; }


    }

}