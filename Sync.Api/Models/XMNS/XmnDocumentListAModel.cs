using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Sync.Api.Models.XMNS
{
    public class XmnDocumentListAModel
    {
        [XmlElement(ElementName = "CODE")]
        public string code { set; get; }

        [XmlElement(ElementName = "TYPE_")]
        public string type { set; get; }

        [XmlElement(ElementName = "ORGANIZATIONCODE")]
        public string organizationCode { set; get; }

        [XmlElement(ElementName = "YEAR_")]
        public int year { set; get; }

        [XmlElement(ElementName = "DOCUMENTARYNO")]
        public string documentaryNo { set; get; }

        [XmlElement(ElementName = "DOCUMENTARYDATE")]
        public string documentaryDate { set; get; }

        [XmlElement(ElementName = "RECEIPTDATETIME")]
        public string receiptDateTime { set; get; }

        [XmlElement(ElementName = "RECEIPTNO")]
        public string receiptNo { set; get; }

        [XmlElement(ElementName = "STATUS")]
        public string status { set; get; }

        [XmlElement(ElementName = "CREATEBY")]
        public string createBy { set; get; }

        [XmlElement(ElementName = "CREATEBYNAME")]
        public string createByName { set; get; }

        [XmlElement(ElementName = "CREATEDATETIME")]
        public string createDatetime { set; get; }

        [XmlElement(ElementName = "UPDATEBY")]
        public string updateBy { set; get; }

        [XmlElement(ElementName = "UPDATEBYNAME")]
        public string updateByName { set; get; }

        [XmlElement(ElementName = "UPDATEDATETIME")]
        public string updateDatetime { set; get; }

        [XmlElement(ElementName = "SITECODE")]
        public string siteCode { set; get; }

        public List<XMN_Document_Att> dataList { get; set; }
    }
}
