using System;
using System.Collections.Generic;
using System.Text;

namespace Booth.Scheduler.Fluent
{
    public class WeeklySchedule : DateSchedule
    {
        public WeeklySchedule(int every)
            : base(new DateTemplate(every, DateUnit.Weeks))
        {

        }

        public WeeklySchedule On(DayOfWeek day)
        {
            Template.Flags[(int)day] = true;
            return this;
        }

        public WeeklySchedule OnWeekdays()
        {
            Template.Flags[(int)DayOfWeek.Sunday] = false;
            Template.Flags[(int)DayOfWeek.Monday] = true;
            Template.Flags[(int)DayOfWeek.Tuesday] = true;
            Template.Flags[(int)DayOfWeek.Wednesday] = true;
            Template.Flags[(int)DayOfWeek.Thursday] = true;
            Template.Flags[(int)DayOfWeek.Friday] = true;
            Template.Flags[(int)DayOfWeek.Saturday] = false;
            return this;
        }
    }
}
