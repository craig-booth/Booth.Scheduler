using System;
using System.Collections.Generic;
using System.Text;

namespace Booth.Scheduler
{
    public class ExactTimeScheduleTemplate : ITimeScheduleTemplate
    {
        public TimeSpan Time { get; set; }

        public ExactTimeScheduleTemplate(int hour, int minute)
        {
            Time = new TimeSpan(hour, minute, 0);
        }

        public ITimeScheduleEnumerator ScheduleEnumerator(DateTime date)
        {
            throw new NotSupportedException();
        }
    }
}
