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

        internal override DateTime GetPeriodStart(DateTime date)
        {
            return date;
        }

        internal override DateTime GetStartOfNextPeriod(DateTime startOfCurrentPeriod)
        {
            return startOfCurrentPeriod.AddDays(Every);
        }

        internal override bool GetNextDateInPeriod(DateTime currentDate, bool firstTime, out DateTime nextDate)
        {
            nextDate = currentDate;
            return firstTime;
        }
    }
}
