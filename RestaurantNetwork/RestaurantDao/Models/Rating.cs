using RestaurantDao.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantDao.Models
{
    public class Rating
    {
        public int Id { get; set; }
        public Restaurant Target { get; set; }
        public int Value { get; set; }
        public string CreateBy { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
