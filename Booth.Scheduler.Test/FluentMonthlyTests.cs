﻿using System;
using System.Linq;

using NUnit.Framework;

using Booth.Scheduler.Fluent;

namespace Booth.Scheduler.Test
{
    class FluentMonthlyTests
    {


        [TestCase]
        public void EveryMonthOn20thAt7()
        {
            var schedule = Schedule.EveryMonth().OnDay(20).At(7, 00);

            var dateTemplate = schedule.DateTemplate;
            Assert.That(dateTemplate, Is.AssignableTo(typeof(MonthlyScheduleTemplate)));
            Assert.That(((MonthlyScheduleTemplate)dateTemplate).Every, Is.EqualTo(1));
            Assert.That(((MonthlyScheduleTemplate)dateTemplate).OccuranceType, Is.EqualTo(OccuranceType.None));
            Assert.That(((MonthlyScheduleTemplate)dateTemplate).DayNumber, Is.EqualTo(20));

            var timeTemplate = schedule.TimeTemplate;
            Assert.That(timeTemplate, Is.AssignableTo(typeof(ExactTimeScheduleTemplate)));
            Assert.That(((ExactTimeScheduleTemplate)timeTemplate).Time, Is.EqualTo(new TimeSpan(07, 00, 00)));
        }

        [TestCase]
        public void EveryMonthOnFirstMondayAt7()
        {
            var schedule = Schedule.EveryMonth().OnFirst(DayOfWeek.Monday).At(7, 00);

            var dateTemplate = schedule.DateTemplate;
            Assert.That(dateTemplate, Is.AssignableTo(typeof(MonthlyScheduleTemplate)));
            Assert.That(((MonthlyScheduleTemplate)dateTemplate).Every, Is.EqualTo(1));
            Assert.That(((MonthlyScheduleTemplate)dateTemplate).OccuranceType, Is.EqualTo(OccuranceType.DayOfWeek));
            Assert.That(((MonthlyScheduleTemplate)dateTemplate).Occurance, Is.EqualTo(Occurance.First));
            Assert.That(((MonthlyScheduleTemplate)dateTemplate).Day, Is.EqualTo(DayOfWeek.Monday));

            var timeTemplate = schedule.TimeTemplate;
            Assert.That(timeTemplate, Is.AssignableTo(typeof(ExactTimeScheduleTemplate)));
            Assert.That(((ExactTimeScheduleTemplate)timeTemplate).Time, Is.EqualTo(new TimeSpan(07, 00, 00)));
        }

        [TestCase]
        public void EveryMonthOnSecondWednesdayAt7()
        {
            var schedule = Schedule.EveryMonth().OnSecond(DayOfWeek.Wednesday).At(7, 00);

            var dateTemplate = schedule.DateTemplate;
            Assert.That(dateTemplate, Is.AssignableTo(typeof(MonthlyScheduleTemplate)));
            Assert.That(((MonthlyScheduleTemplate)dateTemplate).Every, Is.EqualTo(1));
            Assert.That(((MonthlyScheduleTemplate)dateTemplate).OccuranceType, Is.EqualTo(OccuranceType.DayOfWeek));
            Assert.That(((MonthlyScheduleTemplate)dateTemplate).Occurance, Is.EqualTo(Occurance.Second));
            Assert.That(((MonthlyScheduleTemplate)dateTemplate).Day, Is.EqualTo(DayOfWeek.Wednesday));

            var timeTemplate = schedule.TimeTemplate;
            Assert.That(timeTemplate, Is.AssignableTo(typeof(ExactTimeScheduleTemplate)));
            Assert.That(((ExactTimeScheduleTemplate)timeTemplate).Time, Is.EqualTo(new TimeSpan(07, 00, 00)));
        }

        [TestCase]
        public void EveryMonthOnThirdSaturdayAt7()
        {
            var schedule = Schedule.EveryMonth().OnThird(DayOfWeek.Saturday).At(7, 00);

            var dateTemplate = schedule.DateTemplate;
            Assert.That(dateTemplate, Is.AssignableTo(typeof(MonthlyScheduleTemplate)));
            Assert.That(((MonthlyScheduleTemplate)dateTemplate).Every, Is.EqualTo(1));
            Assert.That(((MonthlyScheduleTemplate)dateTemplate).OccuranceType, Is.EqualTo(OccuranceType.DayOfWeek));
            Assert.That(((MonthlyScheduleTemplate)dateTemplate).Occurance, Is.EqualTo(Occurance.Third));
            Assert.That(((MonthlyScheduleTemplate)dateTemplate).Day, Is.EqualTo(DayOfWeek.Saturday));

            var timeTemplate = schedule.TimeTemplate;
            Assert.That(timeTemplate, Is.AssignableTo(typeof(ExactTimeScheduleTemplate)));
            Assert.That(((ExactTimeScheduleTemplate)timeTemplate).Time, Is.EqualTo(new TimeSpan(07, 00, 00)));
        }

