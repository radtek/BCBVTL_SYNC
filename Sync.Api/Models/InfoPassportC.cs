using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Sync.Api.Models
{
    public class InfoPassportC
    {
        [XmlElement(ElementName = "CHIPSERIALNO")]
        public String chipSerialNo { get; set; }

        [XmlElement(ElementName = "TRANSACTIONTYPE")]
        public String transactionType { get; set; }

        [XmlElement(ElementName = "PASSPORTNO")]
        public String passportNo { get; set; }

        [XmlElement(ElementName = "PASSPORTTYPE")]
        public String passportType { get; set; }

        [XmlElement(ElementName = "DATEOFISSUE")]
        public string dateOfIssue { get; set; }

        [XmlElement(ElementName = "DATEOFEXPIRY")]
        public string dateOfExpiry { get; set; }

        [XmlElement(ElementName = "ICAOLINE1")]
        public String icaoLine1 { get; set; }

        [XmlElement(ElementName = "ICAOLINE2")]
        public String icaoLine2 { get; set; }

        [XmlElement(ElementName = "SIGNERNAME")]
        public String signerName { get; set; }

        [XmlElement(ElementName = "SIGNERPOSITION")]
        public String signerPosition { get; set; }

        [XmlElement(ElementName = "DESCRIPTION")]
        public String description { get; set; }

        [XmlElement(ElementName = "STATUS")]
        public String status { get; set; }

        [XmlElement(ElementName = "PLACEOFISSUEID")]
        public String placeOfIssueId { get; set; }

        [XmlElement(ElementName = "PLACEOFISSUENAME")]
        public String placeOfIssueName { get; set; }

        [XmlElement(ElementName = "PERSONID")]
        public String personId { get; set; }

        [XmlElement(ElementName = "FPENCODE")]
        public String fpEncode { get; set; }

        [XmlElement(ElementName = "ISEPASSPORT")]
        public Boolean isEpassport { get; set; }

        [XmlElement(ElementName = "PRINTNAME")]
        public String printName { get; set; }

        [XmlElement(ElementName = "IS_EPASSPORT")]
        public string is_passport
        {
            get
            {
                try
                {
                    if (isEpassport == true)
                    {
                        return "Y";
                    }
                    else
                    {
                        return "N";
                    }
                }
                catch
                {
                    return null;
                }
            }
        }
    }
}
