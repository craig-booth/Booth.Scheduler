using System;
using System.Collections.Generic;
using System.Text;

namespace Booth.Scheduler
{
    public class RecurringTimeScheduleTemplate : ITimeScheduleTemplate
    {
        public int Every { get; set; }

        public TimeSpan FromTime { get; set; }
        public TimeSpan ToTime { get; set; }

        public RecurringTimeScheduleTemplate() : this(1) { }

        public RecurringTimeScheduleTemplate(int every)
        {
            Every = every;
        }

        public void From(int hour, int minute)
        {
            FromTime = new TimeSpan(hour, minute, 0);
        }

        public void To(int hour, int minute)
        {
            ToTime = new TimeSpan(hour, minute, 0);
        }

        public ITimeScheduleEnumerator ScheduleEnumerator(DateTime date)
        {
            throw new NotSupportedException();
        }
    }
}
