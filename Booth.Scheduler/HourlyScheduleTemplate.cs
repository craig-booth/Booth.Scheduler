using System;
using System.Collections.Generic;
using System.Text;

namespace Booth.Scheduler
{
    public class HourlyScheduleTemplate : RecurringTimeScheduleTemplate
    {
        public HourlyScheduleTemplate() : base() { }
        public HourlyScheduleTemplate(int every) : base(every) { }
    }
}
