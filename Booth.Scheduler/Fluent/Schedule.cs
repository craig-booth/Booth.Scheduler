using System;
using System.Collections.Generic;
using System.Text;

namespace Booth.Scheduler.Fluent
{
    public class Schedule
    {
        public static DayScheduleBuilder EveryDay()
        {
            var template = new ScheduleTemplate(new DailyScheduleTemplate());

            return new DayScheduleBuilder(template);
        }

        public static DayScheduleBuilder EveryDays(int every)
        {
            var template = new ScheduleTemplate(new DailyScheduleTemplate(every));

            return new DayScheduleBuilder(template);
        }

        public static WeekScheduleBuilder EveryWeek()
        {
            var template = new ScheduleTemplate(new WeeklyScheduleTemplate());

            return new WeekScheduleBuilder(template);
        }

        public static WeekScheduleBuilder EveryWeeks(int every)
        {
            var template = new ScheduleTemplate(new WeeklyScheduleTemplate(every));

            return new WeekScheduleBuilder(template);
        }
        
        public static MonthScheduleBuilder EveryMonth()
        {
            var template = new ScheduleTemplate(new MonthlyScheduleTemplate());

            return new MonthScheduleBuilder(template);
        }

        public static MonthScheduleBuilder EveryMonths(int every)
        {
            var template = new ScheduleTemplate(new MonthlyScheduleTemplate(every));

            return new MonthScheduleBuilder(template);
        }
         
    }
}
