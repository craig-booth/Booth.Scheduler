using System;
using System.Linq;

using Xunit;
using FluentAssertions;

namespace Booth.Scheduler.Test
{
    public class MonthlyScheduleDayNumberTemplateTests
    {
        [Fact]
        public void FirstRunDateOnCorrectDay()
        {
            var template = new MonthlyScheduleTemplate(1);
            template.DayNumber = 26;

            var startDate = new DateTime(2019, 10, 26, 04, 34, 56);

            var actual = template.GetDates(startDate).First();

            actual.Should().Be(startDate.Date);
        }

        [Fact]
        public void FirstRunDateAfterCorrectDay()
        {
            var template = new MonthlyScheduleTemplate(1);
            template.DayNumber = 26;

            var startDate = new DateTime(2019, 10, 28, 04, 34, 56);

            var actual = template.GetDates(startDate).First();

            actual.Should().Be(new DateTime(2019, 11, 26));
        }

        [Fact]
        public void FirstRunDateBeforeCorrectDay()
        {
            var template = new MonthlyScheduleTemplate(1);
            template.DayNumber = 26;

            var startDate = new DateTime(2019, 10, 02, 04, 34, 56);

            var actual = template.GetDates(startDate).First();

            actual.Should().Be(new DateTime(2019, 10, 26));
        }

        [Fact]
        public void RunEveryMonthOn15th()
        {
            var template = new MonthlyScheduleTemplate(1);
            template.DayNumber = 15;

            var startDate = new DateTime(2019, 10, 24, 04, 34, 56);

            var actual = template.GetDates(startDate).Take(5);

            actual.Should().Equal(new DateTime[] {
                new DateTime(2019, 11, 15),
                new DateTime(2019, 12, 15),
                new DateTime(2020, 01, 15),
                new DateTime(2020, 02, 15),
                new DateTime(2020, 03, 15)
            });
        }

        [Fact]
        public void RunEverySixMonthsOn10th()
        {
            var template = new MonthlyScheduleTemplate(6);
            template.DayNumber = 10;

            var startDate = new DateTime(2019, 10, 24, 04, 34, 56);

            var actual = template.GetDates(startDate).Take(5);

            actual.Should().Equal(new DateTime[] {
                new DateTime(2019, 11, 10),
                new DateTime(2020, 05, 10),
                new DateTime(2020, 11, 10),
                new DateTime(2021, 05, 10),
                new DateTime(2021, 11, 10)
            });
        }

        [Fact]
        public void RunEveryMonthOn31st()
        {
            var template = new MonthlyScheduleTemplate(1);
            template.DayNumber = 31;

            var startDate = new DateTime(2019, 10, 24, 04, 34, 56);

            var actual = template.GetDates(startDate).Take(12);

            actual.Should().Equal(new DateTime[] {
                new DateTime(2019, 10, 31),
                new DateTime(2019, 11, 30),
                new DateTime(2019, 12, 31),
                new DateTime(2020, 01, 31),
                new DateTime(2020, 02, 29),
                new DateTime(2020, 03, 31),
                new DateTime(2020, 04, 30),
                new DateTime(2020, 05, 31),
                new DateTime(2020, 06, 30),
                new DateTime(2020, 07, 31),
                new DateTime(2020, 08, 31),
                new DateTime(2020, 09, 30)
            });
        }

        [Fact]
        public void ValidateEveryNotZero()
        {
            var template = new MonthlyScheduleTemplate(0);

            var errors = template.Validate();

            errors.Should().Equal(new string[]
            {
                "Monthly schedule must occur atleast every 1 months",
                "Day number must be between 1 and 31"
            });
        }

        [Fact]
        public void ValidateEveryNotLessThanZero()
        {
            var template = new MonthlyScheduleTemplate(-1);

            var errors = template.Validate();

            errors.Should().BeEquivalentTo(new string[]
            {
                "Monthly schedule must occur atleast every 1 months",
                "Day number must be between 1 and 31"
            });

        }

        [Fact]
        public void ValidateDayNotLessThan1()
        {
            var template = new MonthlyScheduleTemplate(1);
            template.DayNumber = 0;

            var errors = template.Validate();

            errors.Should().BeEquivalentTo(new string[] { "Day number must be between 1 and 31" });
        }

        [Fact]
        public void ValidateDayNotGreateThan31()
        {
            var template = new MonthlyScheduleTemplate(1);
            template.DayNumber = 32;

            var errors = template.Validate();

            errors.Should().BeEquivalentTo(new string[] { "Day number must be between 1 and 31" });
        }

    }
}
