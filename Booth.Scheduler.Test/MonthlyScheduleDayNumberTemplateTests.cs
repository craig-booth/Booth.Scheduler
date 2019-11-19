using System;
using System.Linq;

using NUnit.Framework;

namespace Booth.Scheduler.Test
{
    class MonthlyScheduleDayNumberTemplateTests
    {
        [TestCase]
        public void FirstRunDateOnCorrectDay()
        {
            var template = new MonthlyScheduleTemplate(1);
            template.DayNumber = 26;

            var startDate = new DateTime(2019, 10, 26, 04, 34, 56);

            var actual = template.GetDates(startDate).First();

            Assert.That(actual, Is.EqualTo(startDate.Date));
        }

        [TestCase]
        public void FirstRunDateAfterCorrectDay()
        {
            var template = new MonthlyScheduleTemplate(1);
            template.DayNumber = 26;

            var startDate = new DateTime(2019, 10, 28, 04, 34, 56);

            var actual = template.GetDates(startDate).First();

            Assert.That(actual, Is.EqualTo(new DateTime(2019, 11, 26)));
        }

        [TestCase]
        public void FirstRunDateBeforeCorrectDay()
        {
            var template = new MonthlyScheduleTemplate(1);
            template.DayNumber = 26;

            var startDate = new DateTime(2019, 10, 02, 04, 34, 56);

            var actual = template.GetDates(startDate).First();

            Assert.That(actual, Is.EqualTo(new DateTime(2019, 10, 26)));
        }

        [TestCase]
        public void RunEveryMonthOn15th()
        {
            var template = new MonthlyScheduleTemplate(1);
            template.DayNumber = 15;

            var startDate = new DateTime(2019, 10, 24, 04, 34, 56);

            var actual = template.GetDates(startDate).Take(5);

            var expected = new DateTime[] {
                new DateTime(2019, 11, 15),
                new DateTime(2019, 12, 15),
                new DateTime(2020, 01, 15),
                new DateTime(2020, 02, 15),
                new DateTime(2020, 03, 15)};

            Assert.That(actual, Is.EqualTo(expected));
        }

        public void RunEverySixMonthsOn10th()
        {
            var template = new MonthlyScheduleTemplate(6);
            template.DayNumber = 10;

            var startDate = new DateTime(2019, 10, 24, 04, 34, 56);

            var actual = template.GetDates(startDate).Take(5);

            var expected = new DateTime[] {
                new DateTime(2019, 11, 10),
                new DateTime(2020, 05, 10),
                new DateTime(2020, 11, 10),
                new DateTime(2021, 05, 10),
                new DateTime(2021, 11, 10)};

            Assert.That(actual, Is.EqualTo(expected));
        }

        public void RunEveryMonthOn30th()
        {
            var template = new MonthlyScheduleTemplate(6);
            template.DayNumber = 31;

            var startDate = new DateTime(2019, 10, 24, 04, 34, 56);

            var actual = template.GetDates(startDate).Take(12);

            var expected = new DateTime[] {
                new DateTime(2019, 10, 30),
                new DateTime(2019, 11, 30),
                new DateTime(2019, 12, 30),
                new DateTime(2019, 01, 30),
                new DateTime(2019, 02, 29)};

            Assert.That(actual, Is.EqualTo(expected));
        }

        [TestCase]
        public void ValidateEveryNotZero()
        {
            var template = new MonthlyScheduleTemplate(0);

            var errors = template.Validate().ToList();
            Assert.That(errors, Has.Count.EqualTo(2));
            Assert.That(errors[0], Is.EqualTo("Monthly schedule must occur atleast every 1 months"));
            Assert.That(errors[1], Is.EqualTo("Day number must be between 1 and 31"));
        }

        [TestCase]
        public void ValidateEveryNotLessThanZero()
        {
            var template = new MonthlyScheduleTemplate(-1);

            var errors = template.Validate().ToList();
            Assert.That(errors, Has.Count.EqualTo(2));
            Assert.That(errors[0], Is.EqualTo("Monthly schedule must occur atleast every 1 months"));
            Assert.That(errors[1], Is.EqualTo("Day number must be between 1 and 31"));
        }

        [TestCase]
        public void ValidateDayNotLessThan1()
        {
            var template = new MonthlyScheduleTemplate(1);
            template.DayNumber = 0;

            var errors = template.Validate().ToList();
            Assert.That(errors, Has.Count.EqualTo(1));
            Assert.That(errors[0], Is.EqualTo("Day number must be between 1 and 31"));
        }

        [TestCase]
        public void ValidateDayNotGreateThan31()
        {
            var template = new MonthlyScheduleTemplate(1);
            template.DayNumber = 32;

            var errors = template.Validate().ToList();
            Assert.That(errors, Has.Count.EqualTo(1));
            Assert.That(errors[0], Is.EqualTo("Day number must be between 1 and 31"));
        }

    }
}
