using Sync.Core.Entities.PA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace Sync.Api.Models.Buf
{
    public class ResponseDowloadBuff
    {
        public List<DataPersonBuff> listData { get; set; }
        public string transactionCode { get; set; }
        /// <summary>
        /// •	Theo vân tay: AFIS
        /// •	Thuộc tính: CPD
        /// </summary>
        public string buffType { get; set; }

        [XmlElement(ElementName = "IDQUEUE")]
        public long? idQueue { get; set; }
    }
    public class DataPersonBuffs
    {
        public List<DataPersonBuff> DataPersonBuff { get; set; }
    }
}