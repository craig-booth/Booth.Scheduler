using System;
using System.Linq;

using NUnit.Framework;

using Booth.Scheduler.Fluent;

namespace Booth.Scheduler.Test
{
    class FluentDailyTests
    {
        [TestCase]
        public void DailyAt14_30()
        {
            var schedule = Schedule.EveryDay().At(14, 30);

            var dateTemplate = schedule.DateTemplate;
            Assert.That(dateTemplate, Is.AssignableTo(typeof(DailyScheduleTemplate)));
            Assert.That(((DailyScheduleTemplate)dateTemplate).Every, Is.EqualTo(1));

            var timeTemplate = schedule.TimeTemplate;
            Assert.That(timeTemplate, Is.AssignableTo(typeof(ExactTimeScheduleTemplate)));
            Assert.That(((ExactTimeScheduleTemplate)timeTemplate).Time, Is.EqualTo(new TimeSpan(14, 30, 00)));

            Assert.That(schedule.ToString(), Is.EqualTo("Run every day, at 14:30"));
        }

        [TestCase]
        public void EveryFifthDayAt09_00()
        {
            var schedule = Schedule.EveryDays(5).At(09, 00);

            var dateTemplate = schedule.DateTemplate;
            Assert.That(dateTemplate, Is.AssignableTo(typeof(DailyScheduleTemplate)));
            Assert.That(((DailyScheduleTemplate)dateTemplate).Every, Is.EqualTo(5));

            var timeTemplate = schedule.TimeTemplate;
            Assert.That(timeTemplate, Is.AssignableTo(typeof(ExactTimeScheduleTemplate)));
            Assert.That(((ExactTimeScheduleTemplate)timeTemplate).Time, Is.EqualTo(new TimeSpan(09, 00, 00)));

            Assert.That(schedule.ToString(), Is.EqualTo("Run every 5 days, at 9:00"));
        }


        [TestCase]
        public void EveryDayEvery2HoursFrom9To5()
        {
            var schedule = Schedule.EveryDay().EveryHours(2).From(9, 00).Until(17, 00);

            var dateTemplate = schedule.DateTemplate;
            Assert.That(dateTemplate, Is.AssignableTo(typeof(DailyScheduleTemplate)));
            Assert.That(((DailyScheduleTemplate)dateTemplate).Every, Is.EqualTo(1));

            var timeTemplate = schedule.TimeTemplate;
            Assert.That(timeTemplate, Is.AssignableTo(typeof(HourlyScheduleTemplate)));
            Assert.That(((HourlyScheduleTemplate)timeTemplate).Every, Is.EqualTo(2));
            Assert.That(((HourlyScheduleTemplate)timeTemplate).FromTime, Is.EqualTo(new TimeSpan(09, 00, 00)));
            Assert.That(((HourlyScheduleTemplate)timeTemplate).ToTime, Is.EqualTo(new TimeSpan(17, 00, 00)));

            Assert.That(schedule.ToString(), Is.EqualTo("Run every day, every 2 hours between 9:00 and 17:00"));
        }

        [TestCase]
        public void Every2DaysEveryMinuteFrom9To5()
        {
            var schedule = Schedule.EveryDays(2).EveryMinute().From(9, 00).Until(17, 00);

            var dateTemplate = schedule.DateTemplate;
            Assert.That(dateTemplate, Is.AssignableTo(typeof(DailyScheduleTemplate)));
            Assert.That(((DailyScheduleTemplate)dateTemplate).Every, Is.EqualTo(2));

            var timeTemplate = schedule.TimeTemplate;
            Assert.That(timeTemplate, Is.AssignableTo(typeof(MinuteScheduleTemplate)));
            Assert.That(((MinuteScheduleTemplate)timeTemplate).Every, Is.EqualTo(1));
            Assert.That(((MinuteScheduleTemplate)timeTemplate).FromTime, Is.EqualTo(new TimeSpan(09, 00, 00)));
            Assert.That(((MinuteScheduleTemplate)timeTemplate).ToTime, Is.EqualTo(new TimeSpan(17, 00, 00)));

            Assert.That(schedule.ToString(), Is.EqualTo("Run every 2 days, every minute between 9:00 and 17:00"));
        }

        [TestCase]
        public void EveryDayEvery30MinutesFrom9To5()
        {
            var schedule = Schedule.EveryDay().EveryMinutes(30).From(9, 00).Until(17, 00);

            var dateTemplate = schedule.DateTemplate;
            Assert.That(dateTemplate, Is.AssignableTo(typeof(DailyScheduleTemplate)));
            Assert.That(((DailyScheduleTemplate)dateTemplate).Every, Is.EqualTo(1));

            var timeTemplate = schedule.TimeTemplate;
            Assert.That(timeTemplate, Is.AssignableTo(typeof(MinuteScheduleTemplate)));
            Assert.That(((MinuteScheduleTemplate)timeTemplate).Every, Is.EqualTo(30));
            Assert.That(((MinuteScheduleTemplate)timeTemplate).FromTime, Is.EqualTo(new TimeSpan(09, 00, 00)));
            Assert.That(((MinuteScheduleTemplate)timeTemplate).ToTime, Is.EqualTo(new TimeSpan(17, 00, 00)));

            Assert.That(schedule.ToString(), Is.EqualTo("Run every day, every 30 minutes between 9:00 and 17:00"));
        }
    }
}
