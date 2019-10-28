using System;
using System.Linq;

using NUnit.Framework;

namespace Booth.Scheduler.Test
{
    class ScheduleInstanceTests
    {
        [TestCase]
        public void DailyAt14_30()
        {
            var dateTemplate = new DailyScheduleTemplate();
            var timeTemplate = new ExactTimeScheduleTemplate(14, 30);
            var template = new ScheduleTemplate(dateTemplate, timeTemplate);

            var startDate = new DateTime(2019, 10, 23, 04, 34, 56);

            var schedule = new ScheduleInstance(template, startDate);

            var expected = new DateTime[] {
                new DateTime(2019, 10, 23, 14, 30, 00), 
                new DateTime(2019, 10, 24, 14, 30, 00),
                new DateTime(2019, 10, 25, 14, 30, 00),
                new DateTime(2019, 10, 26, 14, 30, 00),
                new DateTime(2019, 10, 27, 14, 30, 00)
            };

            Assert.That(schedule.Take(5), Is.EqualTo(expected));
        }

        [TestCase]
        public void DailyEvery3HoursFrom9To5()
        {
            var dateTemplate = new DailyScheduleTemplate();
            var timeTemplate = new HourlyScheduleTemplate(3);
            timeTemplate.From(9, 00);
            timeTemplate.From(17, 00);
            var template = new ScheduleTemplate(dateTemplate, timeTemplate);

            var startDate = new DateTime(2019, 10, 23, 04, 34, 56);

            var schedule = new ScheduleInstance(template, startDate);

            var expected = new DateTime[] {
                new DateTime(2019, 10, 23, 09, 00, 00),
                new DateTime(2019, 10, 23, 12, 00, 00),
                new DateTime(2019, 10, 23, 03, 00, 00),
                new DateTime(2019, 10, 23, 06, 00, 00),
                new DateTime(2019, 10, 24, 09, 00, 00),
                new DateTime(2019, 10, 24, 12, 00, 00),
                new DateTime(2019, 10, 24, 03, 00, 00),
                new DateTime(2019, 10, 24, 06, 00, 00)
            };

            Assert.That(schedule.Take(8), Is.EqualTo(expected));
        }

        [TestCase]
        public void WeeklyEveryMondayAt17_30()
        {
            var dateTemplate = new WeeklyScheduleTemplate(1);
            dateTemplate[DayOfWeek.Monday] = true;
            var timeTemplate = new ExactTimeScheduleTemplate(17, 30);

            var template = new ScheduleTemplate(dateTemplate, timeTemplate);

            var startDate = new DateTime(2019, 10, 23, 04, 34, 56);

            var schedule = new ScheduleInstance(template, startDate);

            var expected = new DateTime[] {
                new DateTime(2019, 10, 28, 17, 30, 00),
                new DateTime(2019, 11, 04, 17, 30, 00),
                new DateTime(2019, 11, 11, 17, 30, 00),
                new DateTime(2019, 11, 18, 17, 30, 00),
                new DateTime(2019, 11, 25, 17, 30, 00)
            };

            Assert.That(schedule.Take(5), Is.EqualTo(expected));
        }

    }
}
