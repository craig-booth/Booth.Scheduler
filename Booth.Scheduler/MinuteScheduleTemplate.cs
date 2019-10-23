using System;
using System.Collections.Generic;
using System.Text;

namespace Booth.Scheduler
{
    public class MinuteScheduleTemplate : RecurringTimeScheduleTemplate
    {
        public MinuteScheduleTemplate() : base() { }

        public MinuteScheduleTemplate(int every) : base(every) { }
    }
}
