using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sync.Web.Models
{
    public class SyncDocumentListToA08Model
    {
        public decimal LIST_ID { get; set; }
        public decimal OFFICE_ID { get; set; }
        public string XML_DATA { get; set; }
        public string LIST_CODE { get; set; }
        public string Message { get; set; }
        public bool Success { get; set; }
        public bool ConnectDB { get; set; }
    }
}