using System;
using System.Collections.Generic;
using System.Text;

namespace Booth.Scheduler
{
    public enum TimeUnit { Hours, Minutes }
    public class TimeTemplate
    {
        public TimeSpan TimeSpan { get; }
        public int Every { get; }
        public TimeUnit Units { get; }

        public TimeSpan FromTime { get; set; }
        public TimeSpan ToTime { get; set;  }


        public TimeTemplate(int hour, int minute)
        {
            TimeSpan = new TimeSpan(hour, minute, 0);
            FromTime = new TimeSpan(0);
            ToTime = new TimeSpan(0);
        }
        public TimeTemplate(int every, TimeUnit units)
        {
            Every = every;
            Units = units;
            FromTime = new TimeSpan(0);
            ToTime = new TimeSpan(0);
        }
    }
}
