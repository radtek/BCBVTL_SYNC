using Sync.Api.Models.DocumentFull;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace Sync.Api.Models
{
    public class TransactionFull
    {
        public Transaction transactionF { get; set; }
        public HandoverA mhandoverA { get; set; }
        public HandoverB mhandoverB { get; set; }
        public HandoverCDocFull mhandoverDC { get; set; }
        public UpdatePassportModel mpassport { get; set; }
        public OrgPerson orgPerson { get; set; }

        [XmlIgnore]
        public string newPlaceOfIssue { get; set; }
    }
}