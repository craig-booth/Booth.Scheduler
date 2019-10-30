using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Booth.Scheduler
{
    public class DailyScheduleTemplate : IDateScheduleTemplate
    {
        public int Every { get; set; }

        public DailyScheduleTemplate() : this(1) { }

        public DailyScheduleTemplate(int every)
        {
            Every = every;
        }

        public IEnumerable<DateTime> GetDates(DateTime start)
        {
            var nextDate = start.Date;

            while (true)
            {
                yield return nextDate;
                nextDate = nextDate.AddDays(Every);
            }
        }
    }
}
