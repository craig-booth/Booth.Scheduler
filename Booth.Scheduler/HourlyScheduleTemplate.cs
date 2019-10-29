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


    }
}
