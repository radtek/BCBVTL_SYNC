using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace Sync.Api.Models.DocumentFull
{
    public class HandoverB
    {
        [XmlElement(ElementName = "PACKAGEID")]
        public string packageId { get; set; }

        [XmlElement(ElementName = "PACKAGEOLDID")]
        public string packageOldId { get; set; }

        [XmlElement(ElementName = "OFFERUSERNAME")]
        public string offerUserName { get; set; }

        [XmlElement(ElementName = "OFFERDATE")]
        public string offerDate { get; set; }

        [XmlElement(ElementName = "APPROVEUSER")]
        public string approveUser { get; set; }

        [XmlElement(ElementName = "APPROVEDATE")]
        public string approveDate { get; set; }

        [XmlElement(ElementName = "SITECODE")]
        public string siteCode { get; set; }

        [XmlElement(ElementName = "TYPE")]
        public string type { get; set; }

        [XmlElement(ElementName = "COUNT")]
        public int? count { get; set; }

        public List<DetailHandover> handovers { get; set; }

        [XmlElement(ElementName = "APPROVENAME")]
        public string approveName { get; set; }

        [XmlElement(ElementName = "PROPOSALNAME")]
        public string proposalName { get; set; }

        [XmlElement(ElementName = "APPROVEPOSITION")]
        public string approvePosition { get; set; }

        [XmlElement(ElementName = "CREATORNAME")]
        public string creatorName { get; set; }
    }
}