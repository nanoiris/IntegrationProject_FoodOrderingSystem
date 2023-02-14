using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantDao.Models
{
    public class MenuCategory
    {
        public int Id { get; set; }
        [MaxLength(30)]
        public string Name { get; set; }
        public Restaurant Owner { get; set; }
        public List<MenuItem> Menus { get; set; }
    }
}
