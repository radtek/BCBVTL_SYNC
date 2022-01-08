using Sync.Api.Models.DocumentFull;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Sync.Api.Models
{
    public class HandoverC
    {
        [XmlElement(ElementName = "PACKAGEID")]
        public String packageId { get; set; }

        [XmlElement(ElementName = "TYPE_")]
        public String type { get; set; }

        [XmlElement(ElementName = "APPROVENAME")]
        public String approveName { get; set; }

        [XmlElement(ElementName = "COUNT_")]
        public decimal? count { get; set; }

        [XmlElement(ElementName = "IDQUEUE")]
        public decimal idQueue { get; set; }

        [XmlElement(ElementName = "CREATEDBY")]
        public String createdBy { get; set; }

        [XmlElement(ElementName = "CREATEDBYNAME")]
        public String createdByName { get; set; }

        [XmlElement(ElementName = "CREATEDDATE")]
        public String createdDate { get; set; }

        public List<DetailHandoverC> handovers { get; set; }
    }

    public class DetailHandoverC
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

        public InfoPassportC info { get; set; }
    }
}
