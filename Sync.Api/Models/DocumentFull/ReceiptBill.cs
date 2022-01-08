using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace Sync.Api.Models.DocumentFull
{
    public class ReceiptBill
    {
        [XmlElement(ElementName = "RECEIPTNO")]
        public string receiptNo { get; set; }
        [XmlElement(ElementName = "CODE")]
        public string code { get; set; }
        [XmlElement(ElementName = "INVOICE_NO")]
        public string number { get; set; }
        [XmlElement(ElementName = "PRICE")]
        public decimal? price { get; set; }
        [XmlElement(ElementName = "BILLFLAG")]
        public string billFlag { get; set; }
        [XmlElement(ElementName = "DESCRIPTION")]
        public string description { get; set; }

        [XmlElement(ElementName = "CASHIERNAME")]
        public string cashierName { get; set; }

        [XmlElement(ElementName = "DATEOFRECEIPT")]
        public string dateOfReceipt { get; set; }

        [XmlElement(ElementName = "CREATEBY")]
        public string createBy { get; set; }

        [XmlElement(ElementName = "CREATEDATE")]
        public string createDate { get; set; }

        [XmlElement(ElementName = "CREATEBYNAME")]
        public string createByName { get; set; }

        [XmlElement(ElementName = "CUSTOMERNAME")]
        public string customerName { get; set; }
    }
}