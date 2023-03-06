using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OssApp.Model
{
    public class RestAddress
    {
        public string? Id { get; set; }
        //public string? PartionKey { get; set; }
        public string? Name { get; set; }
        public Address? Address { get; set; }
        
    }
}
