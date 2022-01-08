using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace Sync.Api.Models
{
    public class PassportUpload
    {
        [XmlElement(ElementName = "TRANSACTIONID")]
        public string transactionId { get; set; }
        [XmlElement(ElementName = "PASSPORTNO")]
        public string passportNo { get; set; }
        [XmlElement(ElementName = "CHIPSERIALNO")]
        public string chipSerialNo { get; set; }
        [XmlElement(ElementName = "PRINTINGSITE")]
        public string printingSite { get; set; }
        [XmlElement(ElementName = "DATEOFISSUE")]
        public string dateOfIssue { get; set; }
        [XmlElement(ElementName = "DATEOFEXPIRY")]
        public string dateOfExpiry { get; set; }
        [XmlElement(ElementName = "STATUS")]
        public string status { get; set; }
        [XmlElement(ElementName = "RECEIVEBY")]
        public string receiveBy { get; set; }
        [XmlElement(ElementName = "RECEIVEDATETIME")]
        public string receiveDatetime { get; set; }
        [XmlElement(ElementName = "ISSUEBY")]
        public string issueBy { get; set; }
        [XmlElement(ElementName = "ISSUEDATETIME")]
        public string issueDatetime { get; set; }
        [XmlElement(ElementName = "REJECTBY")]
        public string rejectBy { get; set; }
        [XmlElement(ElementName = "REJECTDATETIME")]
        public string rejectDatetime { get; set; }
        [XmlElement(ElementName = "CANCELBY")]
        public string cancelBy { get; set; }
        [XmlElement(ElementName = "CANCELDATETIME")]
        public string cancelDatetime { get; set; }
        [XmlElement(ElementName = "ICAOLINE1")]
        public string icaoLine1 { get; set; }
        [XmlElement(ElementName = "ICAOLINE2")]
        public string icaoLine2 { get; set; }
        [XmlElement(ElementName = "SIGNER")]
        public string signer { get; set; }
        [XmlElement(ElementName = "POSITIONSIGNER")]
        public string positionSigner { get; set; }
        [XmlElement(ElementName = "IDQUEUE")]
        public long? idQueue { get; set; }
        [XmlElement(ElementName = "ISPASSPORT")]
        public string isEpassport { get; set; }
        [XmlElement(ElementName = "PASSPORTYPE")]
        public string passportType { get; set; }
    }
}