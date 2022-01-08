using Quartz;
using Sync.Web.Controllers.PA;
using Sync.Web.Utils;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Web.Hosting;
using System.Web.Mvc;

namespace Sync.Web.Jobs.PAJobs
{
    public class PA_GuiThongTinHoSoLenTTDHJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            var controller = DependencyResolver.Current.GetService<PAController>();
            var result = await controller.GuiThongTinHoSoLenTTDH();

            JobDataMap dataMap = context.JobDetail.JobDataMap;
            string logPath = "Log\\" + (string)dataMap["logPath"] + "_" + DateTime.Now.ToString("yyyyMMdd") + ".log";
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
                file.Prepend(result.message + "\n");
            }
        }
    }
}