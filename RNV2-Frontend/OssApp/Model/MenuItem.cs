using Microsoft.AspNetCore.Http;
using RestaurantDaoBase.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OssApp.Model
{
    public class MenuItem
    {
        public string? Name { get; set; }
        public decimal? Price { get; set; }
    }
}
