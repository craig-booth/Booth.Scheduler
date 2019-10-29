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

    }
}
