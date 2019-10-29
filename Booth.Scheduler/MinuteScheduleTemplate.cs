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
    }
}
