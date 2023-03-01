using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantDaoBase.Models
{
    public class LogoViewModel
    {
        public string Id { get; set; }
        public IFormFile? UploadImg { get; set; }
        public string FileName { get; set; }
        public long? FileSize { get; set;}
    }
}
