using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Sync.Web.Helpers
{
    public class JsonHelper
    {
        public static T GetDataFromJsonFile<T>(string configPath)
        {
            var file = File.ReadAllText(configPath);
            var data = JsonConvert.DeserializeObject<T>(file);
            return data;
        }
    }
}