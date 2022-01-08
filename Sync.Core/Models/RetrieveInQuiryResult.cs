using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sync.Core.Models
{
    public partial class RetrieveInQuiryResult
    {
        public string dataSource { get; set; }
        /// <summary>
        /// true: có dữ liệu sinh trắc trùng
        /// false không có dữ liệu sinh trắc trùng
        /// </summary>
        public bool hisDecision { get; set; }
        public decimal personId { get; set; }
        public string personIdNo { get; set; }
        public RetrieveInQuiryResultfingerMap hitInfo { get; set; }
    }

    public class RetrieveInQuiryResultfingerMap
    {
        [JsonProperty("1")]
        public decimal? VanTay1 { get; set; }
        [JsonProperty("2")]
        public decimal? VanTay2 { get; set; }
        [JsonProperty("3")]
        public decimal? VanTay3 { get; set; }
        [JsonProperty("4")]
        public decimal? VanTay4 { get; set; }
        [JsonProperty("5")]
        public decimal? VanTay5 { get; set; }
        [JsonProperty("6")]
        public decimal? VanTay6 { get; set; }
        [JsonProperty("7")]
        public decimal? VanTay7 { get; set; }
        [JsonProperty("8")]
        public decimal? VanTay8 { get; set; }
        [JsonProperty("9")]
        public decimal? VanTay9 { get; set; }
        [JsonProperty("10")]
        public decimal? VanTay10 { get; set; }
    }
}
