using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace Sync.Api.Models.DocumentFull
{
    public class DetailHandoverCDocFull
    {
        [XmlElement(ElementName = "TRANSACTIONID")]
        public String transactionId { get; set; }

        [XmlElement(ElementName = "PERSONID")]
        public decimal? personId { get; set; }

        [XmlElement(ElementName = "RECEIPTNO")]
        public String receiptNo { get; set; }

        [XmlElement(ElementName = "REISTRATIONNO")]
        public String registrationNo { get; set; }

        [XmlElement(ElementName = "TRANSACTIONSTATUS")]
        public String transactionStatus { get; set; }

        [XmlElement(ElementName = "GLOBALID")]
        public decimal? globalId { get; set; }

        [XmlElement(ElementName = "PASSPORTNO")]
        public String passportNo { get; set; }

        public List<PaymentDetail> payments { get; set; }

        public InfoPassportC_Doc_Full info { get; set; }
    }
}