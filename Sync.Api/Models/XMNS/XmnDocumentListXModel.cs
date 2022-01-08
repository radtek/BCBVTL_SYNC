using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Sync.Api.Models.XMNS
{
    public class XmnDocumentListXModel
    {
        [XmlElement(ElementName = "CODE")]
        public string code { get; set; }

        [XmlElement(ElementName = "TYPE_")]
        public string type { get; set; }

        [XmlElement(ElementName = "ORGANIZATIONCODE")]
        public string organizationCode { get; set; }

        [XmlElement(ElementName = "YEAR_")]
        public int year { get; set; }

        [XmlElement(ElementName = "DOCUMENTARYNO")]
        public string documentaryNo { get; set; }

        [XmlElement(ElementName = "DOCUMENTARYDATE")]
        public string documentaryDate { get; set; }

        [XmlElement(ElementName = "CREATEBY")]
        public string createBy { get; set; }

        [XmlElement(ElementName = "CREATEBYNAME")]
        public string createByName { get; set; }

        [XmlElement(ElementName = "CREATEDATETIME")]
        public string createDatetime { get; set; }

        [XmlElement(ElementName = "SITECODE")]
        public string siteCode { get; set; }

        [XmlElement(ElementName = "XMDEAR")]
        public string xmDear { get; set; }

        [XmlElement(ElementName = "XMCONTENTCHECK")]
        public string xmContentCheck { get; set; }

        [XmlElement(ElementName = "XMCONTENTVERIFICATION")]
        public string xmContentVerification { get; set; }

        [XmlElement(ElementName = "XMEXPIREDVERIFICATION")]
        public string xmExpiredVerification { get; set; }

        [XmlElement(ElementName = "APPROVERBY")]
        public string approverBy { get; set; }

        [XmlElement(ElementName = "APPROVERNAME")]
        public string approverName { get; set; }

        [XmlElement(ElementName = "APPROVERPOSITION")]
        public string approverPosition { get; set; }

        [XmlElement(ElementName = "UPDATEBY")]
        public string updateBy { get; set; }

        [XmlElement(ElementName = "UPDATEBYNAME")]
        public string updateByName { get; set; }

        [XmlElement(ElementName = "UPDATEDATETIME")]
        public string updateDatetime { get; set; }

        [XmlElement(ElementName = "STATUS")]
        public string status { get; set; }
    }
}
