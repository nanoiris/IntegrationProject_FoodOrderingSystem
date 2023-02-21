using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantDaoBase.Models
{
    public class RestCategory
    {
        public string PartionKey;

        public string? Id { get; set; }
        public string Name { get; set; }
        public string? Logo { get; set; }

        public static implicit operator RestCategory(MenuCategory v)
        {
            throw new NotImplementedException();
        }
    }
}
