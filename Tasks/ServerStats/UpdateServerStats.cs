using Quartz;
using Quartz.Impl;
using System.Threading.Tasks;

namespace SROMCapi.Tasks.ServerStats
{
    /// <summary>
    /// Update server stats
    /// </summary>
    public class UpdateServerStats
    {
        public static async Task Start()
        {
            StdSchedulerFactory factory = new StdSchedulerFactory();

            // Scheduler
            IScheduler scheduler = await factory.GetScheduler();
            await scheduler.Start();

            // Task
            IJobDetail job = JobBuilder.Create<Tasks.TaskUpdateServerStats>()
                .WithIdentity("TaskUpdateServerStats", null)
                .Build();

            // Trigger
            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("TaskUpdateServerStats")
                .StartNow()
                .WithSimpleSchedule(x => x.WithIntervalInMinutes(5).RepeatForever())
                .Build();

            // Schedule
            await scheduler.ScheduleJob(job, trigger);
        }
    }
}
