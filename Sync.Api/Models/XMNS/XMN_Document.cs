using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Sync.Api.Models.XMNS
{
    public class XMN_Document
    {
        [XmlElement(ElementName = "CODE")]
        public string code { get; set; }

        [XmlElement(ElementName = "STATUS")]
        public string status { get; set; }

        [XmlElement(ElementName = "PERSONCODE")]
        public string personCode { get; set; }

        [XmlElement(ElementName = "NAME_")]
        public string name { get; set; }

        [XmlElement(ElementName = "GENDER")]
        public string gender { get; set; }

        [XmlElement(ElementName = "DATEOFBIRTH")]
        public string dateOfBirth { get; set; }

        [XmlElement(ElementName = "PASSPORTNO")]
        public string passportNo { get; set; }

        [XmlElement(ElementName = "PASSPORTDOI")]
        public string passportDoi { get; set; }

        [XmlElement(ElementName = "PASSPORTISSUERCODE")]
        public string passportIssuerCode { get; set; }

        [XmlElement(ElementName = "PLACEOFBIRTHCODE")]
        public string placeOfBirthCode { get; set; }

        [XmlElement(ElementName = "RESIDENTCOUNTRYCODE")]
        public string residentCountryCode { get; set; }

        [XmlElement(ElementName = "PERMNTRESPLACECODE")]
        public string permntResPlaceCode { get; set; }

        [XmlElement(ElementName = "PERMNTRESAREACODE")]
        public string permntResAreaCode { get; set; }

        [XmlElement(ElementName = "PERMNTRESADDRESS")]
        public string permntResAddress { get; set; }

        [XmlElement(ElementName = "EXPORTREASONCODE")]
        public string exportReasonCode { get; set; }

        [XmlElement(ElementName = "TYPE_")]
        public string type { get; set; }

        [XmlElement(ElementName = "REASON")]
        public string reason { get; set; }

        [XmlElement(ElementName = "CREATEBY")]
        public string createBy { get; set; }

        [XmlElement(ElementName = "CREATEBYNAME")]
        public string createByName { get; set; }

        [XmlElement(ElementName = "CREATEDATETIME")]
        public string createDatetime { get; set; }

        [XmlElement(ElementName = "UPDATEBY")]
        public string updateBy { get; set; }

        [XmlElement(ElementName = "UPDATEBYNAME")]
        public string updateByName { get; set; }

        [XmlElement(ElementName = "UPDATEDATETIME")]
        public string updateDatetime { get; set; }

        [XmlElement(ElementName = "SITECODE")]
        public string siteCode { get; set; }

        [XmlElement(ElementName = "REQUESTTYPE")]
        public string requestType { get; set; }

        [XmlElement(ElementName = "XNCDOCUMENTTYPE")]
        public string xncDocumentType { get; set; }

        [XmlElement(ElementName = "VERIFYRESULT")]
        public string verifyResult { get; set; }

        [XmlElement(ElementName = "ARCHIVECODE")]
        public string archiveCode { get; set; }

        [XmlElement(ElementName = "DESCRIPTION")]
        public string description { get; set; }

        [XmlElement(ElementName = "APPROVERRESULT")]
        public string approverResult { get; set; }

        public List<XMN_Document_Att> dataList { get; set; }
        public List<XMN_Document_Family> family { get; set; }

    }
}
