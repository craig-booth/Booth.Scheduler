using System;
using System.Linq;

using Booth.Common;

using NUnit.Framework;

namespace Booth.Scheduler.Test
{
    class WeeklyScheduleTemplateTests
    {
        [TestCase]
        public void FirstRunDateOnCorrectDay()
        {         
            var template = new WeeklyScheduleTemplate(1);
            template[DayOfWeek.Wednesday] = true;

            var startDate = new Date(2019, 10, 23);

            var actual = template.GetDates(startDate).First();

            Assert.That(actual, Is.EqualTo(startDate));
        }

        [TestCase]
        public void FirstRunDateBeforeDate()
        {
            var template = new WeeklyScheduleTemplate(1);
            template[DayOfWeek.Wednesday] = true;

            var startDate = new Date(2019, 10, 21);

            var actual = template.GetDates(startDate).First();

            Assert.That(actual, Is.EqualTo(new Date(2019, 10, 23)));
        }

        [TestCase]
        public void FirstRunDateAfterDate()
        {
            var template = new WeeklyScheduleTemplate(1);
            template[DayOfWeek.Wednesday] = true;

            var startDate = new Date(2019, 10, 25);

            var actual = template.GetDates(startDate).First();

            Assert.That(actual, Is.EqualTo(new Date(2019, 10, 30)));
        }

        [TestCase]
        public void FirstRunDateBetweenDates()
        {
            var template = new WeeklyScheduleTemplate(1);
            template[DayOfWeek.Wednesday] = true;
            template[DayOfWeek.Saturday] = true;

            var startDate = new Date(2019, 10, 24);

            var actual = template.GetDates(startDate).First();

            Assert.That(actual, Is.EqualTo(new Date(2019, 10, 26)));
        }

        [TestCase]
        public void RunEveryWeekOnMonday()
        {
            var template = new WeeklyScheduleTemplate(1);
            template[DayOfWeek.Monday] = true;

            var startDate = new Date(2019, 10, 24);

            var actual = template.GetDates(startDate).Take(5);

            var expected = new Date[] {
                new Date(2019, 10, 28),
                new Date(2019, 11, 04),
                new Date(2019, 11, 11),
                new Date(2019, 11, 18),
                new Date(2019, 11, 25)};

            Assert.That(actual, Is.EqualTo(expected));
        }

        [TestCase]
        public void RunEvery2WeeksOnMonday()
        {
            var template = new WeeklyScheduleTemplate(2);
            template[DayOfWeek.Monday] = true;

            var startDate = new Date(2019, 10, 24);

            var actual = template.GetDates(startDate).Take(5);

            var expected = new Date[] {
                new Date(2019, 10, 28),
                new Date(2019, 11, 11),
                new Date(2019, 11, 25),
                new Date(2019, 12, 09),
                new Date(2019, 12, 23)};

            Assert.That(actual, Is.EqualTo(expected));
        }

        [TestCase]
        public void RunEveryWeekOnWednesdayAndSunday()
        {
            var template = new WeeklyScheduleTemplate(1);
            template[DayOfWeek.Wednesday] = true;
            template[DayOfWeek.Sunday] = true;

            var startDate = new Date(2019, 10, 24);

            var actual = template.GetDates(startDate).Take(5);

            var expected = new Date[] {
                new Date(2019, 10, 27),
                new Date(2019, 10, 30),
                new Date(2019, 11, 03),
                new Date(2019, 11, 06),
                new Date(2019, 11, 10)};

            Assert.That(actual, Is.EqualTo(expected));
        }

        [TestCase]
        public void RunEveryFourWeeksOnWeekdays()
        {
            var template = new WeeklyScheduleTemplate(4);
            template[DayOfWeek.Monday] = true;
            template[DayOfWeek.Tuesday] = true;
            template[DayOfWeek.Wednesday] = true;
            template[DayOfWeek.Thursday] = true;
            template[DayOfWeek.Friday] = true;

            var startDate = new Date(2019, 10, 24);

            var actual = template.GetDates(startDate).Take(10);

            var expected = new Date[] {
                new Date(2019, 10, 24),
                new Date(2019, 10, 25),
                new Date(2019, 11, 18),
                new Date(2019, 11, 19),
                new Date(2019, 11, 20),
                new Date(2019, 11, 21),
                new Date(2019, 11, 22),
                new Date(2019, 12, 16),
                new Date(2019, 12, 17),
                new Date(2019, 12, 18)};

            Assert.That(actual, Is.EqualTo(expected));
        }

        [TestCase]
        public void ValidateEveryNotZero()
        {
            var template = new WeeklyScheduleTemplate(0);

            var errors = template.Validate().ToList();
            Assert.That(errors, Has.Count.EqualTo(2));
            Assert.That(errors[0], Is.EqualTo("Weekly schedule must occur atleast every 1 weeks"));
            Assert.That(errors[1], Is.EqualTo("Weekly schedule must occur on atleast 1 day"));
        }

        [TestCase]
        public void ValidateEveryNotLessThanZero()
        {
            var template = new WeeklyScheduleTemplate(-1);

            var errors = template.Validate().ToList();
            Assert.That(errors, Has.Count.EqualTo(2));
            Assert.That(errors[0], Is.EqualTo("Weekly schedule must occur atleast every 1 weeks"));
            Assert.That(errors[1], Is.EqualTo("Weekly schedule must occur on atleast 1 day"));
        }

        [TestCase]
        public void ValidateAtLeastOneDaySelected()
        {
            var template = new WeeklyScheduleTemplate(1);

            var errors = template.Validate().ToList();
            Assert.That(errors, Has.Count.EqualTo(1));
            Assert.That(errors[0], Is.EqualTo("Weekly schedule must occur on atleast 1 day"));
        }
    }
}