        [TestCase]
        public void EveryMonthOnFourthSundayAt7()
        {
            var schedule = Schedule.EveryMonth().OnFourth(DayOfWeek.Sunday).At(7, 00);

            var dateTemplate = schedule.DateTemplate;
            Assert.That(dateTemplate, Is.AssignableTo(typeof(MonthlyScheduleTemplate)));
            Assert.That(((MonthlyScheduleTemplate)dateTemplate).Every, Is.EqualTo(1));
            Assert.That(((MonthlyScheduleTemplate)dateTemplate).OccuranceType, Is.EqualTo(OccuranceType.DayOfWeek));
            Assert.That(((MonthlyScheduleTemplate)dateTemplate).Occurance, Is.EqualTo(Occurance.Fourth));
            Assert.That(((MonthlyScheduleTemplate)dateTemplate).Day, Is.EqualTo(DayOfWeek.Sunday));

            var timeTemplate = schedule.TimeTemplate;
            Assert.That(timeTemplate, Is.AssignableTo(typeof(ExactTimeScheduleTemplate)));
            Assert.That(((ExactTimeScheduleTemplate)timeTemplate).Time, Is.EqualTo(new TimeSpan(07, 00, 00)));
        }


        [TestCase]
        public void Every2MonthsOnLastFridayAt7()
        {
            var schedule = Schedule.EveryMonths(2).OnLast(DayOfWeek.Friday).At(7, 0);

            var dateTemplate = schedule.DateTemplate;
            Assert.That(dateTemplate, Is.AssignableTo(typeof(MonthlyScheduleTemplate)));
            Assert.That(((MonthlyScheduleTemplate)dateTemplate).Every, Is.EqualTo(2));
            Assert.That(((MonthlyScheduleTemplate)dateTemplate).OccuranceType, Is.EqualTo(OccuranceType.DayOfWeek));
            Assert.That(((MonthlyScheduleTemplate)dateTemplate).Occurance, Is.EqualTo(Occurance.Last));
            Assert.That(((MonthlyScheduleTemplate)dateTemplate).Day, Is.EqualTo(DayOfWeek.Friday));

            var timeTemplate = schedule.TimeTemplate;
            Assert.That(timeTemplate, Is.AssignableTo(typeof(ExactTimeScheduleTemplate)));
            Assert.That(((ExactTimeScheduleTemplate)timeTemplate).Time, Is.EqualTo(new TimeSpan(07, 00, 00)));
        }

        [TestCase]
        public void Every2MonthsOnLastDayAt7()
        {
            var schedule = Schedule.EveryMonths(2).OnLastDay().At(7, 0);

            var dateTemplate = schedule.DateTemplate;
            Assert.That(dateTemplate, Is.AssignableTo(typeof(MonthlyScheduleTemplate)));
            Assert.That(((MonthlyScheduleTemplate)dateTemplate).Every, Is.EqualTo(2));
            Assert.That(((MonthlyScheduleTemplate)dateTemplate).OccuranceType, Is.EqualTo(OccuranceType.Day));
            Assert.That(((MonthlyScheduleTemplate)dateTemplate).Occurance, Is.EqualTo(Occurance.Last));

            var timeTemplate = schedule.TimeTemplate;
            Assert.That(timeTemplate, Is.AssignableTo(typeof(ExactTimeScheduleTemplate)));
            Assert.That(((ExactTimeScheduleTemplate)timeTemplate).Time, Is.EqualTo(new TimeSpan(07, 00, 00)));
        }

        [TestCase]
        public void Every2MonthsOnFirstWeekdayAt7()
        {
            var schedule = Schedule.EveryMonths(2).OnFirstWeekday().At(7, 0);

            var dateTemplate = schedule.DateTemplate;
            Assert.That(dateTemplate, Is.AssignableTo(typeof(MonthlyScheduleTemplate)));
            Assert.That(((MonthlyScheduleTemplate)dateTemplate).Every, Is.EqualTo(2));
            Assert.That(((MonthlyScheduleTemplate)dateTemplate).OccuranceType, Is.EqualTo(OccuranceType.Weekday));
            Assert.That(((MonthlyScheduleTemplate)dateTemplate).Occurance, Is.EqualTo(Occurance.First));

            var timeTemplate = schedule.TimeTemplate;
            Assert.That(timeTemplate, Is.AssignableTo(typeof(ExactTimeScheduleTemplate)));
            Assert.That(((ExactTimeScheduleTemplate)timeTemplate).Time, Is.EqualTo(new TimeSpan(07, 00, 00)));
        }

        [TestCase]
        public void EveryMonthOnLastWeekdayEveryHourBetween9_17()
        {
            var schedule = Schedule.EveryMonths(2).OnLastWeekday().EveryHour().From(9, 00).Until(17, 00);

            var dateTemplate = schedule.DateTemplate;
            Assert.That(dateTemplate, Is.AssignableTo(typeof(MonthlyScheduleTemplate)));
            Assert.That(((MonthlyScheduleTemplate)dateTemplate).Every, Is.EqualTo(2));
            Assert.That(((MonthlyScheduleTemplate)dateTemplate).OccuranceType, Is.EqualTo(OccuranceType.Weekday));
            Assert.That(((MonthlyScheduleTemplate)dateTemplate).Occurance, Is.EqualTo(Occurance.Last));

            var timeTemplate = schedule.TimeTemplate;
            Assert.That(timeTemplate, Is.AssignableTo(typeof(HourlyScheduleTemplate)));
            Assert.That(((HourlyScheduleTemplate)timeTemplate).Every, Is.EqualTo(1));
            Assert.That(((HourlyScheduleTemplate)timeTemplate).FromTime, Is.EqualTo(new TimeSpan(09, 00, 00)));
            Assert.That(((HourlyScheduleTemplate)timeTemplate).ToTime, Is.EqualTo(new TimeSpan(17, 00, 00)));
        }
    }
}