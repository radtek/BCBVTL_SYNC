using Microsoft.Ajax.Utilities;
using Sync.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sync.Api.Models
{
    public class HandoverResultModel
    {
        public string packageId { get; set; } 
        public decimal idQueue { get; set; }
        public Create_UpdateResponse result { get; set; }
    }

    public class DocumentListResultModel
    {
        public string handoverId { get; set; }
        public decimal idQueue { get; set; }
        public Create_UpdateResponse result { get; set; }
    }

    public class ttImageListResultModel
    {
        public string transactionId { get; set; }
        public decimal idQueue { get; set; }
        public Create_UpdateResponse result { get; set; }
    }
}