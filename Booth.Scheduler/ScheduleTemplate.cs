using System;
using System.Collections.Generic;
using System.Text;

namespace Booth.Scheduler
{
    public class ScheduleTemplate
    {
        public DateScheduleTemplate DateTemplate { get; set; }
        public TimeScheduleTemplate TimeTemplate { get; set; }
    }
}
