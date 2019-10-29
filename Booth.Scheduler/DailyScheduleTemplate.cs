using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Booth.Scheduler
{
    public class DailyScheduleTemplate : DateScheduleTemplate, IDateScheduleTemplate
    {
        public int Every { get; set; }

        public DailyScheduleTemplate() : this(1) { }

        public DailyScheduleTemplate(int every)
        {
            Every = every;
        }

        protected override DateTime GetPeriodStart(DateTime date)
        {
            return date;
        }

        protected override DateTime GetStartOfNextPeriod(DateTime startOfCurrentPeriod)
        {
            return startOfCurrentPeriod.AddDays(Every);
        }

        protected override bool GetNextDateInPeriod(DateTime currentDate, bool firstTime, out DateTime nextDate)
        {
            nextDate = currentDate;
            return firstTime;
        }
    }
}
