using System;

using Booth.Scheduler;
using Booth.Scheduler.Fluent;

namespace Booth.Scheduler.Examples
{
    class Program
    {
        static void Main(string[] args)
        {
            // Schedule every 3 days
            var dailyTemplate = new DailyScheduleTemplate()
            {
                Every = 3
            };
            Schedule.EveryDays(3).At(9, 30);


         //   Schedule.EveryDays(3).Between(DateTime.Today, DateTime.Today.AddYears(1));

            // Schedule weekly on monday and friday
            var weeklyTemplate = new WeeklyScheduleTemplate();
            weeklyTemplate[DayOfWeek.Monday] = true;
            weeklyTemplate[DayOfWeek.Friday] = true;

            //    Schedule.EveryWeek().On(DayOfWeek.Monday).On(DayOfWeek.Friday);
            Schedule.EveryWeek().OnMonday().AndOnFriday().At(10, 30);

            //    Schedule.EveryMonth().On(Occurance.First, DayOfWeek.Thursday).At(9, 30);
            Schedule.EveryMonth().OnFirst(DayOfWeek.Thursday).At(9, 30);


            // Schedule.EveryDay().EveryHour().Between(9, 00, 17, 00);
            Schedule.EveryDay().EveryHour().From(9, 00).Until(17, 00);

            // Schedule every 3 days at 3am
            //  Schedule.Daily().Every(3, DateUnit.Days).At(3, 00);

            // Schedule every day to run hourly between 9am and 5pm
            //  Schedule.Daily().Every(2, TimeUnit.Hours).From(9, 00).To(17, 00);
            Schedule.EveryDay().EveryHours(2).From(9, 00).Until(17, 00);

            Console.WriteLine("Hello World!");
        }
    }

  /*  class DailySchedule : DateSchedule
    {
        // internal DailySchedule() : base(DateUnit.Days) { }

           public override DateSchedule Every(int count, DateUnit units)
            {
                if (units != DateUnit.Days)
                    throw new Exception("Units must be Days for a daily schedule");

                DateRepetition = new DateRepetition(count, units);

                return this;
            } */

        /*     public override bool ValidDate(DateTime date)
             {
                 return true;
             }

             public override DateTime FirstRunDate(DateTime after)
             {
                 return after.Date;
             }

             public override DateTime NextRunDate(DateTime after)
             {
                 return after.AddDays(DateRepetition.Count).Date;
             } 
    }*/


  /*  static class Schedule
    {
        public static DateSchedule Daily()
        {
            return new DailySchedule(1);
        }

        public static DateSchedule Daily(int every)
        {
            return new DailySchedule(every);
        }
    }

    class DailySchedule : DateSchedule
    {
        public DailySchedule(int every)
            : base(new DateTemplate(every, DateUnit.Days))
        {

        }
    }

    abstract class DateSchedule
    {
        protected readonly DateTemplate Template;
        public DateSchedule(DateTemplate template)
        {
            Template = template;
        }
        public DateSchedule Between(DateTime fromDate, DateTime toDate)
        {
            Template.FromDate = fromDate;
            Template.ToDate = toDate;

            return this;
        }
    }

    abstract class TimeSchedule
    {

    }


    class ScheduleInstance
    {
        public DateTemplate Template { get; }

        public ScheduleInstance(DateTemplate template)
        {
            Template = template;
        }
    }

    public enum DateUnit { Days, Weeks, Months, Years }

    class DateTemplate
    {
        public int Every { get; set; }
        public DateUnit Units { get; set; }

        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }

        public DateTemplate(int every, DateUnit units)
        {
            Every = every;
            Units = units;
            FromDate = new DateTime(0001, 01, 01);
            ToDate = new DateTime(9999, 12, 31);
        }
        public DateTemplate(int every, DateUnit units, DateTime fromDate, DateTime toDate)
        {
            Every = every;
            Units = units;
            FromDate = fromDate;
            ToDate = toDate;
        }

        public override string ToString()
        {
            if (Every == 1)
                return ScheduleDescriptions.DateUnitVerbDescriptions[(int)Units];
            else if (Every <= 4)
                return String.Format("every {0} {1}", ScheduleDescriptions.OccuranceDescription(Every), ScheduleDescriptions.DateUnitDescriptions[(int)Units]);
            else
                return String.Format("every {0} {1}", Every, ScheduleDescriptions.DateUnitPluralDescriptions[(int)Units]);
        }
    }
    public static class ScheduleDescriptions
    {
        public static string[] TimeUnitDescriptions = { "minute", "hour" };
        public static string[] TimeUnitPluralDescriptions = { "minutes", "hours" };
        public static string[] TimeUnitVerbDescriptions = { "every minute", "hourly" };

        public static string[] DateUnitDescriptions = { "day", "week", "month", "year" };
        public static string[] DateUnitPluralDescriptions = { "days", "weeks", "months", "years" };
        public static string[] DateUnitVerbDescriptions = { "daily", "weekly", "monthly", "annually" };
        public static string OccuranceDescription(int value)
        {
            if (value == 1)
                return "first";
            else if (value == 2)
                return "second";
            else if (value == 3)
                return "third";
            else if (value == 4)
                return "fourth";
            else
                return value.ToString();
        }
    }  */
}

