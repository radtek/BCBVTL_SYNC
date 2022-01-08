using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace Sync.Api.Models
{
    public class PassportStatus
    {
        [XmlElement(ElementName = "PASSPORT_NO")]
        public string passportNo { get; set; }

        [XmlElement(ElementName = "DATEOFISSUE")]
        public string dateOfIssue { get; set; }

        [XmlElement(ElementName = "DATEOFEXPIRY")]
        public string dateOfExpiry { get; set; }

        [XmlElement(ElementName = "STATUS")]
        public string status { get; set; }

        [XmlElement(ElementName = "STATUS_TEXT")]
        public string status_text
        {
            get
            {
                var value = "";
                if (!string.IsNullOrEmpty(status))
                {
                    //value = Utils.Utils.LoadPassportStatus(status, cancelType);
                }
                return value;
            }
        }

        [XmlElement(ElementName = "CANCEL_TYPE")]
        public string cancelType { get; set; }
    }
}