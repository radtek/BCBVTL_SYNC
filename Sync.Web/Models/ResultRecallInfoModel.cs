using System.Collections.Generic;

namespace Sync.Web.Models
{
    public class ResultRecallInfoModel
    {
        public List<RecallInfoModel> Data { get; set; }
        public bool ConnectDB { get; set; }
    }
}