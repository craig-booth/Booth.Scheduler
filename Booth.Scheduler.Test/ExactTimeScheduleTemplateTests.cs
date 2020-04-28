using System;
using System.Linq;

using Xunit;
using FluentAssertions;

namespace Booth.Scheduler.Test
{
    public class ExactTimeScheduleTemplateTests
    {

        [Fact]
        public void OnlyRunTime()
        {
            var template = new ExactTimeScheduleTemplate(13, 32);

            var actual = template.GetTimes();

            actual.Should().Equal(new TimeSpan[] { new TimeSpan(13, 32, 00) });
        }

        [Fact]
        public void ValidateTimePositive()
        {
            var template = new ExactTimeScheduleTemplate(-1, 00);

            var errors = template.Validate();

            errors.Should().BeEquivalentTo(new string[] { "Time must be less than 1 day" });
        }

        [Fact]
        public void ValidateTimeLessThanOneDay()
        {
            var template = new ExactTimeScheduleTemplate(26, 80);

            var errors = template.Validate();

            errors.Should().BeEquivalentTo(new string[] { "Time must be less than 1 day" });
        }

    }
}
