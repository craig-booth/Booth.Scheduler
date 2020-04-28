using System;
using System.Linq;

using Xunit;
using FluentAssertions;

namespace Booth.Scheduler.Test
{
    public class WeeklyScheduleTemplateTests
    {
        [Fact]
        public void FirstRunDateOnCorrectDay()
        {         
            var template = new WeeklyScheduleTemplate(1);
            template[DayOfWeek.Wednesday] = true;

            var startDate = new DateTime(2019, 10, 23, 04, 34, 56);

            var actual = template.GetDates(startDate).First();

            actual.Should().Be(startDate.Date);
        }

        [Fact]
        public void FirstRunDateBeforeDate()
        {
            var template = new WeeklyScheduleTemplate(1);
            template[DayOfWeek.Wednesday] = true;

            var startDate = new DateTime(2019, 10, 21, 04, 34, 56);

            var actual = template.GetDates(startDate).First();

            actual.Should().Be(new DateTime(2019, 10, 23));
        }

        [Fact]
        public void FirstRunDateAfterDate()
        {
            var template = new WeeklyScheduleTemplate(1);
            template[DayOfWeek.Wednesday] = true;

            var startDate = new DateTime(2019, 10, 25, 04, 34, 56);

            var actual = template.GetDates(startDate).First();

            actual.Should().Be(new DateTime(2019, 10, 30));
        }

        [Fact]
        public void FirstRunDateBetweenDates()
        {
            var template = new WeeklyScheduleTemplate(1);
            template[DayOfWeek.Wednesday] = true;
            template[DayOfWeek.Saturday] = true;

            var startDate = new DateTime(2019, 10, 24, 04, 34, 56);

            var actual = template.GetDates(startDate).First();

            actual.Should().Be(new DateTime(2019, 10, 26));
        }

        [Fact]
        public void RunEveryWeekOnMonday()
        {
            var template = new WeeklyScheduleTemplate(1);
            template[DayOfWeek.Monday] = true;

            var startDate = new DateTime(2019, 10, 24, 04, 34, 56);

            var actual = template.GetDates(startDate).Take(5);

            actual.Should().Equal(new DateTime[] {
                new DateTime(2019, 10, 28),
                new DateTime(2019, 11, 04),
                new DateTime(2019, 11, 11),
                new DateTime(2019, 11, 18),
                new DateTime(2019, 11, 25)
            });
        }

        [Fact]
        public void RunEvery2WeeksOnMonday()
        {
            var template = new WeeklyScheduleTemplate(2);
            template[DayOfWeek.Monday] = true;

            var startDate = new DateTime(2019, 10, 24, 04, 34, 56);

            var actual = template.GetDates(startDate).Take(5);

            actual.Should().Equal(new DateTime[] {
                new DateTime(2019, 10, 28),
                new DateTime(2019, 11, 11),
                new DateTime(2019, 11, 25),
                new DateTime(2019, 12, 09),
                new DateTime(2019, 12, 23)
            });
        }

        [Fact]
        public void RunEveryWeekOnWednesdayAndSunday()
        {
            var template = new WeeklyScheduleTemplate(1);
            template[DayOfWeek.Wednesday] = true;
            template[DayOfWeek.Sunday] = true;

            var startDate = new DateTime(2019, 10, 24, 04, 34, 56);

            var actual = template.GetDates(startDate).Take(5);

            actual.Should().Equal(new DateTime[] {
                new DateTime(2019, 10, 27),
                new DateTime(2019, 10, 30),
                new DateTime(2019, 11, 03),
                new DateTime(2019, 11, 06),
                new DateTime(2019, 11, 10)
            });
        }

        [Fact]
        public void RunEveryFourWeeksOnWeekdays()
        {
            var template = new WeeklyScheduleTemplate(4);
            template[DayOfWeek.Monday] = true;
            template[DayOfWeek.Tuesday] = true;
            template[DayOfWeek.Wednesday] = true;
            template[DayOfWeek.Thursday] = true;
            template[DayOfWeek.Friday] = true;

            var startDate = new DateTime(2019, 10, 24, 04, 34, 56);

            var actual = template.GetDates(startDate).Take(10);

            actual.Should().Equal(new DateTime[] {
                new DateTime(2019, 10, 24),
                new DateTime(2019, 10, 25),
                new DateTime(2019, 11, 18),
                new DateTime(2019, 11, 19),
                new DateTime(2019, 11, 20),
                new DateTime(2019, 11, 21),
                new DateTime(2019, 11, 22),
                new DateTime(2019, 12, 16),
                new DateTime(2019, 12, 17),
                new DateTime(2019, 12, 18)
            });
        }

        [Fact]
        public void ValidateEveryNotZero()
        {
            var template = new WeeklyScheduleTemplate(0);

            var errors = template.Validate().ToList();

            errors.Should().BeEquivalentTo(new string[] {
                "Weekly schedule must occur atleast every 1 weeks",
                "Weekly schedule must occur on atleast 1 day"});
        }

        [Fact]
        public void ValidateEveryNotLessThanZero()
        {
            var template = new WeeklyScheduleTemplate(-1);

            var errors = template.Validate().ToList();

            errors.Should().BeEquivalentTo(new string[] {
                "Weekly schedule must occur atleast every 1 weeks",
                "Weekly schedule must occur on atleast 1 day"});
        }

        [Fact]
        public void ValidateAtLeastOneDaySelected()
        {
            var template = new WeeklyScheduleTemplate(1);

            var errors = template.Validate().ToList();

            errors.Should().BeEquivalentTo(new string[] { "Weekly schedule must occur on atleast 1 day" });
        }
    }
}
