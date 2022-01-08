using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace Sync.Api.Models.DocumentFull
{
    public class PaymentDetail
    {
        [XmlElement(ElementName = "TRANSACTIONID")]
        public string transactionId { get; set; }
        [XmlElement(ElementName = "TYPEPAYMENT")]
        public string typePayment { get; set; }
        [XmlElement(ElementName = "SUBTYPEPAYMENT")]
        public string subTypePayment { get; set; }
        [XmlElement(ElementName = "PAYMENTAMOUNT")]
        public string paymentAmount { get; set; }
        [XmlElement(ElementName = "STATUSFEE")]
        public string statusFee { get; set; }
        [XmlElement(ElementName = "NAMEPAYMENT")]
        public string namePayment { get; set; }
    }
}