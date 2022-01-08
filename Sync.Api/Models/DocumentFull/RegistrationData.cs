using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace Sync.Api.Models.DocumentFull
{
    public class RegistrationData
    {
        [XmlElement(ElementName = "FIRSTNAME")]
        public string firstName { get; set; }
        [XmlElement(ElementName = "MIDNAME")]
        public string midName { get; set; }
        [XmlElement(ElementName = "SURNAME")]
        public string surName { get; set; }
        [XmlElement(ElementName = "FULLNAME")]
        public string fullName { get; set; }
        [XmlElement(ElementName = "SEARCHNAME")]
        public string searchName { get; set; }
        [XmlElement(ElementName = "NATIONALITY")]
        public string nationality { get; set; }
        [XmlElement(ElementName = "GENDER")]
        public string gender { get; set; }
        [XmlElement(ElementName = "PLACEOFBIRTH")]
        public string placeOfBirth { get; set; }
        [XmlElement(ElementName = "DATEOFBIRTH")]
        public string dateOfBirth { get; set; }
        [XmlElement(ElementName = "RESIDENTPLACEID")]
        public string residentPlaceId { get; set; }
        [XmlElement(ElementName = "RESIDENTAREAID")]
        public string residentAreaId { get; set; }
        [XmlElement(ElementName = "RESIDENTADDRESS")]
        public string residentAddress { get; set; }
        [XmlElement(ElementName = "TMPPLACEID")]
        public string tmpPlaceId { get; set; }
        [XmlElement(ElementName = "TMPAREAID")]
        public string tmpAreaId { get; set; }
        [XmlElement(ElementName = "TMPADDRESS")]
        public string tmpAddress { get; set; }
        [XmlElement(ElementName = "RELIGION")]
        public string religion { get; set; }
        [XmlElement(ElementName = "NATION")]
        public string nation { get; set; }
        [XmlElement(ElementName = "ADDRESSNIN")]
        public string addressNin { get; set; }
        [XmlElement(ElementName = "DATENIN")]
        public string dateNin { get; set; }
        [XmlElement(ElementName = "JOB")]
        public string job { get; set; }
        [XmlElement(ElementName = "ADDRESSCOMPANY")]
        public string addressCompany { get; set; }
        [XmlElement(ElementName = "CONTACTNO")]
        public string contactNo { get; set; }
        [XmlElement(ElementName = "TOTALFP")]
        public int? totalFp { get; set; }
        [XmlElement(ElementName = "PERSONCODE")]
        public string personCode { get; set; }
        [XmlElement(ElementName = "NUMDECISION")]
        public string numDecision { get; set; }
        [XmlElement(ElementName = "DAYDECISION")]
        public string dayDecision { get; set; }
        [XmlElement(ElementName = "NAMEDEPARTMENT")]
        public string nameDepartment { get; set; }
        [XmlElement(ElementName = "POSITION")]
        public string position { get; set; }
        [XmlElement(ElementName = "OWNERTYPE")]
        public string ownerType { get; set; }
        [XmlElement(ElementName = "CREATEBY")]
        public string createBy { get; set; }
        [XmlElement(ElementName = "CREATEBYNAME")]
        public string createByName { get; set; }
    }
}