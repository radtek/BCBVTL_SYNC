using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace Sync.Api.Models.DocumentFull
{
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
        public bool? paymentFlag { get; set; }
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
        public List<FreeRecieptPayment> feeRecieptPayment { get; set; }

    }
    public class FreeRecieptPayment
    {
        [XmlElement(ElementName = "RECIEPTNO")]
        public string recieptNo { get; set; }

        [XmlElement(ElementName = "TYPEPAYMENT")]
        public string typePayment { get; set; }

        [XmlElement(ElementName = "PRICE")]
        public double price { get; set; }

        [XmlElement(ElementName = "UNIT")]
        public string unit { get; set; }

        [XmlElement(ElementName = "AMOUNT")]
        public int amount { get; set; }

        [XmlElement(ElementName = "TOTAL")]
        public double total { get; set; }

        [XmlElement(ElementName = "DESCRIPTION")]
        public string description { get; set; }

        [XmlElement(ElementName = "CREATEBY")]
        public string createBy { get; set; }

        [XmlElement(ElementName = "CREATEBYNAME")]
        public string createByName { get; set; }

    }
}