using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace Sync.Api.Models
{
    public class ObjDetailBuffPerson
    {
        [XmlElement(ElementName = "TRANSACTIONID")]
        public string transactionId { get; set; }

        [XmlElement(ElementName = "PERSONCODE")]
        public string personCode { get; set; }

        [XmlElement(ElementName = "PHOTO")]
        public string photo { get; set; }

        [XmlElement(ElementName = "MATCHPOINTDETAI")]
        public string matchPointDetai { get; set; }

        [XmlElement(ElementName = "FP_01")]
        public string FP_01 { get; set; }

        [XmlElement(ElementName = "FP_02")]
        public string FP_02 { get; set; }

        [XmlElement(ElementName = "FP_03")]
        public string FP_03 { get; set; }

        [XmlElement(ElementName = "FP_04")]
        public string FP_04 { get; set; }

        [XmlElement(ElementName = "FP_05")]
        public string FP_05 { get; set; }

        [XmlElement(ElementName = "FP_06")]
        public string FP_06 { get; set; }

        [XmlElement(ElementName = "FP_07")]
        public string FP_07 { get; set; }

        [XmlElement(ElementName = "FP_08")]
        public string FP_08 { get; set; }

        [XmlElement(ElementName = "FP_09")]
        public string FP_09 { get; set; }

        [XmlElement(ElementName = "FP_10")]
        public string FP_10 { get; set; }
        public CountDetailInfo countDetail { get; set; }

        [XmlElement(ElementName = "IDQUEUE")]
        public long? idQueue { get; set; }
    }

    public class CountDetailInfo
    {
        [XmlElement(ElementName = "ABTCCOUNT")]
        public int? abtcCount { get; set; }

        [XmlElement(ElementName = "GIAYPHEPXNCCOUNT")]
        public int? giayPhepXNCCount { get; set; }

        [XmlElement(ElementName = "TROLAIQTCOUNT")]
        public int? troLaiQTCount { get; set; }

        [XmlElement(ElementName = "THOIQTCOUNT")]
        public int? thoiQTCount { get; set; }

        [XmlElement(ElementName = "HOIHUONGCOUNT")]
        public int? hoiHuongCount { get; set; }

        [XmlElement(ElementName = "XACMINHNSCOUNT")]
        public int? xacMinhNSCount { get; set; }

        [XmlElement(ElementName = "VKHOIHUONGCOUNT")]
        public int? vkHoiHuongCount { get; set; }

        [XmlElement(ElementName = "HSVIPHAMCOUNT")]
        public int? hsViPhamCount { get; set; }

        [XmlElement(ElementName = "THONGTINCAPHCCOUNT")]
        public int? thongTinCapHCCount { get; set; }

        [XmlElement(ElementName = "LICHSUXUATNHAPCANHCOUNT")]
        public int? lichSuXuatNhapCanhCount { get; set; }

    }
}