using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace Sync.Api.Models.SyncCategory
{
    public class ListDatas
    {
        public List<CategoryData> listCountry { get; set; }
        public List<DistrictData> listDistrict { get; set; }
        public List<DistrictData> listArea { get; set; }
        public List<PurposeData> listPurpose { get; set; }
        public List<BordergatData> listBordergate { get; set; }
        public List<OfficeData> listOffice { get; set; }
        public List<OrganizationData> listOrganization { get; set; }
        public List<FeeInfoData> listFeeInfo { get; set; }
        public List<ReligionData> listEthnic { get; set; }
        public List<ReligionData> listReligion { get; set; }
        public List<RelationshipData> listRelationship { get; set; }
        public List<IDPlaceData> listIDPlace { get; set; }
        public List<TransactionSubTypeData> listTransactionSubType { get; set; }
        public List<PassportTypeData> listPassportType { get; set; }
    }

    //QUỐC GIA
    public class CategoryData
    {
        [XmlElement(ElementName = "CODE")]
        public string code { get; set; }
        [XmlElement(ElementName = "NAME_")]
        public string name { get; set; }
        [XmlElement(ElementName = "A08ID")]
        public string a08Id { get; set; }
        [XmlElement(ElementName = "NAMEENG")]
        public string nameEng { get; set; }
        [XmlElement(ElementName = "NAMEAB")]
        public string nameAB { get; set; }
        [XmlElement(ElementName = "NAMEABENG")]
        public string nameABEng { get; set; }
        [XmlElement(ElementName = "ACTIVE")]
        public string active { get; set; }
        [XmlElement(ElementName = "IDQUEUE")]
        public long idQueue { get; set; }
        [XmlElement(ElementName = "ACTION")]
        public string action { get; set; }
        [XmlElement(ElementName = "CREATEDBY")]
        public string createdBy { get; set; }
        [XmlElement(ElementName = "CREATEDDATETIME")]
        public string createdDatetime { get; set; }
        [XmlElement(ElementName = "UPDATEDBY")]
        public string updatedBy { get; set; }
        [XmlElement(ElementName = "UPDATEDDATETIME")]
        public string updatedDatetime { get; set; }
        [XmlElement(ElementName = "SITECIDE")]
        public string siteCode { get; set; }
    }
    //TỈNH HUYỆN(District/Area)
    public class DistrictData
    {
        [XmlElement(ElementName = "CODE")]
        public string code { get; set; }
        [XmlElement(ElementName = "NAME_")]
        public string name { get; set; }
        [XmlElement(ElementName = "AO8ID")]
        public string a08Id { get; set; }
        [XmlElement(ElementName = "PARENTCODE")]
        public string parentCode { get; set; }
        [XmlElement(ElementName = "IDQUEUE")]
        public long idQueue { get; set; }
        [XmlElement(ElementName = "ACTIVE")]
        public string active { get; set; }
        [XmlElement(ElementName = "ACTION")]
        public string action { get; set; }
        [XmlElement(ElementName = "CTRATEDBY")]
        public string createdBy { get; set; }
        [XmlElement(ElementName = "CREATEDDATETIME")]
        public string createdDatetime { get; set; }
        [XmlElement(ElementName = "UPDATEBY")]
        public string updatedBy { get; set; }
        [XmlElement(ElementName = "UPDATEDATETIME")]
        public string updatedDatetime { get; set; }
        [XmlElement(ElementName = "STIECODE")]
        public string siteCode { get; set; }
    }
    //Danh mục mục đích xuất nhập cảnh
    public class PurposeData
    {
        [XmlElement(ElementName = "CODE")]
        public string code { get; set; }
        [XmlElement(ElementName = "NAME_")]
        public string name { get; set; }
        [XmlElement(ElementName = "A08ID")]
        public string a08Id { get; set; }
        [XmlElement(ElementName = "NAMEENG")]
        public string nameEng { get; set; }
        [XmlElement(ElementName = "IDQUEUE")]
        public long idQueue { get; set; }
        [XmlElement(ElementName = "ACTIVE")]
        public string active { get; set; }
        [XmlElement(ElementName = "ACTION")]
        public string action { get; set; }
        [XmlElement(ElementName = "CREATEDBY")]
        public string createdBy { get; set; }
        [XmlElement(ElementName = "CREATEDDATETIME")]
        public string createdDatetime { get; set; }
        [XmlElement(ElementName = "UPDATEDBY")]
        public string updatedBy { get; set; }
        [XmlElement(ElementName = "UPDATEEDDATETIME")]
        public string updatedDatetime { get; set; }
        [XmlElement(ElementName = "STIECODE")]
        public string siteCode { get; set; }
    }
    //danh mục cửa khẩu
    public class BordergatData
    {
        [XmlElement(ElementName = "CODE")]
        public string code { get; set; }
        [XmlElement(ElementName = "NAME_")]
        public string name { get; set; }
        [XmlElement(ElementName = "DESCRIPTION")]
        public string description { get; set; }
        [XmlElement(ElementName = "BORDERGATEKIND")]
        public string bordergateKind { get; set; }
        [XmlElement(ElementName = "IDQUEUE")]
        public long idQueue { get; set; }
        [XmlElement(ElementName = "ACTION")]
        public string action { get; set; }
        [XmlElement(ElementName = "CREATEDBY")]
        public string createdBy { get; set; }
        [XmlElement(ElementName = "CREATEDDATETIME")]
        public string createdDatetime { get; set; }
        [XmlElement(ElementName = "UPDATEDBY")]
        public string updatedBy { get; set; }
        [XmlElement(ElementName = "UPDATEEDDATETIME")]
        public string updatedDatetime { get; set; }
        [XmlElement(ElementName = "STIECODE")]
        public string siteCode { get; set; }
        [XmlElement(ElementName = "ACTIVE")]
        public string active { get; set; }
    }
    //danh mục phòng ban
    public class OfficeData
    {
        [XmlElement(ElementName = "CODE")]
        public string code { get; set; }
        [XmlElement(ElementName = "NAME_")]
        public string name { get; set; }
        [XmlElement(ElementName = "SITE_")]
        public string site { get; set; }
        [XmlElement(ElementName = "ADRESS")]
        public string address { get; set; }
        [XmlElement(ElementName = "PLACEID")]
        public string placeId { get; set; }
        [XmlElement(ElementName = "PARENTCODE")]
        public string parentCode { get; set; }
        [XmlElement(ElementName = "MANAGER")]
        public string manager { get; set; }
        [XmlElement(ElementName = "PHONE")]
        public string phone { get; set; }
        [XmlElement(ElementName = "FAX")]
        public string fax { get; set; }
        [XmlElement(ElementName = "WEBSITE")]
        public string website { get; set; }
        [XmlElement(ElementName = "EMAIL")]
        public string email { get; set; }
        [XmlElement(ElementName = "TYPE_")]
        public string type { get; set; }
        [XmlElement(ElementName = "LEVEL_")]
        public string level { get; set; }
        [XmlElement(ElementName = "QLCODE")]
        public string qlCode { get; set; }
        [XmlElement(ElementName = "ACTIVE")]
        public string active { get; set; }
        [XmlElement(ElementName = "A08ID")]
        public string a08Id { get; set; }
        [XmlElement(ElementName = "IDQUEUE")]
        public long idQueue { get; set; }
        [XmlElement(ElementName = "ACTION")]
        public string action { get; set; }
        [XmlElement(ElementName = "STIECODE")]
        public string siteCode { get; set; }
    }
    //danh mục đơn vị
    public class OrganizationData
    {
        [XmlElement(ElementName = "CODE")]
        public string code { get; set; }
        [XmlElement(ElementName = "NAME_")]
        public string name { get; set; }
        [XmlElement(ElementName = "A08ID")]
        public string a08Id { get; set; }
        [XmlElement(ElementName = "DESCRIPTION")]
        public string description { get; set; }
        [XmlElement(ElementName = "PARENTCODE")]
        public string parentCode { get; set; }
        [XmlElement(ElementName = "LEVEL_")]
        public string level { get; set; }

        [XmlElement(ElementName = "TYPE_")]
        public string type { get; set; }

        [XmlElement(ElementName = "SHORTNAME")]
        public string shortName { get; set; }
        [XmlElement(ElementName = "IDQUEUE")]
        public long idQueue { get; set; }
        [XmlElement(ElementName = "ACTION")]
        public string action { get; set; }
        [XmlElement(ElementName = "ACTIVE")]
        public string active { get; set; }
        [XmlElement(ElementName = "CREATEDBY")]
        public string createdBy { get; set; }
        [XmlElement(ElementName = "CREATEDDATETIME")]
        public string createdDatetime { get; set; }
        [XmlElement(ElementName = "UPDATEDBY")]
        public string updatedBy { get; set; }
        [XmlElement(ElementName = "UPDATEDDATETIME")]
        public string updatedDatetime { get; set; }
        [XmlElement(ElementName = "STIECODE")]
        public string siteCode { get; set; }
    }
    //danh mục phí
    public class FeeInfoData
    {
        [XmlElement(ElementName = "TRANSACTIONSUBTYPE")]
        public string transactionSubtype { get; set; }
        [XmlElement(ElementName = "FEEAMOUNT")]
        public string feeAmount { get; set; }
        [XmlElement(ElementName = "TYPEPAYMENT")]
        public string typePayment { get; set; }
        [XmlElement(ElementName = "NAME_")]
        public string name { get; set; }
        [XmlElement(ElementName = "A08ID")]
        public string a08Id { get; set; }
        [XmlElement(ElementName = "IDQUEUE")]
        public long idQueue { get; set; }
        [XmlElement(ElementName = "STIECODE")]
        public string siteCode { get; set; }
        [XmlElement(ElementName = "ACTION")]
        public string action { get; set; }

        [XmlElement(ElementName = "ACTIVE")]
        public string active { get; set; }
    }
    //danh mục phí Religion/Ethnic
    public class ReligionData
    {
        [XmlElement(ElementName = "CODE")]
        public string code { get; set; }
        [XmlElement(ElementName = "NAME_")]
        public string name { get; set; }
        [XmlElement(ElementName = "A08ID")]
        public string a08Id { get; set; }
        [XmlElement(ElementName = "IDQUEUE")]
        public long idQueue { get; set; }
        [XmlElement(ElementName = "ACTIVE")]
        public string active { get; set; }
        [XmlElement(ElementName = "ACTION")]
        public string action { get; set; }
        [XmlElement(ElementName = "CREATEDBY")]
        public string createdBy { get; set; }
        [XmlElement(ElementName = "CREATEDDATETIME")]
        public string createdDatetime { get; set; }
        [XmlElement(ElementName = "UPDATEDBY")]
        public string updatedBy { get; set; }
        [XmlElement(ElementName = "UPDATEDDATETIME")]
        public string updatedDatetime { get; set; }
        [XmlElement(ElementName = "SITECODE")]
        public string siteCode { get; set; }
        [XmlElement(ElementName = "TYPE_")]
        public string type { get; set; }
    }

    //danh mục quan hệ thân nhân
    public class RelationshipData
    {
        [XmlElement(ElementName = "CODE")]
        public string code { get; set; }
        [XmlElement(ElementName = "NAME_")]
        public string name { get; set; }
        [XmlElement(ElementName = "A08ID")]
        public string a08Id { get; set; }
        [XmlElement(ElementName = "IDQUEUE")]
        public long idQueue { get; set; }
        [XmlElement(ElementName = "ACTIVE")]
        public string active { get; set; }
        [XmlElement(ElementName = "ACTION")]
        public string action { get; set; }
        [XmlElement(ElementName = "CREATEDBY")]
        public string createdBy { get; set; }
        [XmlElement(ElementName = "CREATEDDATETIME")]
        public string createdDatetime { get; set; }
        [XmlElement(ElementName = "UPDATEDBY")]
        public string updatedBy { get; set; }
        [XmlElement(ElementName = "UPDATEDDATETIME")]
        public string updatedDatetime { get; set; }
        [XmlElement(ElementName = "SITECODE")]
        public string siteCode { get; set; }
        [XmlElement(ElementName = "TYPE_")]
        public string type { get; set; }
    }

    //danh mục nơi cấp CMND
    public class IDPlaceData
    {
        [XmlElement(ElementName = "CODE")]
        public string code { get; set; }
        [XmlElement(ElementName = "NAME_")]
        public string name { get; set; }
        [XmlElement(ElementName = "A08ID")]
        public string a08Id { get; set; }
        [XmlElement(ElementName = "IDQUEUE")]
        public long idQueue { get; set; }
        [XmlElement(ElementName = "ACTIVE")]
        public string active { get; set; }
        [XmlElement(ElementName = "ACTION")]
        public string action { get; set; }
        [XmlElement(ElementName = "CREATEDBY")]
        public string createdBy { get; set; }
        [XmlElement(ElementName = "CREATEDDATETIME")]
        public string createdDatetime { get; set; }
        [XmlElement(ElementName = "UPDATEDBY")]
        public string updatedBy { get; set; }
        [XmlElement(ElementName = "UPDATEDDATETIME")]
        public string updatedDatetime { get; set; }
        [XmlElement(ElementName = "SITECODE")]
        public string siteCode { get; set; }
        [XmlElement(ElementName = "TYPE_")]
        public string type { get; set; }
    }

    //danh mục nội dung đề nghị
    public class TransactionSubTypeData
    {
        [XmlElement(ElementName = "CODE")]
        public string code { get; set; }
        [XmlElement(ElementName = "NAME_")]
        public string name { get; set; }
        [XmlElement(ElementName = "A08ID")]
        public string a08Id { get; set; }
        [XmlElement(ElementName = "IDQUEUE")]
        public long idQueue { get; set; }
        [XmlElement(ElementName = "ACTIVE")]
        public string active { get; set; }
        [XmlElement(ElementName = "ACTION")]
        public string action { get; set; }
        [XmlElement(ElementName = "CREATEDBY")]
        public string createdBy { get; set; }
        [XmlElement(ElementName = "CREATEDDATETIME")]
        public string createdDatetime { get; set; }
        [XmlElement(ElementName = "UPDATEDBY")]
        public string updatedBy { get; set; }
        [XmlElement(ElementName = "UPDATEDDATETIME")]
        public string updatedDatetime { get; set; }
        [XmlElement(ElementName = "SITECODE")]
        public string siteCode { get; set; }
        [XmlElement(ElementName = "TYPE_")]
        public string type { get; set; }
    }

    //danh mục loại hộ chiếu
    public class PassportTypeData
    {
        [XmlElement(ElementName = "CODE")]
        public string code { get; set; }
        [XmlElement(ElementName = "NAME_")]
        public string name { get; set; }
        [XmlElement(ElementName = "A08ID")]
        public string a08Id { get; set; }
        [XmlElement(ElementName = "IDQUEUE")]
        public long idQueue { get; set; }
        [XmlElement(ElementName = "ACTIVE")]
        public string active { get; set; }
        [XmlElement(ElementName = "ACTION")]
        public string action { get; set; }
        [XmlElement(ElementName = "CREATEDBY")]
        public string createdBy { get; set; }
        [XmlElement(ElementName = "CREATEDDATETIME")]
        public string createdDatetime { get; set; }
        [XmlElement(ElementName = "UPDATEDBY")]
        public string updatedBy { get; set; }
        [XmlElement(ElementName = "UPDATEDDATETIME")]
        public string updatedDatetime { get; set; }
        [XmlElement(ElementName = "SITECODE")]
        public string siteCode { get; set; }
        [XmlElement(ElementName = "TYPE_")]
        public string type { get; set; }
    }
}