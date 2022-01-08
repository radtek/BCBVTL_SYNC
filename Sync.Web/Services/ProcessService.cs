using Newtonsoft.Json;
using Sync.Web.Helpers;
using Sync.Web.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using static Sync.Web.Utils.Constants;

namespace Sync.Web.Services
{
    public class ProcessService
    {
        public List<ProcessModel> GetListProcess()
        {
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Start/ProcessConfig.json");
            var listProcess = JsonHelper.GetDataFromJsonFile<List<ProcessModel>>(path);
            string module = ConfigurationSettings.AppSettings.Get("LoaiTrungTam");
            return listProcess.Where(x => x.Module == module).ToList();
        }
    }
}