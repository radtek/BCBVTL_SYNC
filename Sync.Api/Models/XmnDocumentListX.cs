using Sync.Api.Models.XMNS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Sync.Api.Models
{
    public class XmnDocumentListX
    {
        public XmnDocumentListXModel xmnDocListX { get; set; }
        public XmnDocumentListAModel xmnDocListA { get; set; }
        public List<XMN_Document> documents { get; set; }
        [XmlElement(ElementName = "IDQUEUE")]
        public decimal idQueue { get; set; }
    }
}
