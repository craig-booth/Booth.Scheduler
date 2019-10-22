using System;
using System.Collections.Generic;
using System.Text;

namespace Booth.Scheduler.Fluent
{
    public class TimeSchedule
    {
        public DateSchedule DateSchedule { get; }
        protected readonly TimeTemplate TimeTemplate;

        public TimeSchedule(DateSchedule dateSchedule, int hour, int minute)
        {
            DateSchedule = DateSchedule;
            TimeTemplate = new TimeTemplate(hour, minute);
        }
        public TimeSchedule(DateSchedule dateSchedule, int every, TimeUnit units)
        {
            DateSchedule = DateSchedule;
            TimeTemplate = new TimeTemplate(every, units);
        }

        public TimeSchedule Between(int fromHour, int fromMinute, int toHour, int toMinute)
        {
            TimeTemplate.FromTime = new TimeSpan(fromHour, fromMinute, 0);
            TimeTemplate.ToTime = new TimeSpan(toHour, toMinute, 0);

            return this;
        }
    }
}
