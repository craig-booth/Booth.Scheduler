using System;
using System.Collections;
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
        public IEnumerable<TimeSpan> GetTimes()
        {
            yield return Time;
        }

        public IEnumerable<string> Validate()
        {
            if ((Time.Ticks < 0) || (Time.Days >= 1))
                yield return "Time must be less than 1 day";
        }

        public override string ToString()
        {
            return "at " + Time.ToString(@"h\:mm");
        }
    }
}
