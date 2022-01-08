using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sync.Core.Models
{
    public class ExcuteQueryResult
    {
        public bool ConnectDB { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
        public string XML_DATA { get; set; }
    }
}
