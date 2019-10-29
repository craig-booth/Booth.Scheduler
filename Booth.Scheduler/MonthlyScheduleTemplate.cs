using System;
using System.Collections.Generic;
using System.Text;

namespace Booth.Scheduler
{
    public enum Occurance { None, First, Second, Third, Fourth, Last }
    public class MonthlyScheduleTemplate : IDateScheduleTemplate
    {
        public int Every { get; set; }

        public int DayNumber { get; set; }
        public Occurance Occurance { get; set; }
        public DayOfWeek Day { get; set; }

        public MonthlyScheduleTemplate() : this(1) { }

        public MonthlyScheduleTemplate(int every)
        {
            Every = every;
        }

        public IEnumerable<DateTime> GetDates(DateTime start)
        {
            throw new NotImplementedException();
        }
    }
}
