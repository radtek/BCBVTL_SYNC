using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sync.Web.Models
{
    public class SYS_SYNC_QUEUE_TXN
    {
        public int ID { get; set; }
        public string OBJECT { get; set; }
        public int? OBJECT_ID { get; set; }
        public string OBJECT_CODE { get; set; }
        public string OBJECT_TYPE { get; set; }
        public string CREATED_TS { get; set; }
        public string RECEIVER { get; set; }
        public string STATUS { get; set; }
        public int? SYNC_RETRY { get; set; }
        public TimeSpan? SYNC_TS { get; set; }
    }
}