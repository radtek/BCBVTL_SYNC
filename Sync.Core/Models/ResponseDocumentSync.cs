using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sync.Core.Models
{
    public partial class ResponseDocumentSync
    {
        public decimal? id { get; set; }
        public bool success { get; set; }
        public string errorMessage { get; set; }
        public string code { get; set; }
        public List<RetrieveInQuiryResult> PersonSuspectPoints { get; set; }

        public string JsonAPI { get; set; }


    }
}
