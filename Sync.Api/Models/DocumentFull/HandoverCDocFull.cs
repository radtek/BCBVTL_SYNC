using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace Sync.Api.Models.DocumentFull
{
    public class HandoverCDocFull
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
        public decimal? idQueue { get; set; }

        [XmlElement(ElementName = "CREATEDBY")]
        public String createdBy { get; set; }

        [XmlElement(ElementName = "CREATEDBYNAME")]
        public String createdByName { get; set; }

        [XmlElement(ElementName = "CREATEDDATE")]
        public String createdDate { get; set; }

        public List<DetailHandoverCDocFull> handovers { get; set; }
    }
}