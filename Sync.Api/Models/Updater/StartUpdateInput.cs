using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Sync.Api.Models.Updater
{
    public class StartUpdateInput
    {
        public string desc { get; set; }
        public string type { get; set; }
        public int total { get; set; }
        public List<FileIdentity> listId { get; set; }
    }

    public class FileIdentity
    {
        public long id { get; set; }
        public string fileName { get; set; }
    }

    public class PartFile
    {
        public long id { get; set; }
        public string type { get; set; }
        public string data { get; set; }
        public string fileName { get; set; }
        public string key { get; set; }
    }
    public class UpdateFinishInput
    {
        public string key { get; set; }
        public string type { get; set; }
    }

    public enum UpdateTypeEnum
    {
        [EnumMember(Value = "WEBA")]
        WEBA,
        [EnumMember(Value = "TRANS")]
        TRANS,
        [EnumMember(Value = "RECEI")]
        RECEI,
        [EnumMember(Value = "SCRPT")]
        SCRPT
    }

}