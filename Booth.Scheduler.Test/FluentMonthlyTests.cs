using System;
using System.Linq;

using Xunit;
using FluentAssertions;
using FluentAssertions.Execution;

using Booth.Scheduler.Fluent;

namespace Booth.Scheduler.Test
{
    public class FluentMonthlyTests
    {


        [Fact]
        public void EveryMonthOn20thAt7()
        {
            var schedule = Schedule.EveryMonth().OnDay(20).At(7, 00);

            using (new AssertionScope())
            {
                schedule.DateTemplate.Should().BeOfType<MonthlyScheduleTemplate>().And.BeEquivalentTo(new { Every = 1, OccuranceType = OccuranceType.None, DayNumber = 20 });
                schedule.TimeTemplate.Should().BeOfType<ExactTimeScheduleTemplate>().And.BeEquivalentTo(new { Time = new TimeSpan(07, 00, 00) });
                schedule.ToString().Should().Be("Run on the 20th of every month, at 7:00");
            }
        }

        [Fact]
        public void EveryMonthOnFirstMondayAt7()
        {
            var schedule = Schedule.EveryMonth().OnFirst(DayOfWeek.Monday).At(7, 00);

            using (new AssertionScope())
            {
                schedule.DateTemplate.Should().BeOfType<MonthlyScheduleTemplate>().And.BeEquivalentTo(new { Every = 1, OccuranceType = OccuranceType.DayOfWeek, Occurance = Occurance.First, Day = DayOfWeek.Monday });
                schedule.TimeTemplate.Should().BeOfType<ExactTimeScheduleTemplate>().And.BeEquivalentTo(new { Time = new TimeSpan(07, 00, 00) });
                schedule.ToString().Should().Be("Run on the first Monday of every month, at 7:00");
            }
        }

        [Fact]
        public void Every2MonthsOnSecondWednesdayAt7()
        {
            var schedule = Schedule.EveryMonths(2).OnSecond(DayOfWeek.Wednesday).At(7, 00);

            using (new AssertionScope())
            {
                schedule.DateTemplate.Should().BeOfType<MonthlyScheduleTemplate>().And.BeEquivalentTo(new { Every = 2, OccuranceType = OccuranceType.DayOfWeek, Occurance = Occurance.Second, Day = DayOfWeek.Wednesday });
                schedule.TimeTemplate.Should().BeOfType<ExactTimeScheduleTemplate>().And.BeEquivalentTo(new { Time = new TimeSpan(07, 00, 00) });
                schedule.ToString().Should().Be("Run on the second Wednesday of every 2 months, at 7:00");
            }
        }

        [Fact]
        public void EveryMonthOnThirdSaturdayAt7()
        {
            var schedule = Schedule.EveryMonth().OnThird(DayOfWeek.Saturday).At(7, 00);

            using (new AssertionScope())
            {
                schedule.DateTemplate.Should().BeOfType<MonthlyScheduleTemplate>().And.BeEquivalentTo(new { Every = 1, OccuranceType = OccuranceType.DayOfWeek, Occurance = Occurance.Third, Day = DayOfWeek.Saturday });
                schedule.TimeTemplate.Should().BeOfType<ExactTimeScheduleTemplate>().And.BeEquivalentTo(new { Time = new TimeSpan(07, 00, 00) });
                schedule.ToString().Should().Be("Run on the third Saturday of every month, at 7:00");
            }
        }

        [Fact]
        public void EveryMonthOnFourthSundayAt7()
        {
            var schedule = Schedule.EveryMonth().OnFourth(DayOfWeek.Sunday).At(7, 00);

            using (new AssertionScope())
            {
                schedule.DateTemplate.Should().BeOfType<MonthlyScheduleTemplate>().And.BeEquivalentTo(new { Every = 1, OccuranceType = OccuranceType.DayOfWeek, Occurance = Occurance.Fourth, Day = DayOfWeek.Sunday });
                schedule.TimeTemplate.Should().BeOfType<ExactTimeScheduleTemplate>().And.BeEquivalentTo(new { Time = new TimeSpan(07, 00, 00) });
                schedule.ToString().Should().Be("Run on the fourth Sunday of every month, at 7:00");
            }
        }


        [Fact]
        public void Every2MonthsOnLastFridayAt7()
        {
            var schedule = Schedule.EveryMonths(2).OnLast(DayOfWeek.Friday).At(7, 0);

            using (new AssertionScope())
            {
                schedule.DateTemplate.Should().BeOfType<MonthlyScheduleTemplate>().And.BeEquivalentTo(new { Every = 2, OccuranceType = OccuranceType.DayOfWeek, Occurance = Occurance.Last, Day = DayOfWeek.Friday });
                schedule.TimeTemplate.Should().BeOfType<ExactTimeScheduleTemplate>().And.BeEquivalentTo(new { Time = new TimeSpan(07, 00, 00) });
                schedule.ToString().Should().Be("Run on the last Friday of every 2 months, at 7:00");
            }
        }

        [Fact]
        public void Every2MonthsOnLastDayAt7()
        {
            var schedule = Schedule.EveryMonths(2).OnLastDay().At(7, 0);

            using (new AssertionScope())
            {
                schedule.DateTemplate.Should().BeOfType<MonthlyScheduleTemplate>().And.BeEquivalentTo(new { Every = 2, OccuranceType = OccuranceType.Day, Occurance = Occurance.Last });
                schedule.TimeTemplate.Should().BeOfType<ExactTimeScheduleTemplate>().And.BeEquivalentTo(new { Time = new TimeSpan(07, 00, 00) });
                schedule.ToString().Should().Be("Run on the last day of every 2 months, at 7:00");
            }
        }

        [Fact]
        public void Every2MonthsOnFirstWeekdayAt7()
        {
            var schedule = Schedule.EveryMonths(2).OnFirstWeekday().At(7, 0);

            using (new AssertionScope())
            {
                schedule.DateTemplate.Should().BeOfType<MonthlyScheduleTemplate>().And.BeEquivalentTo(new { Every = 2, OccuranceType = OccuranceType.Weekday, Occurance = Occurance.First });
                schedule.TimeTemplate.Should().BeOfType<ExactTimeScheduleTemplate>().And.BeEquivalentTo(new { Time = new TimeSpan(07, 00, 00) });
                schedule.ToString().Should().Be("Run on the first weekday of every 2 months, at 7:00");
            }
        }

        [Fact]
        public void EveryMonthOnLastWeekdayEveryHourBetween9_17()
        {
            var schedule = Schedule.EveryMonths(2).OnLastWeekday().EveryHour().From(9, 00).Until(17, 00);

            using (new AssertionScope())
            {
                schedule.DateTemplate.Should().BeOfType<MonthlyScheduleTemplate>().And.BeEquivalentTo(new { Every = 2, OccuranceType = OccuranceType.Weekday, Occurance = Occurance.Last });
                schedule.TimeTemplate.Should().BeOfType<HourlyScheduleTemplate>().And.BeEquivalentTo(new { Every = 1, FromTime = new TimeSpan(09, 00, 00), ToTime = new TimeSpan(17, 00, 00) });
                schedule.ToString().Should().Be("Run on the last weekday of every 2 months, every hour between 9:00 and 17:00");
            }
        }

    }
}
