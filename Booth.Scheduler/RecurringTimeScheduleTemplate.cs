using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using Booth.Common;

namespace Booth.Scheduler
{
    public abstract class RecurringTimeScheduleTemplate : ITimeScheduleTemplate
    {
        public int Every { get; set; }

        public Time FromTime { get; set; }
        public Time ToTime { get; set; }
        internal abstract TimeSpan TimeIncrement { get; set; }

        public RecurringTimeScheduleTemplate() : this(1) { }

        public RecurringTimeScheduleTemplate(int every)
        {
            Every = every;
            FromTime = new Time(0, 0, 0);
            ToTime = new Time(23, 59, 0);
        }

        public void From(int hour, int minute)
        {
            FromTime = new Time(hour, minute, 0);
        }

        public void To(int hour, int minute)
        {
            ToTime = new Time(hour, minute, 0);
        }

        public IEnumerable<Time> GetTimes()
        {
            var nextTime = FromTime;           
            while (nextTime <= ToTime)
            {
                yield return nextTime;

                try
                {
                    nextTime = nextTime.Add(TimeIncrement);
                }
                catch (OverflowException)
                {
                    break;
                }
                
            }
        }

        public IEnumerable<string> Validate()
        {
            if ((TimeIncrement.TotalMinutes < 1) || (TimeIncrement.Days >= 1))
                yield return "Time must be less than 1 day";

            if (FromTime >= ToTime)
                yield return "From time must be earlier than To Time";
        }
    }
}
