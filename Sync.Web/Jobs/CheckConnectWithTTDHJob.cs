using Quartz;
using Sync.Core.Models;
using Sync.Web.Services;
using Sync.Web.Utils;
using System;
using System.Configuration;
using System.IO;
using System.Threading.Tasks;

namespace Sync.Web.Jobs
{
    public class CheckConnectWithTTDHJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            string maTrungTam = ConfigurationManager.AppSettings.Get("MaTrungTam");
            string trangThaiKetNoi = "";
            ApiResult result = await BaseServices.CheckConnectAPIAsync(maTrungTam);
            if (result != null && result.code == "200")
            {
                trangThaiKetNoi = "Kết nối thành công";
            }
            else
            {
                trangThaiKetNoi = "Kết nối không thành công";
            }

            string logPath = "Log\\TrangThaiKetNoi" + "_" + DateTime.Now.ToString("yyyyMMdd") + ".log";
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
                file.Prepend($"{DateTime.Now:yy.MM.dd HH:mm:ss} - {trangThaiKetNoi}" + "\n");
            }
        }
    }
}