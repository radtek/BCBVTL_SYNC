using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sync.Core.Models
{
    public class ApiError
    {
        public string error { get; set; }
        public string error_description { get; set; }
        public string details { get; set; }
        public string message { get; set; }
        public string data { get; set; }
        public string code { get; set; }
    }
}
