using System;
using System.Linq;

using Xunit;
using FluentAssertions;

using Booth.Common;

namespace Booth.Scheduler.Test
{
    public class MonthlySchedulePatternTemplateTests
    {
        [Fact]
        public void FirstRunDateOnFirstMonday()
        {
            var template = new MonthlyScheduleTemplate(1);
            template.OccuranceType = OccuranceType.DayOfWeek;
            template.Occurance = Occurance.First;
            template.Day = DayOfWeek.Monday;

            var startDate = new Date(2019, 10, 01);

            var actual = template.GetDates(startDate).First();

            actual.Should().Be(new Date(2019, 10, 07));
        }

        [Fact]
        public void FirstRunDateOnSecondWednesday()
        {
            var template = new MonthlyScheduleTemplate(1);
            template.OccuranceType = OccuranceType.DayOfWeek;
            template.Occurance = Occurance.Second;
            template.Day = DayOfWeek.Wednesday;

            var startDate = new Date(2019, 10, 09);

            var actual = template.GetDates(startDate).First();

            actual.Should().Be(new Date(2019, 10, 09));
        }

        [Fact]
        public void FirstRunDateOnThirdFriday()
        {
            var template = new MonthlyScheduleTemplate(1);
            template.OccuranceType = OccuranceType.DayOfWeek;
            template.Occurance = Occurance.Third;
            template.Day = DayOfWeek.Friday;

            var startDate = new Date(2019, 10, 26);

            var actual = template.GetDates(startDate).First();

            actual.Should().Be(new Date(2019, 11, 15));
        }

        [Fact]
        public void FirstRunDateOnFourthSaturday()
        {
            var template = new MonthlyScheduleTemplate(1);
            template.OccuranceType = OccuranceType.DayOfWeek;
            template.Occurance = Occurance.Fourth;
            template.Day = DayOfWeek.Saturday;

            var startDate = new Date(2019, 10, 26);

            var actual = template.GetDates(startDate).First();

            actual.Should().Be(new Date(2019, 10, 26));
        }

        [Fact]
        public void FirstRunDateOnLastWednesday()
        {
            var template = new MonthlyScheduleTemplate(1);
            template.OccuranceType = OccuranceType.DayOfWeek;
            template.Occurance = Occurance.Last;
            template.Day = DayOfWeek.Wednesday;

            var startDate = new Date(2019, 10, 26);

            var actual = template.GetDates(startDate).First();

            actual.Should().Be(new Date(2019, 10, 30));
        }

        [Fact]
        public void RunEvery3MonthsOnSecondTuesday()
        {
            var template = new MonthlyScheduleTemplate(3);
            template.OccuranceType = OccuranceType.DayOfWeek;
            template.Occurance = Occurance.Second;
            template.Day = DayOfWeek.Tuesday;

            var startDate = new Date(2019, 10, 24);

            var actual = template.GetDates(startDate).Take(5);

            actual.Should().Equal(new Date[] {
                new Date(2019, 11, 12),
                new Date(2020, 02, 11),
                new Date(2020, 05, 12),
                new Date(2020, 08, 11),
                new Date(2020, 11, 10)
            });
        }

        [Fact]
        public void RunEveryMonthOnLastThursday()
        {
            var template = new MonthlyScheduleTemplate(1);
            template.OccuranceType = OccuranceType.DayOfWeek;
            template.Occurance = Occurance.Last;
            template.Day = DayOfWeek.Thursday;

            var startDate = new Date(2019, 10, 24);

            var actual = template.GetDates(startDate).Take(5);

            actual.Should().Equal(new Date[] {
                new Date(2019, 10, 31),
                new Date(2019, 11, 28),
                new Date(2019, 12, 26),
                new Date(2020, 01, 30),
                new Date(2020, 02, 27)
            });
        }

        [Fact]
        public void RunEvery3MonthsOnFirstDay()
        {
            var template = new MonthlyScheduleTemplate(3);
            template.OccuranceType = OccuranceType.Day;
            template.Occurance = Occurance.First;

            var startDate = new Date(2019, 10, 24);

            var actual = template.GetDates(startDate).Take(5);

            actual.Should().Equal(new Date[] {
                new Date(2019, 11, 01),
                new Date(2020, 02, 01),
                new Date(2020, 05, 01),
                new Date(2020, 08, 01),
                new Date(2020, 11, 01)
            });
        }

        [Fact]
        public void RunEvery2MonthsOnLastDay()
        {
            var template = new MonthlyScheduleTemplate(2);
            template.OccuranceType = OccuranceType.Day;
            template.Occurance = Occurance.Last;

            var startDate = new Date(2019, 10, 24);

            var actual = template.GetDates(startDate).Take(5);

            actual.Should().Equal(new Date[] {
                new Date(2019, 10, 31),
                new Date(2019, 12, 31),
                new Date(2020, 02, 29),
                new Date(2020, 04, 30),
                new Date(2020, 06, 30)
            });
        }

        [Fact]
        public void RunEvery2MonthsOnFirstWeekDay()
        {
            var template = new MonthlyScheduleTemplate(2);
            template.OccuranceType = OccuranceType.Weekday;
            template.Occurance = Occurance.First;

            var startDate = new Date(2019, 10, 24);

            var actual = template.GetDates(startDate).Take(5);

            actual.Should().Equal(new Date[] {
                new Date(2019, 11, 01),
                new Date(2020, 01, 01),
                new Date(2020, 03, 02),
                new Date(2020, 05, 01),
                new Date(2020, 07, 01)
            });
        }

        [Fact]
        public void RunEveryMonthOnLastWeekDay()
        {
            var template = new MonthlyScheduleTemplate(1);
            template.OccuranceType = OccuranceType.Weekday;
            template.Occurance = Occurance.Last;

            var startDate = new Date(2019, 10, 24);

            var actual = template.GetDates(startDate).Take(5);

            actual.Should().Equal(new Date[] {
                new Date(2019, 10, 31),
                new Date(2019, 11, 29),
                new Date(2019, 12, 31),
                new Date(2020, 01, 31),
                new Date(2020, 02, 28)
            });
        }

        [Fact]
        public void ValidateEveryNotZero()
        {
            var template = new MonthlyScheduleTemplate(0);
            template.OccuranceType = OccuranceType.DayOfWeek;

            var errors = template.Validate();

            errors.Should().BeEquivalentTo(new string[] { "Monthly schedule must occur atleast every 1 months" });
        }

        [Fact]
        public void ValidateEveryNotLessThanZero()
        {
            var template = new MonthlyScheduleTemplate(-1);
            template.OccuranceType = OccuranceType.DayOfWeek;

            var errors = template.Validate();

            errors.Should().BeEquivalentTo(new string[] { "Monthly schedule must occur atleast every 1 months" });
        }

    }
}
