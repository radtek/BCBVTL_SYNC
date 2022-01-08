using Quartz;
using Sync.Core.Models;
using Sync.Web.Services;
using Sync.Web.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Sync.Web.Jobs
{
    public class RecallApiJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            ApiResult result = await BaseServices.RecallApi();

            string logPath = "Log\\RecallApi" + "_" + DateTime.Now.ToString("yyyyMMdd") + ".log";
            if (!Directory.Exists("Log"))
            {
                Directory.CreateDirectory("Log");
            }

            if (!File.Exists(logPath))
            {
                File.Create(logPath);
            }

            using (var file = File.Open(logPath, FileMode.Open, FileAccess.ReadWrite))
            {
                file.Prepend($"{DateTime.Now:yy.MM.dd HH:mm:ss} - {result.message}" + "\n");
            }
        }
    }
}