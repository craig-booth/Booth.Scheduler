using System;
using System.Collections.Generic;
using System.Text;

namespace Booth.Scheduler.Fluent
{

    public class MonthlySchedule : DateSchedule
    {
        public MonthlySchedule(int every)
            : base(new DateTemplate(every, DateUnit.Months))
        {

        }

        public MonthlySchedule On(int day)
        {
            Template.Occurance = Occurance.None;
            Template.Flags[day] = true;
            return this;
        }

        public MonthlySchedule On(Occurance occurance, DayOfWeek day)
        {
            Template.Occurance = occurance;
            Template.Flags[(int)day] = true;
            return this;
        }
    }

}
