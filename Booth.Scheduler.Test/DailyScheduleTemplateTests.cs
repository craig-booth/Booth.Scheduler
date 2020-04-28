using System;
using System.Linq;

using Xunit;
using FluentAssertions;

namespace Booth.Scheduler.Test
{
    public class DailyScheduleTemplateTests
    {

        [Fact]
        public void FirstRunDate()
        {
            var template = new DailyScheduleTemplate();

            var startDate = new DateTime(2000, 01, 01, 04, 34, 56);

            var actual = template.GetDates(startDate).First();

            actual.Should().Be(startDate.Date);
        }

        [Fact]
        public void RunDateSequenceForEveryDay()
        {
            var template = new DailyScheduleTemplate();

            var startDate = new DateTime(2000, 02, 26, 04, 34, 56);

            var actual = template.GetDates(startDate).Take(5);

            actual.Should().Equal(new DateTime[] {
                new DateTime(2000, 02, 26),
                new DateTime(2000, 02, 27),
                new DateTime(2000, 02, 28),
                new DateTime(2000, 02, 29),
                new DateTime(2000, 03, 01)});
        }

        [Fact]
        public void RunDateSequenceForEveryThirdDay()
        {
            var template = new DailyScheduleTemplate(3);

            var startDate = new DateTime(2000, 02, 26, 04, 34, 56);

            var actual = template.GetDates(startDate).Take(5);

            actual.Should().Equal(new DateTime[] {
                new DateTime(2000, 02, 26),
                new DateTime(2000, 02, 29),
                new DateTime(2000, 03, 03),
                new DateTime(2000, 03, 06),
                new DateTime(2000, 03, 09)});
        }

        [Fact]
        public void ValidateEveryNotZero()
        {
            var template = new DailyScheduleTemplate(0);

            var errors = template.Validate().ToList();

            errors.Should().BeEquivalentTo(new string[] { "Daily schedule must occur atleast every 1 days" });
        }

        [Fact]
        public void ValidateEveryNotLessThanZero()
        {
            var template = new DailyScheduleTemplate(-3);

            var errors = template.Validate().ToList();

            errors.Should().BeEquivalentTo(new string[] { "Daily schedule must occur atleast every 1 days" });
        }

    }
}
