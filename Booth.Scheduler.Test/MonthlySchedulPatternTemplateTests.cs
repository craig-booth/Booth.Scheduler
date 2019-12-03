using System;
using System.Linq;

using Booth.Common;

using NUnit.Framework;

namespace Booth.Scheduler.Test
{
    class MonthlySchedulePatternTemplateTests
    {
        [TestCase]
        public void FirstRunDateOnFirstMonday()
        {
            var template = new MonthlyScheduleTemplate(1);
            template.OccuranceType = OccuranceType.DayOfWeek;
            template.Occurance = Occurance.First;
            template.Day = DayOfWeek.Monday;

            var startDate = new Date(2019, 10, 01);

            var actual = template.GetDates(startDate).First();

            Assert.That(actual, Is.EqualTo(new Date(2019, 10, 07)));
        }

        [TestCase]
        public void FirstRunDateOnSecondWednesday()
        {
            var template = new MonthlyScheduleTemplate(1);
            template.OccuranceType = OccuranceType.DayOfWeek;
            template.Occurance = Occurance.Second;
            template.Day = DayOfWeek.Wednesday;

            var startDate = new Date(2019, 10, 09);

            var actual = template.GetDates(startDate).First();

            Assert.That(actual, Is.EqualTo(new Date(2019, 10, 09)));
        }

        [TestCase]
        public void FirstRunDateOnThirdFriday()
        {
            var template = new MonthlyScheduleTemplate(1);
            template.OccuranceType = OccuranceType.DayOfWeek;
            template.Occurance = Occurance.Third;
            template.Day = DayOfWeek.Friday;

            var startDate = new Date(2019, 10, 23);

            var actual = template.GetDates(startDate).First();

            Assert.That(actual, Is.EqualTo(new Date(2019, 11, 15)));
        }

        [TestCase]
        public void FirstRunDateOnFourthSaturday()
        {
            var template = new MonthlyScheduleTemplate(1);
            template.OccuranceType = OccuranceType.DayOfWeek;
            template.Occurance = Occurance.Fourth;
            template.Day = DayOfWeek.Saturday;

            var startDate = new Date(2019, 10, 09);

            var actual = template.GetDates(startDate).First();

            Assert.That(actual, Is.EqualTo(new Date(2019, 10, 26)));
        }

        [TestCase]
        public void FirstRunDateOnLastWednesday()
        {
            var template = new MonthlyScheduleTemplate(1);
            template.OccuranceType = OccuranceType.DayOfWeek;
            template.Occurance = Occurance.Last;
            template.Day = DayOfWeek.Wednesday;

            var startDate = new Date(2019, 10, 09);

            var actual = template.GetDates(startDate).First();

            Assert.That(actual, Is.EqualTo(new Date(2019, 10, 30)));
        }

        [TestCase]
        public void RunEvery3MonthsOnSecondTuesday()
        {
            var template = new MonthlyScheduleTemplate(3);
            template.OccuranceType = OccuranceType.DayOfWeek;
            template.Occurance = Occurance.Second;
            template.Day = DayOfWeek.Tuesday;

            var startDate = new Date(2019, 10, 09);

            var actual = template.GetDates(startDate).Take(5);

            var expected = new Date[] {
                new Date(2019, 11, 12),
                new Date(2020, 02, 11),
                new Date(2020, 05, 12),
                new Date(2020, 08, 11),
                new Date(2020, 11, 10)};

            Assert.That(actual, Is.EqualTo(expected));
        }

        [TestCase]
        public void RunEveryMonthOnLastThursday()
        {
            var template = new MonthlyScheduleTemplate(1);
            template.OccuranceType = OccuranceType.DayOfWeek;
            template.Occurance = Occurance.Last;
            template.Day = DayOfWeek.Thursday;

            var startDate = new Date(2019, 10, 09);

            var actual = template.GetDates(startDate).Take(5);

            var expected = new Date[] {
                new Date(2019, 10, 31),
                new Date(2019, 11, 28),
                new Date(2019, 12, 26),
                new Date(2020, 01, 30),
                new Date(2020, 02, 27)};

            Assert.That(actual, Is.EqualTo(expected));
        }

        [TestCase]
        public void RunEvery3MonthsOnFirstDay()
        {
            var template = new MonthlyScheduleTemplate(3);
            template.OccuranceType = OccuranceType.Day;
            template.Occurance = Occurance.First;

            var startDate = new Date(2019, 10, 09);

            var actual = template.GetDates(startDate).Take(5);

            var expected = new Date[] {
                new Date(2019, 11, 01),
                new Date(2020, 02, 01),
                new Date(2020, 05, 01),
                new Date(2020, 08, 01),
                new Date(2020, 11, 01)};

            Assert.That(actual, Is.EqualTo(expected));
        }

        [TestCase]
        public void RunEvery2MonthsOnLastDay()
        {
            var template = new MonthlyScheduleTemplate(2);
            template.OccuranceType = OccuranceType.Day;
            template.Occurance = Occurance.Last;

            var startDate = new Date(2019, 10, 09);

            var actual = template.GetDates(startDate).Take(5);

            var expected = new Date[] {
                new Date(2019, 10, 31),
                new Date(2019, 12, 31),
                new Date(2020, 02, 29),
                new Date(2020, 04, 30),
                new Date(2020, 06, 30)};

            Assert.That(actual, Is.EqualTo(expected));
        }

        [TestCase]
        public void RunEvery2MonthsOnFirstWeekDay()
        {
            var template = new MonthlyScheduleTemplate(2);
            template.OccuranceType = OccuranceType.Weekday;
            template.Occurance = Occurance.First;

            var startDate = new Date(2019, 10, 09);

            var actual = template.GetDates(startDate).Take(5);

            var expected = new Date[] {
                new Date(2019, 11, 01),
                new Date(2020, 01, 01),
                new Date(2020, 03, 02),
                new Date(2020, 05, 01),
                new Date(2020, 07, 01)};

            Assert.That(actual, Is.EqualTo(expected));
        }

        [TestCase]
        public void RunEveryMonthOnLastWeekDay()
        {
            var template = new MonthlyScheduleTemplate(1);
            template.OccuranceType = OccuranceType.Weekday;
            template.Occurance = Occurance.Last;

            var startDate = new Date(2019, 10, 09);

            var actual = template.GetDates(startDate).Take(5);

            var expected = new Date[] {
                new Date(2019, 10, 31),
                new Date(2019, 11, 29),
                new Date(2019, 12, 31),
                new Date(2020, 01, 31),
                new Date(2020, 02, 28)};

            Assert.That(actual, Is.EqualTo(expected));
        }

        [TestCase]
        public void ValidateEveryNotZero()
        {
            var template = new MonthlyScheduleTemplate(0);
            template.OccuranceType = OccuranceType.DayOfWeek;

            var errors = template.Validate().ToList();
            Assert.That(errors, Has.Count.EqualTo(1));
            Assert.That(errors[0], Is.EqualTo("Monthly schedule must occur atleast every 1 months"));
        }

        [TestCase]
        public void ValidateEveryNotLessThanZero()
        {
            var template = new MonthlyScheduleTemplate(-1);
            template.OccuranceType = OccuranceType.DayOfWeek;

            var errors = template.Validate().ToList();
            Assert.That(errors, Has.Count.EqualTo(1));
            Assert.That(errors[0], Is.EqualTo("Monthly schedule must occur atleast every 1 months"));
        }

    }
}
