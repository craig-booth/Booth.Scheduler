using System;
using System.Collections.Generic;
using System.Text;

namespace Booth.Scheduler.Fluent
{
    public abstract class DateSchedule
    {
        protected readonly DateTemplate Template;
        public DateSchedule(DateTemplate template)
        {
            Template = template;
        }
        public DateSchedule Between(DateTime fromDate, DateTime toDate)
        {
            Template.FromDate = fromDate;
            Template.ToDate = toDate;

            return this;
        }

        public TimeSchedule At(int hour, int minute)
        {
            return new TimeSchedule(this, hour, minute);
        }

        public TimeSchedule EveryHour()
        {
            return new TimeSchedule(this, 1, TimeUnit.Hours);
        }
        public TimeSchedule EveryHours(int every)
        {
            return new TimeSchedule(this, every, TimeUnit.Hours);
        }
        public TimeSchedule EveryMinute()
        {
            return new TimeSchedule(this, 1, TimeUnit.Minutes);
        }
        public TimeSchedule EveryMinutes(int every)
        {
            return new TimeSchedule(this, every, TimeUnit.Minutes);
        }
    }
}
