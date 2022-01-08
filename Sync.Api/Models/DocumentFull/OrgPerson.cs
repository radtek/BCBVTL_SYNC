using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace Sync.Api.Models.DocumentFull
{
    public class OrgPerson
    {
        [XmlElement(ElementName = "PERSONCODE")]
        public string personCode { get; set; }
        [XmlElement(ElementName = "PERSONORGCODE")]
        public string personOrgCode { get; set; }
        [XmlElement(ElementName = "REFID")]
        public string refId { get; set; }
        [XmlElement(ElementName = "NAME")]
        public string name { get; set; }
        [XmlElement(ElementName = "SEARCHNAME")]
        public string searchName { get; set; }
        [XmlElement(ElementName = "GENDER")]
        public string gender { get; set; }
        [XmlElement(ElementName = "DATEOFBIRTH")]
        public string dateOfBirth { get; set; }
        [XmlElement(ElementName = "PLACEOFBIRTHCODE")]
        public string placeOfBirthCode { get; set; }
        [XmlElement(ElementName = "PLACEOFBIRTHNAME")]
        public string placeOfBirthName { get; set; }
        [XmlElement(ElementName = "IDNUMBER")]
        public string idNumber { get; set; }
        [XmlElement(ElementName = "DATEOFIDISSUE")]
        public string dateOfIdIssue { get; set; }
        [XmlElement(ElementName = "PLACEOFIDISSUENAME")]
        public string placeOfIdIssueName { get; set; }
        [XmlElement(ElementName = "ETHNIC")]
        public string ethNic { get; set; }
        [XmlElement(ElementName = "RELIGION")]
        public string religion { get; set; }
        [XmlElement(ElementName = "ETHNICCODE")]
        public string ethnicCode { get; set; }
        [XmlElement(ElementName = "RELIGIONCODE")]
        public string religionCode { get; set; }
        [XmlElement(ElementName = "NATIONALITYNAME")]
        public string nationalityName { get; set; }
        [XmlElement(ElementName = "NATIONALITYCODE")]
        public string nationalityCode { get; set; }
        [XmlElement(ElementName = "FATHERNAME")]
        public string fatherName { get; set; }
        [XmlElement(ElementName = "FATHERSEARCHNAME")]
        public string fatherSearchName { get; set; }
        [XmlElement(ElementName = "MOTHERNAME")]
        public string motherName { get; set; }
        [XmlElement(ElementName = "MOTHERSEARCHNAME")]
        public string motherSearchName { get; set; }
        [XmlElement(ElementName = "CREATEDBY")]
        public string createdBy { get; set; }
        [XmlElement(ElementName = "CREATEDDATE")]
        public string createdDate { get; set; }
        [XmlElement(ElementName = "UPDATEDBY")]
        public string updatedBy { get; set; }
        [XmlElement(ElementName = "UPDATEDDATE")]
        public string updatedDate { get; set; }
        [XmlElement(ElementName = "ISCHECKED")]
        public string isChecked { get; set; }
        [XmlElement(ElementName = "DESCRIPTION")]
        public string description { get; set; }
        [XmlElement(ElementName = "SRCOFFICE")]
        public string srcOffice { get; set; }
        [XmlElement(ElementName = "STATUS")]
        public string status { get; set; }
        [XmlElement(ElementName = "CREATEDBYNAME")]
        public string createdByName { get; set; }
        [XmlElement(ElementName = "UPDATEDBYNAME")]
        public string updatedByName { get; set; }

        [XmlElement(ElementName = "OTHERNAME")]
        public string otherName { get; set; }
        [XmlElement(ElementName = "COUNTRYOFBIRTH")]
        public string countryOfBirth { get; set; }
    }
}