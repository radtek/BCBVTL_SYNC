using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sync.Api.Models.Buf
{
    public class BufRequest
    {
        public string siteCode { get; set; }
        public string transactionId { get; set; }
    }
}