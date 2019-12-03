using System;
using System.Linq;

using Booth.Common;

using NUnit.Framework;

namespace Booth.Scheduler.Test
{
    class DailyScheduleTemplateTests
    {

        [TestCase]
        public void FirstRunDate()
        {
            var template = new DailyScheduleTemplate();

            var startDate = new Date(2000, 01, 01);

            var actual = template.GetDates(startDate).First();

            Assert.That(actual, Is.EqualTo(startDate));
        }

        [TestCase]
        public void RunDateSequenceForEveryDay()
        {
            var template = new DailyScheduleTemplate();

            var startDate = new Date(2000, 02, 26);

            var actual = template.GetDates(startDate).Take(5);

            var expected = new Date[] {
                new Date(2000, 02, 26),
                new Date(2000, 02, 27),
                new Date(2000, 02, 28),
                new Date(2000, 02, 29),
                new Date(2000, 03, 01)};
            Assert.That(actual, Is.EqualTo(expected));
        }

        [TestCase]
        public void RunDateSequenceForEveryThirdDay()
        {
            var template = new DailyScheduleTemplate(3);

            var startDate = new Date(2000, 02, 26);

            var actual = template.GetDates(startDate).Take(5);

            var expected = new Date[] {
                new Date(2000, 02, 26),
                new Date(2000, 02, 29),
                new Date(2000, 03, 03),
                new Date(2000, 03, 06),
                new Date(2000, 03, 09)};
            Assert.That(actual, Is.EqualTo(expected));
        }

        [TestCase]
        public void ValidateEveryNotZero()
        {
            var template = new DailyScheduleTemplate(0);

            var errors = template.Validate().ToList();
            Assert.That(errors, Has.Count.EqualTo(1));
            Assert.That(errors[0], Is.EqualTo("Daily schedule must occur atleast every 1 days"));
        }

        [TestCase]
        public void ValidateEveryNotLessThanZero()
        {
            var template = new DailyScheduleTemplate(-3);

            var errors = template.Validate().ToList();
            Assert.That(errors, Has.Count.EqualTo(1));
            Assert.That(errors[0], Is.EqualTo("Daily schedule must occur atleast every 1 days"));
        }

    }
}
