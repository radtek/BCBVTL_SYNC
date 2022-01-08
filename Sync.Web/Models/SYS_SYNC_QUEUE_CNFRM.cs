using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sync.Web.Models
{
    public class SYS_SYNC_QUEUE_CNFRM
    {
        public decimal? Id { get; set; }
        public string Object_ { get; set; }
        public decimal? Object_Id { get; set; }
        public string Object_Code { get; set; }
        public decimal? Office_Id { get; set; }
        public string Sync_Status { get; set; }
    }
}