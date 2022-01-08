using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Sync.Api.Models.XMNS
{
    public class XMN_Document_Family
    {

        [XmlElement(ElementName = "NAME_")]
        public string name { get; set; }

        [XmlElement(ElementName = "SEARCHNAME")]
        public string searchName { get; set; }

        [XmlElement(ElementName = "DATEOFBIRTH")]
        public string dateOfBirth { get; set; }

        [XmlElement(ElementName = "GENDER")]
        public string gender { get; set; }

        [XmlElement(ElementName = "ADDRESS")]
        public string address { get; set; }

        [XmlElement(ElementName = "PLACEOFBIRTHCODE")]
        public string placeOfBirthCode { get; set; }

        [XmlElement(ElementName = "PLACEOFBIRTH")]
        public string placeOfBirth { get; set; }

        [XmlElement(ElementName = "NATIONCODE")]
        public string nationCode { get; set; }

        [XmlElement(ElementName = "NATIONNAME")]
        public string nationName { get; set; }

        [XmlElement(ElementName = "NATIONALITYCODE")]
        public string nationalityCode { get; set; }

        [XmlElement(ElementName = "NATIONALITYNAME")]
        public string nationalityName { get; set; }

        [XmlElement(ElementName = "RELATIONSHIP")]
        public string relationship { get; set; }

        [XmlElement(ElementName = "RELATIONSHIPCODE")]
        public string relationshipCode { get; set; }

        [XmlElement(ElementName = "LOST")]
        public string lost { get; set; }


        [XmlElement(ElementName = "JOB")]
        public string job { get; set; }

        [XmlElement(ElementName = "CREATEDATETIME")]
        public string createDatetime { get; set; }

        [XmlElement(ElementName = "CREATEBY")]
        public string createBy { get; set; }

        [XmlElement(ElementName = "NOTE")]
        public string note { get; set; }

        [XmlElement(ElementName = "TRANSACTIONID")]
        public string transactionId { get; set; }
    }
}
