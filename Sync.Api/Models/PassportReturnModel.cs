using System.Collections.Generic;
using System.Xml.Serialization;

namespace Sync.Api.Models
{
    public class PassportReturnModel
    {
        public List<PPReturnData> data { get; set; }
    }
    public class PPReturnData
    {
        [XmlElement(ElementName = "TRANSACTIONID")]
        public string transactionId { get; set; }
        [XmlElement(ElementName = "IDQUEUE")]
        public int? idQueue { get; set; }
    }
    public class PPReturnDatas
    {
        public List<PPReturnData> PPReturnData { get; set; }
    }
}