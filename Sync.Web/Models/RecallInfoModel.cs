using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sync.Web.Models
{
    public class RecallInfoModel
    {
        public decimal Id { get; set; }
        public string Apitype { get; set; }
        public string Url { get; set; }
        public string Header { get; set; }
        public string Body { get; set; }
        public string Reftable { get; set; }
        public string Result { get; set; }
    }
}