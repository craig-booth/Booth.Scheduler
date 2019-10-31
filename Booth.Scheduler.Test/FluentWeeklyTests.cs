using System;
using System.Linq;

using NUnit.Framework;

using Booth.Scheduler.Fluent;

namespace Booth.Scheduler.Test
{
    class FluentWeeklyTests
    {

        [TestCase]
        public void EveryWeekOnMondayAt14_00()
        {
            var schedule = Schedule.EveryWeek().OnMonday().At(14, 00);

            var dateTemplate = schedule.DateTemplate;
            Assert.That(dateTemplate, Is.AssignableTo(typeof(WeeklyScheduleTemplate)));
            Assert.That(((WeeklyScheduleTemplate)dateTemplate).Every, Is.EqualTo(1));
            Assert.That(((WeeklyScheduleTemplate)dateTemplate)[DayOfWeek.Sunday], Is.EqualTo(false));
            Assert.That(((WeeklyScheduleTemplate)dateTemplate)[DayOfWeek.Monday], Is.EqualTo(true));
            Assert.That(((WeeklyScheduleTemplate)dateTemplate)[DayOfWeek.Tuesday], Is.EqualTo(false));
            Assert.That(((WeeklyScheduleTemplate)dateTemplate)[DayOfWeek.Wednesday], Is.EqualTo(false));
            Assert.That(((WeeklyScheduleTemplate)dateTemplate)[DayOfWeek.Thursday], Is.EqualTo(false));
            Assert.That(((WeeklyScheduleTemplate)dateTemplate)[DayOfWeek.Friday], Is.EqualTo(false));
            Assert.That(((WeeklyScheduleTemplate)dateTemplate)[DayOfWeek.Sunday], Is.EqualTo(false));

            var timeTemplate = schedule.TimeTemplate;
            Assert.That(timeTemplate, Is.AssignableTo(typeof(ExactTimeScheduleTemplate)));
            Assert.That(((ExactTimeScheduleTemplate)timeTemplate).Time, Is.EqualTo(new TimeSpan(14, 00, 00)));

            Assert.That(schedule.ToString(), Is.EqualTo("Run every week on Monday, at 14:00"));
        }

        [TestCase]
        public void EveryWeekOnWeekdaysAt14_00()
        {
            var schedule = Schedule.EveryWeek().OnWeekdays().At(14, 00);

            var dateTemplate = schedule.DateTemplate;
            Assert.That(dateTemplate, Is.AssignableTo(typeof(WeeklyScheduleTemplate)));
            Assert.That(((WeeklyScheduleTemplate)dateTemplate).Every, Is.EqualTo(1));
            Assert.That(((WeeklyScheduleTemplate)dateTemplate)[DayOfWeek.Sunday], Is.EqualTo(false));
            Assert.That(((WeeklyScheduleTemplate)dateTemplate)[DayOfWeek.Monday], Is.EqualTo(true));
            Assert.That(((WeeklyScheduleTemplate)dateTemplate)[DayOfWeek.Tuesday], Is.EqualTo(true));
            Assert.That(((WeeklyScheduleTemplate)dateTemplate)[DayOfWeek.Wednesday], Is.EqualTo(true));
            Assert.That(((WeeklyScheduleTemplate)dateTemplate)[DayOfWeek.Thursday], Is.EqualTo(true));
            Assert.That(((WeeklyScheduleTemplate)dateTemplate)[DayOfWeek.Friday], Is.EqualTo(true));
            Assert.That(((WeeklyScheduleTemplate)dateTemplate)[DayOfWeek.Sunday], Is.EqualTo(false));

            var timeTemplate = schedule.TimeTemplate;
            Assert.That(timeTemplate, Is.AssignableTo(typeof(ExactTimeScheduleTemplate)));
            Assert.That(((ExactTimeScheduleTemplate)timeTemplate).Time, Is.EqualTo(new TimeSpan(14, 00, 00)));

            Assert.That(schedule.ToString(), Is.EqualTo("Run every week on Monday, Tuesday, Wednesday, Thursday and Friday, at 14:00"));
        }

