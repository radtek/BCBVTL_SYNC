using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace Sync.Api.Models
{
    public class TransactionDocument
    {
        [XmlElement(ElementName = "DOCTYPE")]
        public string docType { get; set; }
        [XmlElement(ElementName = "SERIALNO")]
        public string serialNo { get; set; }
        [XmlElement(ElementName = "DOCDATA")]
        public string docData { get; set; }
        [XmlElement(ElementName = "FILENAME")]
        public string fileName { get; set; }
        [XmlElement(ElementName = "CREATEDDATE")]
        public string createdDate { get; set; }
        [XmlElement(ElementName = "CREATEDBY")]
        public string createdBy { get; set; }
        [XmlElement(ElementName = "CREATEDBYNAME")]
        public string createdByName { get; set; }
        [XmlElement(ElementName = "UPDATEDDATE")]
        public string updatedDate { get; set; }
        [XmlElement(ElementName = "UPDATEDBY")]
        public string updatedBy { get; set; }
        [XmlElement(ElementName = "UPDATEDBYNAME")]
        public string updatedByName { get; set; }

        [XmlElement(ElementName = "NOTE")]
        public string note { get; set; }

        [XmlElement(ElementName = "QUALITY")]
        public int quality { get; set; }
    }
}