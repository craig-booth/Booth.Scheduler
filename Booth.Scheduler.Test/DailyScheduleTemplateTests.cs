using System;
using System.Linq;

using NUnit.Framework;

namespace Booth.Scheduler.Test
{
    class DailyScheduleTemplateTests
    {

        [TestCase]
        public void FirstRunDate()
        {
            var template = new DailyScheduleTemplate();

            var startDate = new DateTime(2000, 01, 01, 04, 34, 56);

            var actual = template.GetDates(startDate).First();

            Assert.That(actual, Is.EqualTo(startDate.Date));
        }

        [TestCase]
        public void RunDateSequenceForEveryDay()
        {
            var template = new DailyScheduleTemplate();

            var startDate = new DateTime(2000, 02, 26, 04, 34, 56);

            var actual = template.GetDates(startDate).Take(5);

            var expected = new DateTime[] {
                new DateTime(2000, 02, 26),
                new DateTime(2000, 02, 27),
                new DateTime(2000, 02, 28),
                new DateTime(2000, 02, 29),
                new DateTime(2000, 03, 01)};
            Assert.That(actual, Is.EqualTo(expected));
        }

        [TestCase]
        public void RunDateSequenceForEveryThirdDay()
        {
            var template = new DailyScheduleTemplate(3);

            var startDate = new DateTime(2000, 02, 26, 04, 34, 56);

            var actual = template.GetDates(startDate).Take(5);

            var expected = new DateTime[] {
                new DateTime(2000, 02, 26),
                new DateTime(2000, 02, 29),
                new DateTime(2000, 03, 03),
                new DateTime(2000, 03, 06),
                new DateTime(2000, 03, 09)};
            Assert.That(actual, Is.EqualTo(expected));
        }

    }
}
