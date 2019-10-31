using System;
using System.Collections.Generic;
using System.Text;

namespace Booth.Scheduler
{
    public class MinuteScheduleTemplate : RecurringTimeScheduleTemplate
    {
        internal override TimeSpan TimeIncrement { get; set; }

        public MinuteScheduleTemplate() : this(1) { }
        public MinuteScheduleTemplate(int every) : base(every)
        {
            TimeIncrement = new TimeSpan(0, every, 0);
        }

        public override string ToString()
        {
            if (Every == 1)
                return "every minute between " + FromTime.ToString(@"h\:mm") + " and " + ToTime.ToString(@"h\:mm");
            else
                return "every " + Every.ToString() + " minutes between " + FromTime.ToString(@"h\:mm") + " and " + ToTime.ToString(@"h\:mm");

        }
    }
}
