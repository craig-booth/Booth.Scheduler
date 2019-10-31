using System;
using System.Collections.Generic;
using System.Text;

namespace Booth.Scheduler
{
    public class HourlyScheduleTemplate : RecurringTimeScheduleTemplate
    {
        internal override TimeSpan TimeIncrement { get; set; }

        public HourlyScheduleTemplate() : this(1) { }
        public HourlyScheduleTemplate(int every) : base(every) 
        {
            TimeIncrement = new TimeSpan(every, 0, 0);
        }

        public override string ToString()
        {
            if (Every == 1)
                return "every hour between " + FromTime.ToString(@"h\:mm") + " and " + ToTime.ToString(@"h\:mm");
            else
                return "every " + Every.ToString() + " hours between " + FromTime.ToString(@"h\:mm") + " and " + ToTime.ToString(@"h\:mm");
        }

    }
}
