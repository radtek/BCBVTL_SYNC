using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sync.Api.Models.UpdatePerson
{
    public class ListUser
    {
        public List<SYS_USERS> Users { get; set; }
        public bool success { get; set; }
    }
    public class SYS_USERS
    {
        public decimal? USER_ID { get; set; }
        public string PASSWORD { get; set; }
        public string FULL_NAME { get; set; }
        public decimal? DEPT_ID { get; set; }
        public string START_DATE { get; set; }
        public string END_DATE { get; set; }
        public string USER_GROUP { get; set; }
        public DateTime? LAST_SIGNED_IN { get; set; }
        public string LOGIN_NAME { get; set; }
        public decimal? UNIT_ID { get; set; }
        public string USER_DESC { get; set; }
        public string USER_CLASS { get; set; }
        public decimal? FAIL_LOGIN_COUNT { get; set; }
        public string ENABLED_FLAG { get; set; }
        public string TEL_NO { get; set; }
        public string IMG_PATH { get; set; }
        public decimal? UPDATED_BY { get; set; }
        public DateTime? UPDATE_DATE { get; set; }
        public decimal? SUB_UNIT_ID { get; set; }
        public string IS_SIGNER { get; set; }
        public string IS_EXEC { get; set; }

    }
}