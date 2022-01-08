using Quartz;
using Quartz.Impl;
using Sync.Web.Jobs;
using Sync.Web.Jobs.PAJobs;
using Sync.Web.Models;
using Sync.Web.Services;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using static Sync.Web.Utils.Constants;

namespace Sync.Web.ScheduleTasks
{
    public class JobScheduler
    {
        static string logDirectory = ConfigurationManager.AppSettings.Get("LogDirectory");
        static string loaiTrungTam = ConfigurationManager.AppSettings.Get("LoaiTrungTam");
        public static void StartAllAsync()
        {
            var directory = logDirectory + "\\Sync-log";
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);
            if (Directory.GetCurrentDirectory() != directory)
            {
                Directory.SetCurrentDirectory(logDirectory);

                if (!Directory.Exists("Sync-log"))
                {
                    Directory.CreateDirectory("Sync-log");
                }
                Directory.SetCurrentDirectory(directory);
            }

            ProcessService processService = new ProcessService();
            List<ProcessModel> processModels = processService.GetListProcess();

            IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler().Result;
            scheduler.Start();

            //Job tự động kiểm tra trạng thái kết nối với TTĐH
            IJobDetail job_CheckConnectionStatus = JobBuilder.Create<CheckConnectWithTTDHJob>().Build();
            ITrigger trigger_CheckConnectionStatus = TriggerBuilder.Create()
                .StartNow()
                .WithCronSchedule("0 0/1 * * * ?") //Tự động chạy sau mỗi 1 phút
                .Build();
            scheduler.ScheduleJob(job_CheckConnectionStatus, trigger_CheckConnectionStatus);

            //Job tự động kiểm tra và gọi lại các API
            if (loaiTrungTam != "PA")
            {
                IJobDetail job_RecallApi = JobBuilder.Create<RecallApiJob>().Build();
                ITrigger trigger_RecallApi = TriggerBuilder.Create()
                    .StartNow()
                    .WithCronSchedule("0 0/1 * * * ?")
                    .Build();
                scheduler.ScheduleJob(job_RecallApi, trigger_RecallApi);
            }

            #region Các job thực thi các tiến trình đồng bộ dữ liệu
            foreach (ProcessModel item in processModels)
            {
                switch (item.Code)
                {
                    

                    #region PA Tasks
                    
                    case ProcessCode.PA_GuiThongTinHoSoLenTTDH:
                        if (item.Active)
                        {
                            IJobDetail job_PA_GuiThongTinHoSoLenTTDH = JobBuilder.Create<PA_GuiThongTinHoSoLenTTDHJob>().Build();
                            job_PA_GuiThongTinHoSoLenTTDH.JobDataMap["logPath"] = ProcessCode.PA_GuiThongTinHoSoLenTTDH;
                            ITrigger trigger_PA_GuiThongTinHoSoLenTTDH = TriggerBuilder.Create()
                                .WithIdentity("trigger_PA_GuiThongTinHoSoLenTTDH")
                                .StartNow()
                                .WithSimpleSchedule(x => x
                                    .WithIntervalInSeconds(item.TimeLoop)
                                    .RepeatForever())
                                .Build();
                            scheduler.ScheduleJob(job_PA_GuiThongTinHoSoLenTTDH, trigger_PA_GuiThongTinHoSoLenTTDH);
                            break;
                        }
                        else
                        {
                            break;
                        }
                    
                    #endregion

                    
                    default:
                        break;
                }
            }
            #endregion
        }

    }
}