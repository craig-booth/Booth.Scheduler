using System;
using System.Collections.Generic;
using System.Text;

namespace Booth.Scheduler
{
    public class ExactTimeScheduleTemplate : TimeScheduleTemplate
    {
        public TimeSpan Time { get; set; }

        public ExactTimeScheduleTemplate(int hour, int minute)
        {
            Time = new TimeSpan(hour, minute, 0);
        }
    }
}
