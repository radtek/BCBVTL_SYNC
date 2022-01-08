using Sync.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sync.Core.Entities.PA
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

    public class Result_Queue_TXN_Model
    {
        public IList<SYS_SYNC_QUEUE_TXN> Data { get; set; }
        public bool ConnectDB { get; set; }
    }

    public class SyncDocumentListToA08Model : ExcuteQueryResult
    {
        public decimal LIST_ID { get; set; }
        public decimal OFFICE_ID { get; set; }
        public string LIST_CODE { get; set; }
    }
}
