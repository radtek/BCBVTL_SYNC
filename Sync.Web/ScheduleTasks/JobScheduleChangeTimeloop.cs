using Quartz;
using Quartz.Impl;
using Quartz.Impl.Matchers;
using Sync.Web.Jobs.PAJobs;
using Sync.Web.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using static Sync.Web.Utils.Constants;

namespace Sync.Web.ScheduleTasks
{
    public static class JobScheduleChangeTimeloop
    {
        public static async Task ChangeTimeloopAsync(ProcessModel item)
        {
            IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler().Result;
            await scheduler.Start();

            var currentlyExecuting = scheduler.GetCurrentlyExecutingJobs().Result;

            foreach (var job in currentlyExecuting)
            {
                if (string.Equals(job.JobDetail.JobType.Name, item.Code + "Job"))
                {
                    await scheduler.UnscheduleJob(job.Trigger.Key);
                    await scheduler.DeleteJob(job.JobDetail.Key);

                    IJobDetail jobDetail = CreateJob(item.Code);
                    ITrigger trigger = TriggerBuilder.Create()
                        .WithIdentity("trigger_" + item.Code)
                        .StartNow()
                        .WithSimpleSchedule(x => x
                            .WithIntervalInSeconds(item.TimeLoop)
                            .RepeatForever())
                        .Build();
                    await scheduler.ScheduleJob(jobDetail, trigger);
                    break;
                }
            }

        }

        public static IJobDetail CreateJob(string code)
        {
            switch (code)
            {
                

                #region PA Jobs
               
                case ProcessCode.PA_GuiThongTinHoSoLenTTDH:
                    return JobBuilder.Create<PA_GuiThongTinHoSoLenTTDHJob>().Build();
                #endregion

                default:
                    return null;
            }
        }

        public static async Task<List<IJobDetail>> GetJobsAsync(IScheduler scheduler)
        {
            List<IJobDetail> jobs = new List<IJobDetail>();
            var jobKeys = await scheduler.GetJobKeys(GroupMatcher<JobKey>.AnyGroup());
            foreach (JobKey jobKey in jobKeys)
            {
                jobs.Add(await scheduler.GetJobDetail(jobKey));
            }

            return jobs;
        }
    }
}