using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

using Booth.Common;

namespace Booth.Scheduler
{
    public class ExactTimeScheduleTemplate : ITimeScheduleTemplate
    {
        public Time Time { get; set; }

        public ExactTimeScheduleTemplate(int hour, int minute)
        {
            Time = new Time(hour, minute, 0);
        }
        public IEnumerable<Time> GetTimes()
        {
            yield return Time;
        }

        public IEnumerable<string> Validate()
        {
            return new string[0];
        }

        public override string ToString()
        {
            return "at " + Time.ToString(@"H\:mm");
        }
    }
}
