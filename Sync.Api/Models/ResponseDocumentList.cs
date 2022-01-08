using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace Sync.Api.Models
{
    public class documentList
    {
        [XmlElement(ElementName = "HANDOVERID")]
        public string handoverId { get; set; }

        [XmlElement(ElementName = "CREATEDATE")]
        public string createDate { get; set; }

        [XmlElement(ElementName = "CREATEBY")]
        public string createBy { get; set; }

        [XmlElement(ElementName = "AMOUNTDOC")]
        public int amountDoc { get; set; }
        public long idQueue { get; set; }
        [XmlElement(ElementName = "SITECODE")]
        public string siteCode { get; set; }

        public List<passportInfo> listPassportInfo { get; set; }
    }

    public class passportInfo
    {
        // ID hồ sơ
        [XmlElement(ElementName = "TRANSACTIONID")]
        public string transactionId { get; set; }

        // Họ tên đầy đủ
        [XmlElement(ElementName = "FULLNAME")]
        public string fullName { get; set; }

        //    CMTND
        [XmlElement(ElementName = "PID")]
        public string pid { get; set; }

        //     Ngày sinh (yyyy-mm-dd)
        [XmlElement(ElementName = "DOB")]
        public string dob { get; set; }

        //  Giới tính: 
        //	M: Nam
        //F: Nữ   
        [XmlElement(ElementName = "GENDER")]
        public string gender { get; set; }

        //    ĐỊa chỉ
        [XmlElement(ElementName = "ADDRESS")]
        public string address { get; set; }

        //    Địa chỉ full
        [XmlElement(ElementName = "DETAILADDRESS")]
        public string detailaddress { get; set; }

        //    Ngày hẹn trả
        [XmlElement(ElementName = "ESTOFRECIEVE")]
        public string estOfRecieve { get; set; }

        //    Tên nơi kết quả
        [XmlElement(ElementName = "NAMEPLACEOFISSUE")]
        public string namePlaceOfIssue { get; set; }

        //    Số danh sách A
        [XmlElement(ElementName = "HANDOVERA")]
        public string handoverA { get; set; }

        //    Số danh sách A
        [XmlElement(ElementName = "REGSITECOTE")]
        public string regSiteCode { get; set; }

        //    Nơi sinh
        [XmlElement(ElementName = "PLACEOFBIRTH")]
        public string placeOfBirth { get; set; }

        //    Loại hộ chiếu:
        //	P: Hộ chiếu phổ thông
        [XmlElement(ElementName = "PASSPORTTYPE")]
        public string passportType { get; set; }

        [XmlElement(ElementName = "PLACEOFISSUE")]
        //   Mã Trung tâm đăng ký
        public string placeOfIssue { get; set; }


        //   Mã trung tâm Cá thể hóa sẽ in
        [XmlElement(ElementName = "PLACEPERSOID")]
        public long? placePersoId { get; set; }

        //    Ảnh mặt  
        [XmlElement(ElementName = "PICTURE")]
        public string picture { get; set; }

        //    Mã quốc gia      
        [XmlElement(ElementName = "COUNTRYCODE")]
        public string countryCode { get; set; }

        //    Quốc tịch  
        [XmlElement(ElementName = "NATIONALITY")]
        public string nationality { get; set; }

        //    Ngày hết hạn   
        [XmlElement(ElementName = "DATEOFEXPIRY")]
        public string dateOfExpiry { get; set; }

        //      Ngày phát hành  
        [XmlElement(ElementName = "DATEOFISSUE")]
        public string dateOfIssue { get; set; }

        //      Ngày phát hành  
        [XmlElement(ElementName = "STYLEPASSPORT")]
        public string stylePassport { get; set; }

        //     Số biên nhận
        [XmlElement(ElementName = "RECEIPTNO")]
        public string receiptNo { get; set; }

        // nội dung đề nghị
        [XmlElement(ElementName = "DOCTYPE")]
        public string docType { get; set; }

        // nội dung đề nghị
        [XmlElement(ElementName = "ISSSITECODE")]
        public string issSiteCode { get; set; }

        /// <summary>
        /// trẻ em đi kèm
        /// </summary>
        public epppersonsdto epp_persons { get; set; }

        public List<PrevPassport> prevPassport { get; set; }

        //Ngày ký quyết định
        [XmlElement(ElementName = "DAYDECISION")]
        public string dayDecision { get; set; }

        //Số quyết định
        [XmlElement(ElementName = "NUMDECISION")]
        public string numDecision { get; set; }

        // Cơ quan chủ quản
        [XmlElement(ElementName = "NAMEDEPARTMENT")]
        public string nameDepartment { get; set; }

        //Địa chỉ cơ quan
        [XmlElement(ElementName = "ADDRESSDEPARTMENT")]
        public string addressDepartment { get; set; }

        //Số công văn
        [XmlElement(ElementName = "NUMDISPATCH")]
        public string numDispatch { get; set; }

        //Ngày công văn
        [XmlElement(ElementName = "DAYDISPATCH")]
        public string dayDispatch { get; set; }

        //nước được miễn thị thực
        [XmlElement(ElementName = "ISSUECOUNTRYCODE_AB")]
        public string issueCountryCodeTemAB { get; set; }

        //ngày hiệu lực
        [XmlElement(ElementName = "DATEOFISSUE_AB")]
        public string dateOfIssueTemAB { get; set; }

        //ngày hết hạn
        [XmlElement(ElementName = "DATEOFEXPIRY_AB")]
        public string dateOfExpiryTemAB { get; set; }

    }

    public class epppersonsdto
    {
        public List<epppersondto> epp_person { get; set; }
    }

    public class epppersondto
    {
        // ID person
        [XmlElement(ElementName = "ID")]
        public string id { get; set; }

        // Họ tên đầy đủ
        [XmlElement(ElementName = "NAME")]
        public string name { get; set; }

        //   Họ tên tìm kiếm 
        [XmlElement(ElementName = "SEARCHNAME")]
        public string searchName { get; set; }

        //    Giới tính
        [XmlElement(ElementName = "GENDER")]
        public string gender { get; set; }

        // Ngày sinh   
        [XmlElement(ElementName = "DATEOFBIRTH")]
        public string dateOfBirth { get; set; }

        //   Nơi sinh 
        [XmlElement(ElementName = "PLACEOFBIRTHID")]
        public string placeOfBirthId { get; set; }

        //    Quốc tịch
        [XmlElement(ElementName = "NATIONALITY")]
        public string nationality { get; set; }

        //  Loại  
        [XmlElement(ElementName = "TYPE_")]
        public string type_ { get; set; }

        // Hình ảnh
        [XmlElement(ElementName = "PICTURE")]
        public string picture { get; set; }
    }

    public class PrevPassport
    {
        [XmlElement(ElementName = "PASSPORT_NO")]
        public string passportNo { get; set; }

        [XmlElement(ElementName = "PASSPORT_TYPE")]
        public string passportType { get; set; }

        [XmlElement(ElementName = "CHIPSERIAL_NO")]
        public string chipSerialNo { get; set; }

        [XmlElement(ElementName = "DATE_OF_ISSUE")]
        public string dateOfIssue { get; set; }

        [XmlElement(ElementName = "DATE_OF_EXPIRY")]
        public string dateOfExpiry { get; set; }

        [XmlElement(ElementName = "ICAO_LINE1")]
        public string icaoLine1 { get; set; }

        [XmlElement(ElementName = "ICAO_LINE2")]
        public string icaoLine2 { get; set; }

        [XmlElement(ElementName = "SIGNER_NAME")]
        public string signerName { get; set; }

        [XmlElement(ElementName = "SIGNER_POSITION")]
        public string signerPosition { get; set; }

        [XmlElement(ElementName = "DESCRIPTION")]
        public string description { get; set; }

        [XmlElement(ElementName = "STATUS")]
        public string status { get; set; }

        [XmlElement(ElementName = "PLACE_OF_ISSUE_ID")]
        public string placeOfIssueId { get; set; }

        [XmlElement(ElementName = "PLACE_OF_ISSUE_NAME")]
        public string placeOfIssueName { get; set; }

        [XmlElement(ElementName = "IS_PASSPORT")]
        public string isEpassport { get; set; }

        [XmlElement(ElementName = "FULL_NAME")]
        public string fullName { get; set; }

        [XmlElement(ElementName = "DATE_OF_BIRTH")]
        public string dateOfBirth { get; set; }

        [XmlElement(ElementName = "DEF_DATE_OF_BIRTH")]
        public string defDateOfBirth { get; set; }

        [XmlElement(ElementName = "GENDER")]
        public string gender { get; set; }

        [XmlElement(ElementName = "PID")]
        public string pid { get; set; }

        [XmlElement(ElementName = "PHOISERIAL_NO")]
        public string phoiSerialNo { get; set; }
    }
}