using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sync.Core.Models
{
    public class Create_UpdateResponse
    {
        public bool success { get; set; }
        public string message { get; set; }
        public decimal id { get; set; } = 0;
        public string code { get; set; }
        public string[] messages { get; set; }
        public string data { get; set; }
        public bool ConnectDB { get; set; } = false;
    }
}
