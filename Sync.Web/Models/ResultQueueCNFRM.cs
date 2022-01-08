using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sync.Web.Models
{
    public class ResultQueueCNFRM
    {
        public List<SYS_SYNC_QUEUE_CNFRM> Data { get; set; }
        public bool ConnectDB { get; set; }
    }
}