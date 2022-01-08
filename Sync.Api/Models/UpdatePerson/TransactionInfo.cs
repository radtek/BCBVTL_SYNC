using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sync.Api.Models.UpdatePerson
{
    public class TransactionInfo
    {
        public string transactionId { get; set; }
        public string siteCode { get; set; }
        public string updateBy { get; set; }
        public string updateByName { get; set; }
        public string updateDate { get; set; }
        public string photo { get; set; }
    }

    public class TransactionInfos
    {
        public List<TransactionInfo> info { get; set; }
    }
}