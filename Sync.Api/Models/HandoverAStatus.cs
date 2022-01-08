using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Sync.Api.Models
{
    public class HandoverAStatus
    {
        [XmlElement(ElementName = "PACKAGEID")]
        public string packageId { get; set; }

        [XmlElement(ElementName = "STATUS")]
        public string status { get; set; }

        [XmlElement(ElementName = "IDQUEUE")]
        public long idQueue { get; set; }
    }

    public class HandoverAStatuss
    {
        public List<HandoverAStatus> ListA { get; set; }
    }
}
