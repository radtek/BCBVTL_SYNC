using Sync.Api.Models.DocumentFull;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace Sync.Api.Models
{
    public class SyncDocumentListAModel
    {
        public string code { get; set; }
        public string message { get; set; }
        public List<HandoverA> data { get; set; }
    }

    public class HandoverA
    {
        [XmlElement(ElementName = "PACKAGEID")]
        public string packageId { get; set; }
        [XmlElement(ElementName = "OFFERUSERNAME")]
        public string offerUserName { get; set; }
        [XmlElement(ElementName = "OFFERDATE")]
        public string offerDate { get; set; }
        [XmlElement(ElementName = "APPROVEUSERNAME")]
        public string approveUserName { get; set; }
        [XmlElement(ElementName = "APPROVEDATE")]
        public string approveDate { get; set; }
        [XmlElement(ElementName = "SITECODE")]
        public string siteCode { get; set; }
        [XmlElement(ElementName = "TYPE")]
        public string type { get; set; }
        [XmlElement(ElementName = "COUNT")]
        public int? count { get; set; }
        public List<ReceiptManager> receipts { get; set; }
        [XmlElement(ElementName = "IDQUEUE")]
        public long idQueue { get; set; }
        [XmlElement(ElementName = "PROPOSALNAME")]
        public string proposalName { get; set; }
        [XmlElement(ElementName = "APPROVENAME")]
        public string approveName { get; set; }
        [XmlElement(ElementName = "APPROVEPOSITION")]
        public string approvePosition { get; set; }
        [XmlElement(ElementName = "CREATORNAME")]
        public string creatorName { get; set; }
        [XmlElement(ElementName = "UPDATEDATE")]
        public string updateDate { get; set; }
        [XmlElement(ElementName = "UPDATEBY")]
        public string updateBy { get; set; }
        [XmlElement(ElementName = "UPDATEBYNAME")]
        public string updateByName { get; set; }
        [XmlElement(ElementName = "DATEOFDELIVERY")]
        public string dateOfDelivery { get; set; }
        [XmlElement(ElementName = "PLACEOFDELIVERY")]
        public string placeOfDelivery { get; set; }
    }

    public class ReceiptManager
    {
        [XmlElement(ElementName = "RECEIPTNO")]
        public string receiptNo { get; set; }
        [XmlElement(ElementName = "OFFICENAME")]
        public string officeName { get; set; }
        [XmlElement(ElementName = "NAME")]
        public string name { get; set; }
        [XmlElement(ElementName = "DOB")]
        public string dob { get; set; }
        [XmlElement(ElementName = "ADDRESS")]
        public string address { get; set; }
        [XmlElement(ElementName = "NIN")]
        public string nin { get; set; }
        [XmlElement(ElementName = "PHONE")]
        public string phone { get; set; }
        [XmlElement(ElementName = "PAYMENTAMOUNT")]
        public decimal? paymentAmount { get; set; }
        [XmlElement(ElementName = "PAYMENTFLAG")]
        public string paymentFlag { get; set; }
        public List<DetailHandover> handovers { get; set; }
        public List<ReceiptBill> bills { get; set; }
        [XmlElement(ElementName = "REGSITECODE")]
        public string regSiteCode { get; set; }
        [XmlElement(ElementName = "DATEOFISSUE")]
        public string dateOfIssue { get; set; }
        [XmlElement(ElementName = "DATEINWEEK")]
        public string dateInWeek { get; set; }
        [XmlElement(ElementName = "NOTE")]
        public string note { get; set; }
        [XmlElement(ElementName = "PLACEOFRECIEPT")]
        public string placeOfReciept { get; set; }
        [XmlElement(ElementName = "DELIVERYATOFFICE")]
        public string deliveryAtOffice { get; set; }
        [XmlElement(ElementName = "DELIVERYOFFICE")]
        public string deliveryOffice { get; set; }
        [XmlElement(ElementName = "AMOUNTOFPASSPORT")]
        public string amountOfPassport { get; set; }
        [XmlElement(ElementName = "AMOUNTOFREGISTRATION")]
        public string amountOfRegistration { get; set; }
        [XmlElement(ElementName = "AMOUNTOFPERSON")]
        public string amountOfPerson { get; set; }
        [XmlElement(ElementName = "AMOUNTOFIMAGE")]
        public string amountOfImage { get; set; }
        [XmlElement(ElementName = "DOCUMENTTYPE")]
        public string documentType { get; set; }
        [XmlElement(ElementName = "PREVPASSPORTNO")]
        public string prevPassportNo { get; set; }
        [XmlElement(ElementName = "ADDEDDOCUMENTS")]
        public string addedDocuments { get; set; }
        [XmlElement(ElementName = "DOCUMENTARYNO")]
        public string documentaryNo { get; set; }
        [XmlElement(ElementName = "DOCUMENTARYISSUED")]
        public string documentaryIssued { get; set; }
        [XmlElement(ElementName = "STATUS")]
        public string status { get; set; }
        [XmlElement(ElementName = "ISDELIVERED")]
        public string isDelivered { get; set; }
        [XmlElement(ElementName = "ISPOSTOFFICE")]
        public string isPostOffice { get; set; }
        [XmlElement(ElementName = "NOTEOFDELIVERY")]
        public string noteOfDelivery { get; set; }
        [XmlElement(ElementName = "SIGNNAME")]
        public string signName { get; set; }
        [XmlElement(ElementName = "DOCOFDELIVERY")]
        public string docOfDelivery { get; set; }
        [XmlElement(ElementName = "DOCUMENTARYOFFICE")]
        public string documentaryOffice { get; set; }
        [XmlElement(ElementName = "DOCUMENTARYADDRESS")]
        public string documentaryAddress { get; set; }
        [XmlElement(ElementName = "LISTCODE")]
        public string listCode { get; set; }
        [XmlElement(ElementName = "INPUTCOMPLETED")]
        public string inputCompleted { get; set; }
        [XmlElement(ElementName = "DELETEDDATE")]
        public string deletedDate { get; set; }
        [XmlElement(ElementName = "DELETEDBY")]
        public string deletedBy { get; set; }
        [XmlElement(ElementName = "DELETEDNAME")]
        public string deletedName { get; set; }
        [XmlElement(ElementName = "DELETEDREASON")]
        public string deletedReason { get; set; }
        [XmlElement(ElementName = "CREATEBY")]
        public string createBy { get; set; }
        [XmlElement(ElementName = "CREATEBYNAME")]
        public string createByName { get; set; }
        [XmlElement(ElementName = "RECEIVEDBY")]
        public string receivedBy { get; set; }
        [XmlElement(ElementName = "CREATEDATE")]
        public string createDate { get; set; }
        public List<FeeRecieptPayment> feeRecieptPayment { get; set; }
    }

    //public class OrgPerson
    //{
    //    [XmlElement(ElementName = "PERSONCODE")]
    //    public string personCode { get; set; }
    //    [XmlElement(ElementName = "PERSONORGCODE")]
    //    public string personOrgCode { get; set; }
    //    [XmlElement(ElementName = "REFID")]
    //    public string refId { get; set; }
    //    [XmlElement(ElementName = "NAME")]
    //    public string name { get; set; }
    //    [XmlElement(ElementName = "SEARCHNAME")]
    //    public string searchName { get; set; }
    //    [XmlElement(ElementName = "GENDER")]
    //    public string gender { get; set; }
    //    [XmlElement(ElementName = "DATEOFBIRTH")]
    //    public string dateOfBirth { get; set; }
    //    [XmlElement(ElementName = "PLACEOFBIRTHCODE")]
    //    public string placeOfBirthCode { get; set; }
    //    [XmlElement(ElementName = "PLACEOFBIRTHNAME")]
    //    public string placeOfBirthName { get; set; }
    //    [XmlElement(ElementName = "IDNUMBER")]
    //    public string idNumber { get; set; }
    //    [XmlElement(ElementName = "DATEOFIDISSUE")]
    //    public string dateOfIdIssue { get; set; }
    //    [XmlElement(ElementName = "PLACEOFIDISSUENAME")]
    //    public string placeOfIdIssueName { get; set; }
    //    [XmlElement(ElementName = "ETHNIC")]
    //    public string ethNic { get; set; }
    //    [XmlElement(ElementName = "RELIGION")]
    //    public string religion { get; set; }
    //    [XmlElement(ElementName = "ETHNICCODE")]
    //    public string ethnicCode { get; set; }
    //    [XmlElement(ElementName = "RELIGIONCODE")]
    //    public string religionCode { get; set; }
    //    [XmlElement(ElementName = "NATIONALITYNAME")]
    //    public string nationalityName { get; set; }
    //    [XmlElement(ElementName = "NATIONALITYCODE")]
    //    public string nationalityCode { get; set; }
    //    [XmlElement(ElementName = "FATHERNAME")]
    //    public string fatherName { get; set; }
    //    [XmlElement(ElementName = "FATHERSEARCHNAME")]
    //    public string fatherSearchName { get; set; }
    //    [XmlElement(ElementName = "MOTHERNAME")]
    //    public string motherName { get; set; }
    //    [XmlElement(ElementName = "MOTHERSEARCHNAME")]
    //    public string motherSearchName { get; set; }
    //    [XmlElement(ElementName = "CREATEDBY")]
    //    public string createdBy { get; set; }
    //    [XmlElement(ElementName = "CREATEDDATE")]
    //    public string createdDate { get; set; }
    //    [XmlElement(ElementName = "UPDATEDBY")]
    //    public string updatedBy { get; set; }
    //    [XmlElement(ElementName = "UPDATEDDATE")]
    //    public string updatedDate { get; set; }
    //    [XmlElement(ElementName = "ISCHECKED")]
    //    public string isChecked { get; set; }
    //    [XmlElement(ElementName = "DESCRIPTION")]
    //    public string description { get; set; }
    //    [XmlElement(ElementName = "SRCOFFICE")]
    //    public string srcOffice { get; set; }
    //    [XmlElement(ElementName = "STATUS")]
    //    public string status { get; set; }
    //    [XmlElement(ElementName = "CREATEDBYNAME")]
    //    public string createdByName { get; set; }
    //    [XmlElement(ElementName = "UPDATEDBYNAME")]
    //    public string updatedByName { get; set; }
    //    [XmlElement(ElementName = "COUNTRYOFBIRTH")]
    //    public string countryOfBirth { get; set; }
    //    [XmlElement(ElementName = "OTHERNAME")]
    //    public string otherName { get; set; }
    //}

    public class ReceiptBill
    {
        [XmlElement(ElementName = "RECEIPTNO")]
        public string receiptNo { get; set; }
        [XmlElement(ElementName = "CODE")]
        public string code { get; set; }
        [XmlElement(ElementName = "NUMBER")]
        public string number { get; set; }
        [XmlElement(ElementName = "PRICE")]
        public decimal? price { get; set; }
        [XmlElement(ElementName = "BILLFLAG")]
        public string billFlag { get; set; }
        [XmlElement(ElementName = "DESCRIPTION")]
        public string description { get; set; }
        [XmlElement(ElementName = "CASHIERNAME")]
        public string cashierName { get; set; }
        [XmlElement(ElementName = "DATEOFRECEIPT")]
        public string dateOfReceipt { get; set; }
        [XmlElement(ElementName = "CREATEBY")]
        public string createBy { get; set; }
        [XmlElement(ElementName = "CREATEDATE")]
        public string createDate { get; set; }
        [XmlElement(ElementName = "CREATEBYNAME")]
        public string createByName { get; set; }
        [XmlElement(ElementName = "CUSTOMERNAME")]
        public string customerName { get; set; }
    }

    //public class PaymentDetail
    //{
    //    [XmlElement(ElementName = "TRANSACTIONID")]
    //    public string transactionId { get; set; }
    //    [XmlElement(ElementName = "TYPEPAYMENT")]
    //    public string typePayment { get; set; }
    //    [XmlElement(ElementName = "SUBTYPEPAYMENT")]
    //    public string subTypePayment { get; set; }
    //    [XmlElement(ElementName = "PAYMENTAMOUNT")]
    //    public string paymentAmount { get; set; }
    //    [XmlElement(ElementName = "STATUSFEE")]
    //    public string statusFee { get; set; }
    //    [XmlElement(ElementName = "NAMEPAYMENT")]
    //    public string namePayment { get; set; }

    //}

    public class FeeRecieptPayment
    {
        [XmlElement(ElementName = "RECIEPTNO")]
        public string recieptNo { get; set; }
        [XmlElement(ElementName = "TYPEPAYMENT")]
        public string typePayment { get; set; }
        [XmlElement(ElementName = "PRICE")]
        public double? price { get; set; }
        [XmlElement(ElementName = "UNIT")]
        public string unit { get; set; }
        [XmlElement(ElementName = "AMOUNT")]
        public int? amount { get; set; }
        [XmlElement(ElementName = "TOTAL")]
        public double? total { get; set; }
        [XmlElement(ElementName = "DESCRIPTION")]
        public string description { get; set; }
        [XmlElement(ElementName = "CREATEBY")]
        public string createBy { get; set; }

    }
}