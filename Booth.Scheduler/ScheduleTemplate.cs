using System;
using System.Collections.Generic;
using System.Text;

namespace Booth.Scheduler
{
    public class ScheduleTemplate
    {
        public IDateScheduleTemplate DateTemplate { get; internal set; }
        public ITimeScheduleTemplate TimeTemplate { get; internal set; }

        public ScheduleTemplate(IDateScheduleTemplate dateTemplate, ITimeScheduleTemplate timeTemplate)
        {
            DateTemplate = dateTemplate;
            TimeTemplate = timeTemplate;
        }

        public ScheduleTemplate(IDateScheduleTemplate dateTemplate)
        {
            DateTemplate = dateTemplate;
            TimeTemplate = new ExactTimeScheduleTemplate(0, 0);
        }
    }
}
