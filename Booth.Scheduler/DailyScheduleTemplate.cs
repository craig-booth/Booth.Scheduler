using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

using Booth.Common;

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

        public IEnumerable<Date> GetDates(Date start)
        {
            var nextDate = start;

            while (true)
            {
                yield return nextDate;
                nextDate = nextDate.AddDays(Every);
            }
        }

        public IEnumerable<string> Validate()
        {
            if (Every < 1)
                yield return "Daily schedule must occur atleast every 1 days";
        }

        public override string ToString()
        {
            if (Every == 1)
                return "every day";
            else
                return "every " + Every.ToString() + " days";

        }
    }
}
