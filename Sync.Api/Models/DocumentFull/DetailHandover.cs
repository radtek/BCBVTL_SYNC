using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace Sync.Api.Models.DocumentFull
{
    public class DetailHandover
    {
        [XmlElement(ElementName = "PACKAGEID")]
        public string packageId { get; set; }
        [XmlElement(ElementName = "TRANSACTIONID")]
        public string transactionId { get; set; }
        [XmlElement(ElementName = "APPROVESTAGE")]
        public string approveStage { get; set; }
        [XmlElement(ElementName = "OFFERSTAGE")]
        public string offerStage { get; set; }
        [XmlElement(ElementName = "NOTEOFFER")]
        public string noteOffer { get; set; }
        [XmlElement(ElementName = "NOTEAPPROVE")]
        public string noteApprove { get; set; }
        public List<PaymentDetail> payments { get; set; }
        [XmlElement(ElementName = "PERSONCODE")]
        public string personCode { get; set; }
        [XmlElement(ElementName = "PERSONSTAGE")]
        public string personStage { get; set; }
        [XmlElement(ElementName = "PERSONORGCODE")]
        public string personOrgCode { get; set; }
        public OrgPerson orgPerson { get; set; }
    }
}