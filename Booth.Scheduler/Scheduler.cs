using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Threading;
using System.Linq;
using System.Threading.Tasks;

namespace Booth.Scheduler
{
    public class Scheduler
    {
        private Timer _Timer;
        public bool Running { get; private set; }

        private ConcurrentDictionary<string, ScheduledJob> _ScheduledJobs = new ConcurrentDictionary<string, ScheduledJob>();

        public Scheduler()
        {
            _Timer = new Timer(RunJobs, null, Timeout.Infinite, Timeout.Infinite);
            Running = false;
        }
        
        public void AddJob(string name, Action action, ScheduleTemplate template, DateTime startDate)
        {
            var job = new ScheduledJob(name, template, action, startDate);

            if (!_ScheduledJobs.TryAdd(name, job))
                throw new ArgumentException("A job with that name already exists");

            job.GetFirstRunTime(DateTime.Now);

            if (Running)
                _Timer.Change(0, Timeout.Infinite);
        }

        public void Start()
        {
            Running = true;
            _Timer.Change(0, Timeout.Infinite);   
        }

        public void Stop()
        {
            Running = false;
            _Timer.Change(Timeout.Infinite, Timeout.Infinite);
        }

        public IReadOnlyList<ScheduledJob> Jobs
        {
            get
            {
                return _ScheduledJobs.Values.ToList();
            }
        }

        private void RunJobs(object state)
        {
            _Timer.Change(Timeout.Infinite, Timeout.Infinite);
     
            var now = DateTime.Now;
            var nextCheck = now.AddDays(1);

            foreach (var scheduledJob in _ScheduledJobs.Values)
            {
                if (scheduledJob.Status == JobStatus.Active)
                {
                    if (scheduledJob.NextRunTime <= now)
                    {
                        // Run the job
                        Task.Run(scheduledJob.Action);

                        scheduledJob.GetNextRunTime();
                    }
                    if (scheduledJob.NextRunTime < nextCheck)
                        nextCheck = scheduledJob.NextRunTime;
                }
            }

            var interval = (int)(nextCheck - DateTime.Now).TotalMilliseconds;
            if (interval < 0)
                interval = 0;

            _Timer.Change(interval, Timeout.Infinite);
        }

    }
}
