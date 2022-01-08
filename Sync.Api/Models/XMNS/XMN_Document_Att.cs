using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Sync.Api.Models.XMNS
{
    public class XMN_Document_Att
    {
        [XmlElement(ElementName = "CODE")]
        public string code { get; set; }

        [XmlElement(ElementName = "DOCTYPE")]
        public string docType { get; set; }

        [XmlElement(ElementName = "DOCDATA")]
        public string docData { get; set; }

        [XmlElement(ElementName = "CREATEDDATE")]
        public string createdDate { get; set; }

        [XmlElement(ElementName = "CREATEDBYNAME")]
        public string createdByName { get; set; }

        [XmlElement(ElementName = "UPDATEDDATE")]
        public string updatedDate { get; set; }

        [XmlElement(ElementName = "UPDATEDBYNAME")]
        public string updatedByName { get; set; }

        [XmlElement(ElementName = "UPDATEDBY")]
        public string updatedBy { get; set; }

    }
}
