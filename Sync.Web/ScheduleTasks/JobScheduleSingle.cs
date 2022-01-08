using Quartz;
using Quartz.Impl;
using Sync.Web.Jobs.PAJobs;
using Sync.Web.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static Sync.Web.Utils.Constants;

namespace Sync.Web.ScheduleTasks
{
    public class JobScheduleSingle
    {
        public static void StartSingle(ProcessModel item)
        {
            IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler().Result;

            scheduler.Start();

            var currentlyExecuting = scheduler.GetCurrentlyExecutingJobs().Result;

            switch (item.Code)
            {
                

                #region PA Tasks
               
                case ProcessCode.PA_GuiThongTinHoSoLenTTDH:
                    if (item.Active)
                    {
                        IJobDetail job_PA_GuiThongTinHoSoLenTTDH = JobBuilder.Create<PA_GuiThongTinHoSoLenTTDHJob>().Build();
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
                        foreach (var job in currentlyExecuting)
                        {
                            if (string.Equals(job.JobDetail.JobType.Name, "PA_GuiThongTinHoSoLenTTDHJob"))
                            {
                                scheduler.DeleteJob(job.JobDetail.Key);
                                break;
                            }
                        }

                    }
                    break;
               
                #endregion

                
                default:
                    break;
            }
        }
    }
}