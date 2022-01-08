using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sync.Api.Models
{
    public class ThongTinXML
    {
        public string mrzLine1 { get; set; }
        public string mrzLine2 { get; set; }
        public string dg1 { get; set; }
        public string dg2 { get; set; }
        public string dg3 { get; set; }
        public string dg4 { get; set; }
        public string sod { get; set; }
        public string com { get; set; }
        public string xml { get; set; }
        public string dateOfExpiry { get; set; }
        public ThongTinHCTonTai infoPassport { get; set; }
    }

    public class ThongTinHCTonTai
    {
        public string passportNo { get; set; }
        public string passportType { get; set; }
        public string chipSerialNo { get; set; }
        public string dateOfIssue { get; set; }
        public string dateOfExpiry { get; set; }
        public string icaoLine1 { get; set; }
        public string icaoLine2 { get; set; }
        public string signerName { get; set; }
        public string signerPosition { get; set; }
        public string description { get; set; }
        public string status { get; set; }
        public string placeOfIssueId { get; set; }
        public string placeOfIssueName { get; set; }
        public string personId { get; set; }
        public string fpEncode { get; set; }
        public bool isEpassport { get; set; }
    }
}