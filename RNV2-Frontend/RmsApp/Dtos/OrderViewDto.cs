using System;
using System.ComponentModel.DataAnnotations;
using RestaurantDaoBase.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Components.Forms;

namespace RmsApp.Dtos
{
    public class OrderViewDto
    {
        public string OrderId { get; set; }
        public StatusEnum Status { get; set; }

    }

}