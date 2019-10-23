using System;
using System.Collections.Generic;
using System.Text;

namespace Booth.Scheduler
{
    public class DailyScheduleTemplate : DateScheduleTemplate
    {
        public int Every { get; set; }

        public DailyScheduleTemplate() : this(1) { }

        public DailyScheduleTemplate(int every)
        {
            Every = every;
        }
    }
}
