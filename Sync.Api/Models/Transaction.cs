using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace Sync.Api.Models
{
    public class Transaction
    {
        [XmlElement(ElementName = "TRANSACTIONID")]
        public string transactionId { get; set; }
        [XmlElement(ElementName = "NIN")]
        public string nin { get; set; }
        [XmlElement(ElementName = "DATEOFAPPLICATION")]
        public string dateOfApplication { get; set; }
        [XmlElement(ElementName = "ESTDATEOFCOLLECTION")]
        public string estDateOfCollection { get; set; }
        [XmlElement(ElementName = "PASSPORTTYPE")]
        public string passportType { get; set; }
        [XmlElement(ElementName = "PRIORITY")]
        public int? priority { get; set; }
        [XmlElement(ElementName = "REGSITECODE")]
        public string regSiteCode { get; set; }
        [XmlElement(ElementName = "ISSSITECODE")]
        public string issSiteCode { get; set; }
        [XmlElement(ElementName = "TRANSACTIONTYPE")]
        public string transactionType { get; set; }
        [XmlElement(ElementName = "TRANSACTIONSTATUS")]
        public string transactionStatus { get; set; }
        [XmlElement(ElementName = "CHECKSUM")]
        public string checksum { get; set; }
        [XmlElement(ElementName = "ISPOSTOFFICE")]
        public string isPostOffice { get; set; }
        [XmlElement(ElementName = "RECIEPTNO")]
        public string recieptNo { get; set; }
        [XmlElement(ElementName = "REGISTRATIONNO")]
        public string registrationNo { get; set; }
        [XmlElement(ElementName = "PASSPORTSTYLE")]
        public string passportStyle { get; set; }
        [XmlElement(ElementName = "PREVPASSPORTNO")]
        public string prevPassportNo { get; set; }
        [XmlElement(ElementName = "PREVDATEOFISSUE")]
        public string prevDateOfIssue { get; set; }
        [XmlElement(ElementName = "IDQUEUE")]
        public long? idQueue { get; set; }
        [XmlElement(ElementName = "PLACEISSUANCE")]
        public string placeIssuance { get; set; }
        [XmlElement(ElementName = "PREVDATEOFEXPR")]
        public string prevDateOfExpr { get; set; }
        [XmlElement(ElementName = "APPOINTMENTPLACE")]
        public string appointmentPlace { get; set; }
        [XmlElement(ElementName = "APPLICANT")]
        public string applicant { get; set; }
        [XmlElement(ElementName = "REGISTRATIONTYPE")]
        public string registrationType { get; set; }
        [XmlElement(ElementName = "PABLACKLISTID")]
        public long? paBlacklistId { get; set; }
        [XmlElement(ElementName = "PAINQBLLUSER")]
        public string paInqBllUser { get; set; }
        [XmlElement(ElementName = "PAARCHIVECODE")]
        public string paArchiveCode { get; set; }
        [XmlElement(ElementName = "PASEARCHBIO")]
        public string paSearchBio { get; set; }
        [XmlElement(ElementName = "DESCRIPTION")]
        public string description { get; set; }
        [XmlElement(ElementName = "PAJOINPERSONDATE")]
        public string paJoinPersonDate { get; set; }
        [XmlElement(ElementName = "PASEARCHOBJDATE")]
        public string paSearchObjDate { get; set; }
        [XmlElement(ElementName = "PASAVEDOCDATE")]
        public string paSaveDocDate { get; set; }
        [XmlElement(ElementName = "NOTE")]
        public string note { get; set; }
        public RegistrationData regisData { get; set; }
        public List<TransactionDocument> documents { get; set; }
        public List<PersonAttachment>  personAtts { get; set; }
        public List<PersonFamily> families { get; set; }
    }
}