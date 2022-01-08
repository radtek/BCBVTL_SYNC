using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sync.Web.Models
{
    public class ResponseDocumentSync
    {
        public decimal? id { get; set; }
        public bool success { get; set; }
        public string errorMessage { get; set; }
        public string code { get; set; }
        public string JsonAPI { get; set; }
        public bool ConnectAPI { get; set; }
    }
}