        [TestCase]
        public void EveryWeekOnWeekdayEvery2HoursFrom9to5()
        {
            var schedule = Schedule.EveryWeek().OnWeekdays().EveryHours(2).From(9, 00).Until(17, 00);

            var dateTemplate = schedule.DateTemplate;
            Assert.That(dateTemplate, Is.AssignableTo(typeof(WeeklyScheduleTemplate)));
            Assert.That(((WeeklyScheduleTemplate)dateTemplate).Every, Is.EqualTo(1));
            Assert.That(((WeeklyScheduleTemplate)dateTemplate)[DayOfWeek.Sunday], Is.EqualTo(false));
            Assert.That(((WeeklyScheduleTemplate)dateTemplate)[DayOfWeek.Monday], Is.EqualTo(true));
            Assert.That(((WeeklyScheduleTemplate)dateTemplate)[DayOfWeek.Tuesday], Is.EqualTo(true));
            Assert.That(((WeeklyScheduleTemplate)dateTemplate)[DayOfWeek.Wednesday], Is.EqualTo(true));
            Assert.That(((WeeklyScheduleTemplate)dateTemplate)[DayOfWeek.Thursday], Is.EqualTo(true));
            Assert.That(((WeeklyScheduleTemplate)dateTemplate)[DayOfWeek.Friday], Is.EqualTo(true));
            Assert.That(((WeeklyScheduleTemplate)dateTemplate)[DayOfWeek.Sunday], Is.EqualTo(false));

            var timeTemplate = schedule.TimeTemplate;
            Assert.That(timeTemplate, Is.AssignableTo(typeof(HourlyScheduleTemplate)));
            Assert.That(((HourlyScheduleTemplate)timeTemplate).Every, Is.EqualTo(2));
            Assert.That(((HourlyScheduleTemplate)timeTemplate).FromTime, Is.EqualTo(new TimeSpan(09, 00, 00)));
            Assert.That(((HourlyScheduleTemplate)timeTemplate).ToTime, Is.EqualTo(new TimeSpan(17, 00, 00)));

            Assert.That(schedule.ToString(), Is.EqualTo("Run every week on Monday, Tuesday, Wednesday, Thursday and Friday, every 2 hours between 9:00 and 17:00"));
        }


        [TestCase]
        public void Every2WeeksOnTuesdayWednesdayAndThursdayAt7()
        {
            var schedule = Schedule.EveryWeeks(2).OnTuesday().AndOnWednesday().AndOnThursday().At(7, 00);

            var dateTemplate = schedule.DateTemplate;
            Assert.That(dateTemplate, Is.AssignableTo(typeof(WeeklyScheduleTemplate)));
            Assert.That(((WeeklyScheduleTemplate)dateTemplate).Every, Is.EqualTo(2));
            Assert.That(((WeeklyScheduleTemplate)dateTemplate)[DayOfWeek.Sunday], Is.EqualTo(false));
            Assert.That(((WeeklyScheduleTemplate)dateTemplate)[DayOfWeek.Monday], Is.EqualTo(false));
            Assert.That(((WeeklyScheduleTemplate)dateTemplate)[DayOfWeek.Tuesday], Is.EqualTo(true));
            Assert.That(((WeeklyScheduleTemplate)dateTemplate)[DayOfWeek.Wednesday], Is.EqualTo(true));
            Assert.That(((WeeklyScheduleTemplate)dateTemplate)[DayOfWeek.Thursday], Is.EqualTo(true));
            Assert.That(((WeeklyScheduleTemplate)dateTemplate)[DayOfWeek.Friday], Is.EqualTo(false));
            Assert.That(((WeeklyScheduleTemplate)dateTemplate)[DayOfWeek.Sunday], Is.EqualTo(false));

            var timeTemplate = schedule.TimeTemplate;
            Assert.That(timeTemplate, Is.AssignableTo(typeof(ExactTimeScheduleTemplate)));
            Assert.That(((ExactTimeScheduleTemplate)timeTemplate).Time, Is.EqualTo(new TimeSpan(07, 00, 00)));

            Assert.That(schedule.ToString(), Is.EqualTo("Run every 2 weeks on Tuesday, Wednesday and Thursday, at 7:00"));
        }

    }
}
