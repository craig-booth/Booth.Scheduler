using System;
using System.Collections.Generic;
using System.Text;

namespace Booth.Scheduler
{
    public class WeeklyScheduleTemplate : IDateScheduleTemplate
    {
        public int Every { get; set; }
        private bool[] _Days { get; } = new bool[7];

        public WeeklyScheduleTemplate() : this(1) { }

        public WeeklyScheduleTemplate(int every)
        {
            Every = every;
        }

        public bool this[DayOfWeek dayOfWeek]
        {
            get { return _Days[(int)dayOfWeek]; }
            set { _Days[(int)dayOfWeek] = value; }
        }
        public IDateScheduleEnumerator ScheduleEnumerator(DateTime start)
        {
            throw new NotImplementedException();
        }
    }
}
