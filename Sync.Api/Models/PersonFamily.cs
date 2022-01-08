using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace Sync.Api.Models
{
    public class PersonFamily
    {
        [XmlElement(ElementName = "NAME")]
        public string name { get; set; }
        [XmlElement(ElementName = "GENDER")]
        public string gender { get; set; }
        [XmlElement(ElementName = "DATEOFBIRTH")]
        public string dateOfBirth { get; set; }
        [XmlElement(ElementName = "TYPEDOB")]
        public string typeDob { get; set; }
        [XmlElement(ElementName = "PLACEOFBIRTH")]
        public string placeOfBirth { get; set; }
        [XmlElement(ElementName = "RELATIONSHIP")]
        public string relationship { get; set; }
        [XmlElement(ElementName = "PHOTO")]
        public string photo { get; set; }
        [XmlElement(ElementName = "LOST")]
        public string lost { get; set; }
    }
}