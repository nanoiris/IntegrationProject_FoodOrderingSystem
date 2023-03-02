using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OssApp.Model
{
    public class AppResult
    {
        public string message { get; set; }
        public bool isSuccess { get; set; }
        public IEnumerable<string>? Errors { get; set; }

        public AppResult() { }
  
    }
}
