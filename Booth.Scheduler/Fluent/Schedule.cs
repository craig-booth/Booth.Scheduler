using System;
using System.Collections.Generic;
using System.Text;

namespace Booth.Scheduler.Fluent
{
    public class Schedule
    {
        public static DailySchedule EveryDay()
        {
            return new DailySchedule(1);
        }

        public static DailySchedule EveryDays(int every)
        {
            return new DailySchedule(every);
        }

        public static WeeklySchedule EveryWeek()
        {
            return new WeeklySchedule(1);
        }

        public static WeeklySchedule EveryWeeks(int every)
        {
            return new WeeklySchedule(every);
        }

        public static MonthlySchedule EveryMonth()
        {
            return new MonthlySchedule(1);
        }

        public static MonthlySchedule EveryMonths(int every)
        {
            return new MonthlySchedule(every);
        }

    }
}
