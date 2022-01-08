using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace Sync.Api.Models
{
    public class PersonAttachment
    {
        [XmlElement(ElementName = "TYPE_")]
        public string TYPE_ { get; set; }

        [XmlElement(ElementName = "SERIAL_NO")]
        public string SERIAL_NO { get; set; }

        [XmlElement(ElementName = "FILE_NAME")]
        public string FILE_NAME { get; set; }

        [XmlElement(ElementName = "BASE64")]
        public string BASE64 { get; set; }

        [XmlElement(ElementName = "R_CAI_PHAI_WSQ")]
        public string R_CAI_PHAI_WSQ { get; set; }

        [XmlElement(ElementName = "R_CAI_PHAI_PNG")]
        public string R_CAI_PHAI_PNG { get; set; }

        [XmlElement(ElementName = "R_CAI_PHAI_MNU")]
        public string R_CAI_PHAI_MNU { get; set; }

        [XmlElement(ElementName = "F_CAI_PHAI_WSQ")]
        public string F_CAI_PHAI_WSQ { get; set; }

        [XmlElement(ElementName = "F_CAI_PHAI_MNU")]
        public string F_CAI_PHAI_MNU { get; set; }

        [XmlElement(ElementName = "CAI_PHAI_NOTE")]
        public string CAI_PHAI_NOTE { get; set; }

        [XmlElement(ElementName = "CAI_PHAI_IQL")]
        public int? CAI_PHAI_IQL { get; set; }

        [XmlElement(ElementName = "R_TRO_PHAI_WSQ")]
        public string R_TRO_PHAI_WSQ { get; set; }

        [XmlElement(ElementName = "R_TRO_PHAI_PNG")]
        public string R_TRO_PHAI_PNG { get; set; }

        [XmlElement(ElementName = "R_TRO_PHAI_MNU")]
        public string R_TRO_PHAI_MNU { get; set; }

        [XmlElement(ElementName = "F_TRO_PHAI_WSQ")]
        public string F_TRO_PHAI_WSQ { get; set; }

        [XmlElement(ElementName = "F_TRO_PHAI_MNU")]
        public string F_TRO_PHAI_MNU { get; set; }

        [XmlElement(ElementName = "TRO_PHAI_NOTE")]
        public string TRO_PHAI_NOTE { get; set; }

        [XmlElement(ElementName = "TRO_PHAI_IQL")]
        public int? TRO_PHAI_IQL { get; set; }

        [XmlElement(ElementName = "R_GIUA_PHAI_WSQ")]
        public string R_GIUA_PHAI_WSQ { get; set; }

        [XmlElement(ElementName = "R_GIUA_PHAI_PNG")]
        public string R_GIUA_PHAI_PNG { get; set; }

        [XmlElement(ElementName = "R_GIUA_PHAI_MNU")]
        public string R_GIUA_PHAI_MNU { get; set; }

        [XmlElement(ElementName = "F_GIUA_PHAI_WSQ")]
        public string F_GIUA_PHAI_WSQ { get; set; }

        [XmlElement(ElementName = "F_GIUA_PHAI_MNU")]
        public string F_GIUA_PHAI_MNU { get; set; }

        [XmlElement(ElementName = "GIUA_PHAI_NOTE")]
        public string GIUA_PHAI_NOTE { get; set; }

        [XmlElement(ElementName = "GIUA_PHAI_IQL")]
        public int? GIUA_PHAI_IQL { get; set; }

        [XmlElement(ElementName = "R_NHAN_PHAI_WSQ")]
        public string R_NHAN_PHAI_WSQ { get; set; }

        [XmlElement(ElementName = "R_NHAN_PHAI_PNG")]
        public string R_NHAN_PHAI_PNG { get; set; }

        [XmlElement(ElementName = "R_NHAN_PHAI_MNU")]
        public string R_NHAN_PHAI_MNU { get; set; }

        [XmlElement(ElementName = "F_NHAN_PHAI_WSQ")]
        public string F_NHAN_PHAI_WSQ { get; set; }

        [XmlElement(ElementName = "F_NHAN_PHAI_MNU")]
        public string F_NHAN_PHAI_MNU { get; set; }

        [XmlElement(ElementName = "NHAN_PHAI_NOTE")]
        public string NHAN_PHAI_NOTE { get; set; }

        [XmlElement(ElementName = "NHAN_PHAI_IQL")]
        public int? NHAN_PHAI_IQL { get; set; }

        [XmlElement(ElementName = "R_UT_PHAI_WSQ")]
        public string R_UT_PHAI_WSQ { get; set; }

        [XmlElement(ElementName = "R_UT_PHAI_PNG")]
        public string R_UT_PHAI_PNG { get; set; }

        [XmlElement(ElementName = "R_UT_PHAI_MNU")]
        public string R_UT_PHAI_MNU { get; set; }

        [XmlElement(ElementName = "F_UT_PHAI_WSQ")]
        public string F_UT_PHAI_WSQ { get; set; }

        [XmlElement(ElementName = "F_UT_PHAI_MNU")]
        public string F_UT_PHAI_MNU { get; set; }

        [XmlElement(ElementName = "UT_PHAI_NOTE")]
        public string UT_PHAI_NOTE { get; set; }

        [XmlElement(ElementName = "UT_PHAI_IQL")]
        public int? UT_PHAI_IQL { get; set; }

        [XmlElement(ElementName = "R_CAI_TRAI_WSQ")]
        public string R_CAI_TRAI_WSQ { get; set; }

        [XmlElement(ElementName = "R_CAI_TRAI_MNU")]
        public string R_CAI_TRAI_MNU { get; set; }

        [XmlElement(ElementName = "F_CAI_TRAI_WSQ")]
        public string F_CAI_TRAI_WSQ { get; set; }

        [XmlElement(ElementName = "R_CAI_TRAI_PNG")]
        public string R_CAI_TRAI_PNG { get; set; }

        [XmlElement(ElementName = "F_CAI_TRAI_MNU")]
        public string F_CAI_TRAI_MNU { get; set; }

        [XmlElement(ElementName = "CAI_TRAI_NOTE")]
        public string CAI_TRAI_NOTE { get; set; }

        [XmlElement(ElementName = "CAI_TRAI_IQL")]
        public int? CAI_TRAI_IQL { get; set; }

        [XmlElement(ElementName = "R_TRO_TRAI_WSQ")]
        public string R_TRO_TRAI_WSQ { get; set; }

        [XmlElement(ElementName = "R_TRO_TRAI_PNG")]
        public string R_TRO_TRAI_PNG { get; set; }

        [XmlElement(ElementName = "R_TRO_TRAI_MNU")]
        public string R_TRO_TRAI_MNU { get; set; }

        [XmlElement(ElementName = "F_TRO_TRAI_WSQ")]
        public string F_TRO_TRAI_WSQ { get; set; }

        [XmlElement(ElementName = "F_TRO_TRAI_MNU")]
        public string F_TRO_TRAI_MNU { get; set; }

        [XmlElement(ElementName = "TRO_TRAI_NOTE")]
        public string TRO_TRAI_NOTE { get; set; }

        [XmlElement(ElementName = "TRO_TRAI_IQL")]
        public int? TRO_TRAI_IQL { get; set; }

        [XmlElement(ElementName = "R_GIUA_TRAI_WSQ")]
        public string R_GIUA_TRAI_WSQ { get; set; }

        [XmlElement(ElementName = "R_GIUA_TRAI_PNG")]
        public string R_GIUA_TRAI_PNG { get; set; }

        [XmlElement(ElementName = "R_GIUA_TRAI_MNU")]
        public string R_GIUA_TRAI_MNU { get; set; }

        [XmlElement(ElementName = "F_GIUA_TRAI_WSQ")]
        public string F_GIUA_TRAI_WSQ { get; set; }

        [XmlElement(ElementName = "F_GIUA_TRAI_MNU")]
        public string F_GIUA_TRAI_MNU { get; set; }

        [XmlElement(ElementName = "GIUA_TRAI_NOTE")]
        public string GIUA_TRAI_NOTE { get; set; }

        [XmlElement(ElementName = "GIUA_TRAI_IQL")]
        public int? GIUA_TRAI_IQL { get; set; }

        [XmlElement(ElementName = "R_NHAN_TRAI_WSQ")]
        public string R_NHAN_TRAI_WSQ { get; set; }

        [XmlElement(ElementName = "R_NHAN_TRAI_PNG")]
        public string R_NHAN_TRAI_PNG { get; set; }

        [XmlElement(ElementName = "R_NHAN_TRAI_MNU")]
        public string R_NHAN_TRAI_MNU { get; set; }

        [XmlElement(ElementName = "F_NHAN_TRAI_WSQ")]
        public string F_NHAN_TRAI_WSQ { get; set; }

        [XmlElement(ElementName = "F_NHAN_TRAI_MNU")]
        public string F_NHAN_TRAI_MNU { get; set; }

        [XmlElement(ElementName = "NHAN_TRAI_NOTE")]
        public string NHAN_TRAI_NOTE { get; set; }

        [XmlElement(ElementName = "NHAN_TRAI_IQL")]
        public int? NHAN_TRAI_IQL { get; set; }

        [XmlElement(ElementName = "R_UT_TRAI_WSQ")]
        public string R_UT_TRAI_WSQ { get; set; }

        [XmlElement(ElementName = "R_UT_TRAI_PNG")]
        public string R_UT_TRAI_PNG { get; set; }

        [XmlElement(ElementName = "R_UT_TRAI_MNU")]
        public string R_UT_TRAI_MNU { get; set; }

        [XmlElement(ElementName = "F_UT_TRAI_WSQ")]
        public string F_UT_TRAI_WSQ { get; set; }

        [XmlElement(ElementName = "F_UT_TRAI_MNU")]
        public string F_UT_TRAI_MNU { get; set; }

        [XmlElement(ElementName = "UT_TRAI_NOTE")]
        public string UT_TRAI_NOTE { get; set; }

        [XmlElement(ElementName = "UT_TRAI_IQL")]
        public int? UT_TRAI_IQL { get; set; }
        [XmlElement(ElementName = "F_SLAP_PHAI")]
        public string F_SLAP_PHAI { get; set; }
        [XmlElement(ElementName = "F_SLAP_TRAI")]
        public string F_SLAP_TRAI { get; set; }

        [XmlElement(ElementName = "CREATEDDATE")]
        public string createdDate { get; set; }
        [XmlElement(ElementName = "CREATEDBY")]
        public string createdBy { get; set; }
        [XmlElement(ElementName = "CREATEDBYNAME")]
        public string createdByName { get; set; }
        [XmlElement(ElementName = "UPDATEDDATE")]
        public string updatedDate { get; set; }
        [XmlElement(ElementName = "UPDATEDBY")]
        public string updatedBy { get; set; }
        [XmlElement(ElementName = "UPDATEDBYNAME")]
        public string updatedByName { get; set; }
    }
}