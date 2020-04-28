using System;
using System.Collections.Generic;
using System.Text;

using Booth.Common;

namespace Booth.Scheduler
{
    public enum JobStatus { Active, Stopped, Complete } 
    public class ScheduledJob
    {
        public string Name { get; }
        public string Description { get; }
        public JobStatus Status { get; private set; }
        public DateTime NextRunTime { get; private set; }

        internal readonly Action Action;

        private readonly ScheduleInstance _ScheduleInstance;
        private IEnumerator<DateTime> _ScheduleEnumerator;

        public ScheduledJob(string name, ScheduleTemplate schedule, Action action, DateTime startDate)
        {
            Name = name;
            Description = schedule.ToString();      
            Action = action;
            Status = JobStatus.Active;

            _ScheduleInstance = new ScheduleInstance(schedule, startDate);
        }

        internal DateTime GetFirstRunTime(DateTime after)
        {
            if (_ScheduleEnumerator == null)
            {
                _ScheduleEnumerator = _ScheduleInstance.GetEnumerator();
                _ScheduleEnumerator.MoveNext();
            }

            while (_ScheduleEnumerator.Current < after)
                _ScheduleEnumerator.MoveNext();

            NextRunTime = _ScheduleEnumerator.Current;
            return NextRunTime;
        }

        internal DateTime GetNextRunTime()
        {         
            _ScheduleEnumerator.MoveNext();
            NextRunTime = _ScheduleEnumerator.Current;
            return NextRunTime;
        }
    }
}
