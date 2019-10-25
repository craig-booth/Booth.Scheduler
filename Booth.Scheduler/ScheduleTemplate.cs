using System;
using System.Collections.Generic;
using System.Text;

namespace Booth.Scheduler
{
    public class ScheduleTemplate
    {
        public IDateScheduleTemplate DateTemplate { get; set; }
        public ITimeScheduleTemplate TimeTemplate { get; set; }
    }
}
