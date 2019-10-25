using System;
using System.Linq;

using NUnit.Framework;

namespace Booth.Scheduler.Test
{
    public class DailyScheduleTemplateTests
    {

        [TestCase]
        public void FirstRunDate()
        {
            var template = new DailyScheduleTemplate();

            var startDate = new DateTime(2000, 01, 01, 04, 34, 56);

            var dates = template.Schedule(startDate);

            Assert.That(dates.First(), Is.EqualTo(startDate.Date));
        }

        [TestCase]
        public void RunDateSequenceForEveryDay()
        {
            var template = new DailyScheduleTemplate();

            var startDate = new DateTime(2000, 01, 01, 04, 34, 56);

            var dates = template.Schedule(startDate).Take(5);

            var expected = new DateTime[] {
                startDate.Date,
                startDate.Date.AddDays(1),
                startDate.Date.AddDays(2),
                startDate.Date.AddDays(3),
                startDate.Date.AddDays(4)};
            Assert.That(dates, Is.EqualTo(expected));
        }

        [TestCase]
        public void RunDateSequenceForEveryThirdDay()
        {
            var template = new DailyScheduleTemplate(3);

            var startDate = new DateTime(2000, 01, 01, 04, 34, 56);

            var dates = template.Schedule(startDate).Take(5);

            var expected = new DateTime[] {
                startDate.Date,
                startDate.Date.AddDays(3),
                startDate.Date.AddDays(6),
                startDate.Date.AddDays(9),
                startDate.Date.AddDays(12)};
            Assert.That(dates, Is.EqualTo(expected));
        }

    }
}
