using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sync.Api.Models.Updater
{
    public class DataGroup
    {
        public string type { get; set; }
        public List<PartFile> partFiles {
            get { 
                if (partFiles == null)
                {
                    return new List<PartFile>();
                }
                return partFiles;
            }
            set {
                this.partFiles = value;
            }
        }
        public int total { get; set; }

    }
}