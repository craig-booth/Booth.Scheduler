using System;
using System.Linq;

using NUnit.Framework;

namespace Booth.Scheduler.Test
{
    class ExactTimeScheduleTemplateTests
    {

        [TestCase]
        public void OnlyRunTime()
        {
            var template = new ExactTimeScheduleTemplate(13, 32);

            var actual = template.GetTimes();

            var expected = new TimeSpan[] {
                new TimeSpan(13, 32, 00)
            };

            Assert.That(actual, Is.EqualTo(expected));
        }

        [TestCase]
        public void ValidateTimePositive()
        {
            var template = new ExactTimeScheduleTemplate(-1, 00);

            var errors = template.Validate().ToList();
            Assert.That(errors, Has.Count.EqualTo(1));
            Assert.That(errors[0], Is.EqualTo("Time must be less than 1 day"));
        }

        [TestCase]
        public void ValidateTimeLessThanOneDay()
        {
            var template = new ExactTimeScheduleTemplate(26, 80);

            var errors = template.Validate().ToList();
            Assert.That(errors, Has.Count.EqualTo(1));
            Assert.That(errors[0], Is.EqualTo("Time must be less than 1 day"));
        }

    }
}
