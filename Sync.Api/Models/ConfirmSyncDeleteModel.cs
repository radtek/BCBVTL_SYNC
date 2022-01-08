using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sync.Api.Models
{
    public class ConfirmSyncDeleteModel
    {
        public string Message { get; set; }
        public bool Success { get; set; }
        public bool ConnectDB { get; set; }
    }
}