using System;
using System.ComponentModel.DataAnnotations;
using RestaurantDaoBase.Models;
namespace RmsApp.Dtos
{
    public class ItemDeleteDto
    {
        public RestaurantDaoBase.Models.MenuItem Item { get; set; }

    }
}