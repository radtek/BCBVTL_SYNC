using Sync.Api.Models.DocumentFull;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Sync.Api.Models
{
    public class ListBModels
    {
        public List<ListBModel> ListB { get; set; }
    }
    public class ListBModel
    {
        [XmlElement(ElementName = "PACKAGEID")]
        public string packageId { get; set; }

        [XmlElement(ElementName = "OFFERNAME")]
        public string offerName { get; set; }

        [XmlElement(ElementName = "OFFERDATE")]
        public string offerDate { get; set; }

        [XmlElement(ElementName = "APPROVENAME")]
        public string approveName { get; set; }

        [XmlElement(ElementName = "APPROVEDATE")]
        public string approveDate { get; set; }

        [XmlElement(ElementName = "SITECODE")]
        public string siteCode { get; set; }

        [XmlElement(ElementName = "TYPE")]
        public int type { get; set; }

        [XmlElement(ElementName = "COUNT")]
        public int count { get; set; }

        [XmlElement(ElementName = "IDQUEUE")]
        public long idQueue { get; set; }

        public List<DetailHandoverBModel> handovers { get; set; }
    }

    public class DetailHandoverBModel : DetailHandover
    {
        public List<PaymentDetail> payments { get; set; }

        [XmlElement(ElementName = "PERSONCODE")]
        public string personCode { get; set; }

        [XmlElement(ElementName = "PERSONSTAGE")]
        public string personStage { get; set; }
    }
    //public class DetailHandover
    //{
    //    [XmlElement(ElementName = "PACKAGEID")]
    //    public string packageId { get; set; }

    //    [XmlElement(ElementName = "TRANSACTIONID")]
    //    public string transactionId { get; set; }

    //    [XmlElement(ElementName = "APPROVESTAGE")]
    //    public string approveStage { get; set; }

    //    [XmlElement(ElementName = "OFFERSTAGE")]
    //    public string offerStage { get; set; }

    //    [XmlElement(ElementName = "NOTEOFFER")]
    //    public string noteOffer { get; set; }

    //    [XmlElement(ElementName = "NOTEAPPROVE")]
    //    public string noteApprove { get; set; }

    //}
}
