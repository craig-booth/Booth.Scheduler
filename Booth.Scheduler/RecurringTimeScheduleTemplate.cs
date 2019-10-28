using System;
using System.Collections;
using System.Collections.Generic;

namespace Booth.Scheduler
{
    public abstract class RecurringTimeScheduleTemplate : ITimeScheduleTemplate
    {
        public int Every { get; set; }

        public TimeSpan FromTime { get; private set; }
        public TimeSpan ToTime { get; private set; }
        internal abstract TimeSpan TimeIncrement { get; set; }

        public RecurringTimeScheduleTemplate() : this(1) { }

        public RecurringTimeScheduleTemplate(int every)
        {
            Every = every;
            FromTime = new TimeSpan(0, 0, 0);
            ToTime = new TimeSpan(23, 59, 0);
        }

        public void From(int hour, int minute)
        {
            FromTime = new TimeSpan(hour, minute, 0);
        }

        public void To(int hour, int minute)
        {
            ToTime = new TimeSpan(hour, minute, 0);
        }
    }
}
