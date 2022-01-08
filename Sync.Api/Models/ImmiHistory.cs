using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace Sync.Api.Models
{
    public class ImmiHistory
    {

        [XmlElement(ElementName = "IMMIGRATION_TIME")]
        public string immigrationTime { get; set; }

        [XmlElement(ElementName = "TRANSACTION_ID")]
        public string transactionId { get; set; }

        [XmlElement(ElementName = "IMMI_TYPE")]
        public string immiType { get; set; }

        [XmlElement(ElementName = "WORKSTATION_CODE")]
        public long workstationCode { get; set; }

        [XmlElement(ElementName = "FIRST_NAME")]
        public string firstName { get; set; }

        [XmlElement(ElementName = "MIDDLE_NAME")]
        public string middleName { get; set; }

        [XmlElement(ElementName = "LAST_NAME")]
        public string lastName { get; set; }

        [XmlElement(ElementName = "FULL_NAME")]
        public string fullName { get; set; }

        [XmlElement(ElementName = "FULL_NAME_WITHOUT")]
        public string fullNameWithout { get; set; }

        [XmlElement(ElementName = "PLACE_OF_BIRTH_CODE")]
        public string placeOfBirthCode { get; set; }

        [XmlElement(ElementName = "IDENTITY_CARD_NO")]
        public string identityCardNo { get; set; }

        [XmlElement(ElementName = "DATE_OF_BIRTH")]
        public string dateOfBirth { get; set; }

        [XmlElement(ElementName = "DEF_DATE_OF_BIRTH")]
        public string defDateOfBirth { get; set; }

        [XmlElement(ElementName = "GENDER")]
        public string gender { get; set; }

        [XmlElement(ElementName = "COUNTRY_CODE")]
        public string countryCode { get; set; }

        [XmlElement(ElementName = "PASSPORT_NO")]
        public string passportNo { get; set; }

        [XmlElement(ElementName = "PASSPORT_TYPE")]
        public string passportType { get; set; }

        [XmlElement(ElementName = "PASSPORT_ISSUE_PLACE_CODE")]
        public string passportIssuePlaceCode { get; set; }

        [XmlElement(ElementName = "PASSPORT_EXPIRED_DATE")]
        public string passportExpiredDate { get; set; }

        [XmlElement(ElementName = "ICAO_LINE")]
        public string icaoLine { get; set; }

        [XmlElement(ElementName = "PERSON_CODE")]  //------------
        public string personCode { get; set; }

        [XmlElement(ElementName = "PERSON_TYPE")]
        public string personType { get; set; }

        [XmlElement(ElementName = "CA_SERIAL_NUMBER")]
        public string caSerialNumber { get; set; }

        [XmlElement(ElementName = "CA_SIGNED_DATE")]
        public string caSignedDate { get; set; }

        [XmlElement(ElementName = "CA_VALID_FROM_DATE")]
        public string caValidFromDate { get; set; }

        [XmlElement(ElementName = "CA_VALID_TO_DATE")]
        public string caValidToDate { get; set; }

        [XmlElement(ElementName = "VISA_NO")]
        public string visaNo { get; set; }

        [XmlElement(ElementName = "VISA_TYPE_CODE")]
        public string visaTypeCode { get; set; }

        [XmlElement(ElementName = "VISA_VALUE")]
        public string visaValue { get; set; }

        [XmlElement(ElementName = "VISA_SYMBOL_CODE")]
        public string visaSymbolCode { get; set; }

        [XmlElement(ElementName = "VISA_ISSUE_PLACE_CODE")]
        public string visaIssuePlaceCode { get; set; }

        [XmlElement(ElementName = "VISA_ISSUE_DATE")]
        public string visaIssueDate { get; set; }

        [XmlElement(ElementName = "FREE_VISA_ID")]
        public long? freeVisaId { get; set; }

        [XmlElement(ElementName = "RESIDENCE_UNTIL_DATE")]
        public string residenceUntilDate { get; set; }

        [XmlElement(ElementName = "FLIGHT_NO")]
        public string flightNo { get; set; }

        [XmlElement(ElementName = "PURPOSE_CODE")]
        public string purposeCode { get; set; }

        [XmlElement(ElementName = "PURPOSE_NAME")]
        public string purposeName { get; set; }

        [XmlElement(ElementName = "PREPROCESS_SKEY")]
        public long? preprocessSkey { get; set; }

        [XmlElement(ElementName = "GATE_NOTE")]
        public string gateNote { get; set; }

        [XmlElement(ElementName = "CHECK_CA_RESULT")]
        public string checkCaResult { get; set; }

        [XmlElement(ElementName = "CHECK_BLACKLIST_RESULT")]
        public string checkBlackListResult { get; set; }

        [XmlElement(ElementName = "CHECK_BLACKLIST_ID_STR")]
        public string checkBlackListIdStr { get; set; }

        [XmlElement(ElementName = "CHECK_DOCUMENT_RESULT")]
        public string checkDocumentResult { get; set; }

        [XmlElement(ElementName = "SYSTEM_CHECK_RESULT")]
        public int systemCheckResult { get; set; }

        [XmlElement(ElementName = "SUPERVISOR_FULLNAME")]
        public string supervisorFullname { get; set; }

        [XmlElement(ElementName = "SUPERVISOR_RESULT")]
        public int supervisorResult { get; set; }

        [XmlElement(ElementName = "SUPERVISOR_NOTE")]
        public string supervisorNote { get; set; }

        [XmlElement(ElementName = "ADMIN_FULLNAME")]
        public string adminFullname { get; set; }

        [XmlElement(ElementName = "ADMIN_RESULT")]
        public int adminResult { get; set; }

        [XmlElement(ElementName = "ADMIN_NOTE")]
        public string adminNote { get; set; }

        [XmlElement(ElementName = "DELETED_FLAG")]
        public string deleteFlag { get; set; }

       
        //public string syncType { get; set; }

        [XmlElement(ElementName = "CHILDRENS")]
        public List<ImmihistoryChildren> childrens { get; set; }

        [XmlElement(ElementName = "IMAGES")]
        public List<ImmihistoryImage> images { get; set; }

        [XmlElement(ElementName = "GATE_FULLNAME")]
        public string gateFullname { get; set; }

        [XmlElement(ElementName = "FLIGHT_ROUTER_CODE")]
        public string flightRouterCode { get; set; }

        [XmlElement(ElementName = "CREATED_TIME")]
        public string createdTime { get; set; }

        [XmlElement(ElementName = "CREATED_BY")]
        public string createdBy { get; set; }

        [XmlElement(ElementName = "LAST_MODIFIED_BY")]
        public string lastModifiedBy { get; set; }

        [XmlElement(ElementName = "LAST_MODIFIED_TIME")]
        public string lastModifiedTime { get; set; }

        [XmlElement(ElementName = "HISTORY_ORDER")]
        public int _HistoryOrder { get; set; }
        //public long idQueue { get; set; }

    }

    public class ImmihistoryChildren
    {
        [XmlElement(ElementName = "HISTORY_ORDER")]
        public int _HistoryOrder { get; set; }

        [XmlElement(ElementName = "FULL_NAME")]
        public string fullName { get; set; }

        [XmlElement(ElementName = "DATE_OF_BIRTH")]
        public string dateOfBirth { get; set; }

        [XmlElement(ElementName = "GENDER")]
        public string gender { get; set; }

        [XmlElement(ElementName = "FAMILY_RELATIONSHIP_CODE")]
        public string familyrelationshipCode { get; set; }

        [XmlElement(ElementName = "PLACE_OF_BIRTH_CODE")]
        public string placeOfBirthCode { get; set; }

        [XmlElement(ElementName = "ADDRESS")]
        public string address { get; set; }

        [XmlElement(ElementName = "CREATED_BY")]
        public string createdBy { get; set; }

        [XmlElement(ElementName = "CREATED_TIME")]
        public string createdTime { get; set; }

        [XmlElement(ElementName = "LAST_MODIFIED_BY")]
        public string lastModifiedBy { get; set; }

        [XmlElement(ElementName = "LAST_MODIFIED_TIME")]
        public string lastModifiedTime { get; set; }
    }

    public class ImmihistoryImage
    {
        [XmlElement(ElementName = "HISTORY_ORDER")]
        public int _HistoryOrder { get; set; }

        [XmlElement(ElementName = "TYPE")]
        public string type { get; set; }

        [XmlElement(ElementName = "HSANH_ID")]
        public string hSanhId { get; set; }

        [XmlElement(ElementName = "DATA")]
        public string data { get; set; }

        [XmlElement(ElementName = "CREATED_BY")]
        public string createdBy { get; set; }

        [XmlElement(ElementName = "CREATED_TIME")]
        public string createdTime { get; set; }

        [XmlElement(ElementName = "LAST_MODIFIED_BY")]
        public string lastModifiedBy { get; set; }

        [XmlElement(ElementName = "LAST_MODIFIED_TIME")]
        public string lastModifiedTime { get; set; }
    }
}