using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace Sync.Api.Models
{
    public class DataPersonBuff
    {

        [XmlElement(ElementName = "TRANSACTIONMASTERID")]
        public string transactionMasterId { get; set; }

        [XmlElement(ElementName = "TRANSACTIONID")]
        public string transactionId { get; set; }

        [XmlElement(ElementName = "APXPERSONCCODE")]
        public string apxPersonCcode { get; set; }

        [XmlElement(ElementName = "MACANHAN")]
        public string maCaNhan { get; set; }

        [XmlElement(ElementName = "NAME")]
        public string name { get; set; }

        [XmlElement(ElementName = "OTHERNAME")]
        public string otherName { get; set; }

        [XmlElement(ElementName = "GENDER")]
        public string gender { get; set; }

        [XmlElement(ElementName = "DATEOFBIRTH")]
        public string dateOfBirth { get; set; }

        [XmlElement(ElementName = "PLACEOFBIRTHNAME")]
        public string placeOfBirthName { get; set; }

        [XmlElement(ElementName = "IDNUMBER")]
        public string idNumber { get; set; }

        [XmlElement(ElementName = "ETHNIC")]
        public string ethNic { get; set; }

        [XmlElement(ElementName = "RELIGION")]
        public string religion { get; set; }

        [XmlElement(ElementName = "SEARCHNAME")]
        public string searchName { get; set; }

        [XmlElement(ElementName = "NATIONALITYNAME")]
        public string nationalityName { get; set; }

        [XmlElement(ElementName = "RESIDENTPLACENAME")]
        public string residentPlaceName { get; set; }

        [XmlElement(ElementName = "RESIDENTADDRESS")]
        public string residentAddress { get; set; }

        [XmlElement(ElementName = "TEMPADDRESS")]
        public string tempAddress { get; set; }

        [XmlElement(ElementName = "OCCUPATION")]
        public string occupation { get; set; }

        [XmlElement(ElementName = "OFFICEINFO")]
        public string officeInfo { get; set; }

        [XmlElement(ElementName = "FATHERNAME")]
        public string fatherName { get; set; }

        [XmlElement(ElementName = "FATHERNATIONALITY")]
        public string fatherNationality { get; set; }

        [XmlElement(ElementName = "FATHEROCCUPATION")]
        public string fatherOccupation { get; set; }

        [XmlElement(ElementName = "MOTHERNAME")]
        public string motherName { get; set; }

        [XmlElement(ElementName = "MOTHERNATIONALITY")]
        public string motherNationality { get; set; }

        [XmlElement(ElementName = "MOTHEROCCUPATION")]
        public string motherOccupation { get; set; }

        [XmlElement(ElementName = "PASSPORTNO")]
        public string passportNo { get; set; }

        [XmlElement(ElementName = "MATCHPOINT")]
        public double? matchPoint { get; set; }

        [XmlElement(ElementName = "SEARCHTS")]
        public string searchTs { get; set; }

        [XmlElement(ElementName = "SRC")]
        public string src { get; set; }



        /// <summary>
        /// •	HC: Lịch sử cấp phát hộ chiếu 
        /// •	LSXNC: Lịch sử XNC
        /// •	HSVP: HSVP
        /// •	GPXNC: Giấy phép XNC
        /// •	ABTC: ABTC
        /// •	TLQT: Trở lại quốc tịch
        /// •	TQT: Thôi quốc tịch
        /// •	NTL: Nhận trở lại
        /// •	XMNS: Xác minh nhân sự
        /// •	VKHH: Việt kiểu hồi hương
        /// cách nhau bởi dấu phẩy
        /// </summary>
        [XmlElement(ElementName = "DATATYPE")]
        public string dataType { get; set; }

        [XmlElement(ElementName = "ORGPERSON")]
        public string orgPerson { get; set; }

        [XmlElement(ElementName = "PLACEOFBIRTHCODE")]
        public string placeOfBirthCode { get; set; }

        [XmlElement(ElementName = "NATIONALITYCODE")]
        public string nationalityCode { get; set; }

        [XmlElement(ElementName = "RESIDENTPLACECODE")]
        public string residentPlaceCode { get; set; }

        public List<PassportStatus> listPassportStatus { get; set; }
    }
}