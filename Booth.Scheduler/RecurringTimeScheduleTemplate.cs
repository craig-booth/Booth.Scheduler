using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Booth.Scheduler
{
    public abstract class RecurringTimeScheduleTemplate : ITimeScheduleTemplate
    {
        public int Every { get; set; }

        public TimeSpan FromTime { get; set; }
        public TimeSpan ToTime { get; set; }
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

        public IEnumerable<TimeSpan> GetTimes()
        {
            var nextTime = FromTime;           
            while (nextTime <= ToTime)
            {
                yield return nextTime;
                nextTime = nextTime.Add(TimeIncrement);
            }
        }

        public IEnumerable<string> Validate()
        {
            if ((TimeIncrement.TotalMinutes < 1) || (TimeIncrement.Days >= 1))
                yield return "Time must be less than 1 day";

            if ((FromTime.Ticks < 0) || (FromTime.Days >= 1))
                yield return "From time must be less than 1 day";

            if ((ToTime.Ticks < 0) || (ToTime.Days >= 1))
                yield return "To time must be less than 1 day";

            if (FromTime >= ToTime)
                yield return "From time must be earlier than To Time";
        }
    }
}
