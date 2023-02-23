using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantDaoBase.Models
{
    public class CreateCartViewModel
    {
        public string Email { get; set; }
        public MenuItem MenuItem { get; set; }
        public string RestaurantId { get; set; }
        public string RestaurantName { get;set; }
    }
}
