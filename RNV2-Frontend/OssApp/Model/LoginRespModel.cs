using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OssApp.Model
{
    public class LoginRespModel
    {
        public string UserName { get; set; }
        public string Role { get; set; }
        public string Logo { get; set; }
        public string Token { get; set; }
        public string RestaurantId { get; set; }
        public string RestaurantName { get; set; }
    }
}
