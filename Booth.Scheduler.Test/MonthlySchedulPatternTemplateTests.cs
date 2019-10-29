using System;
using System.Linq;

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

            var startDate = new DateTime(2019, 10, 01, 04, 34, 56);

            var actual = template.GetDates(startDate).First();

            Assert.That(actual, Is.EqualTo(startDate.Date));

            Assert.That(actual, Is.EqualTo(new DateTime(2019, 10, 07)));
        }

        [TestCase]
        public void FirstRunDateOnSecondWednesday()
        {
            var template = new MonthlyScheduleTemplate(1);
            template.OccuranceType = OccuranceType.DayOfWeek;
            template.Occurance = Occurance.Second;
            template.Day = DayOfWeek.Wednesday;

            var startDate = new DateTime(2019, 10, 09, 04, 34, 56);

            var actual = template.GetDates(startDate).First();

            Assert.That(actual, Is.EqualTo(new DateTime(2019, 10, 04)));
        }

        [TestCase]
        public void FirstRunDateOnThirdFriday()
        {
            var template = new MonthlyScheduleTemplate(1);
            template.OccuranceType = OccuranceType.DayOfWeek;
            template.Occurance = Occurance.Third;
            template.Day = DayOfWeek.Friday;

            var startDate = new DateTime(2019, 10, 26, 04, 34, 56);

            var actual = template.GetDates(startDate).First();

            Assert.That(actual, Is.EqualTo(new DateTime(2019, 11, 15)));
        }

        [TestCase]
        public void FirstRunDateOnFourthSaturday()
        {
            var template = new MonthlyScheduleTemplate(1);
            template.OccuranceType = OccuranceType.DayOfWeek;
            template.Occurance = Occurance.Fourth;
            template.Day = DayOfWeek.Saturday;

            var startDate = new DateTime(2019, 10, 26, 04, 34, 56);

            var actual = template.GetDates(startDate).First();

            Assert.That(actual, Is.EqualTo(new DateTime(2019, 10, 26)));
        }

        [TestCase]
        public void FirstRunDateOnLastWednesday()
        {
            var template = new MonthlyScheduleTemplate(1);
            template.OccuranceType = OccuranceType.DayOfWeek;
            template.Occurance = Occurance.Last;
            template.Day = DayOfWeek.Wednesday;

            var startDate = new DateTime(2019, 10, 26, 04, 34, 56);

            var actual = template.GetDates(startDate).First();

            Assert.That(actual, Is.EqualTo(new DateTime(2019, 10, 30)));
        }

        [TestCase]
        public void RunEvery3MonthsOnSecondTuesday()
        {
            var template = new MonthlyScheduleTemplate(3);
            template.OccuranceType = OccuranceType.DayOfWeek;
            template.Occurance = Occurance.Second;
            template.Day = DayOfWeek.Tuesday;

            var startDate = new DateTime(2019, 10, 24, 04, 34, 56);

            var actual = template.GetDates(startDate).Take(5);

            var expected = new DateTime[] {
                new DateTime(2019, 11, 12),
                new DateTime(2020, 02, 11),
                new DateTime(2020, 05, 12),
                new DateTime(2020, 08, 11),
                new DateTime(2020, 11, 10)};

            Assert.That(actual, Is.EqualTo(expected));
        }

        [TestCase]
        public void RunEveryMonthOnLastThursday()
        {
            var template = new MonthlyScheduleTemplate(1);
            template.OccuranceType = OccuranceType.DayOfWeek;
            template.Occurance = Occurance.Last;
            template.Day = DayOfWeek.Thursday;

            var startDate = new DateTime(2019, 10, 24, 04, 34, 56);

            var actual = template.GetDates(startDate).Take(5);

            var expected = new DateTime[] {
                new DateTime(2019, 10, 31),
                new DateTime(2019, 11, 28),
                new DateTime(2019, 12, 26),
                new DateTime(2020, 01, 30),
                new DateTime(2020, 02, 27)};

            Assert.That(actual, Is.EqualTo(expected));
        }

        [TestCase]
        public void RunEvery3MonthsOnFirstDay()
        {
            var template = new MonthlyScheduleTemplate(1);
            template.OccuranceType = OccuranceType.Day;
            template.Occurance = Occurance.First;

            var startDate = new DateTime(2019, 10, 24, 04, 34, 56);

            var actual = template.GetDates(startDate).Take(5);

            var expected = new DateTime[] {
                new DateTime(2019, 11, 01),
                new DateTime(2019, 12, 01),
                new DateTime(2020, 01, 01),
                new DateTime(2020, 02, 01),
                new DateTime(2020, 03, 01)};

            Assert.That(actual, Is.EqualTo(expected));
        }

        [TestCase]
        public void RunEvery2MonthsOnLastDay()
        {
            var template = new MonthlyScheduleTemplate(1);
            template.OccuranceType = OccuranceType.Day;
            template.Occurance = Occurance.Last;

            var startDate = new DateTime(2019, 10, 24, 04, 34, 56);

            var actual = template.GetDates(startDate).Take(5);

            var expected = new DateTime[] {
                new DateTime(2019, 10, 31),
                new DateTime(2019, 11, 30),
                new DateTime(2020, 11, 31),
                new DateTime(2020, 01, 31),
                new DateTime(2020, 02, 29)};

            Assert.That(actual, Is.EqualTo(expected));
        }

        [TestCase]
        public void RunEvery2MonthsOnFirstWeekDay()
        {
            var template = new MonthlyScheduleTemplate(1);
            template.OccuranceType = OccuranceType.Weekday;
            template.Occurance = Occurance.Last;

            var startDate = new DateTime(2019, 10, 24, 04, 34, 56);

            var actual = template.GetDates(startDate).Take(5);

            var expected = new DateTime[] {
                new DateTime(2019, 11, 01),
                new DateTime(2020, 01, 01),
                new DateTime(2020, 03, 02),
                new DateTime(2020, 05, 01),
                new DateTime(2020, 07, 01)};

            Assert.That(actual, Is.EqualTo(expected));
        }

        [TestCase]
        public void RunEveryMonthOnLastWeekDay()
        {
            var template = new MonthlyScheduleTemplate(1);
            template.OccuranceType = OccuranceType.Weekday;
            template.Occurance = Occurance.Last;

            var startDate = new DateTime(2019, 10, 24, 04, 34, 56);

            var actual = template.GetDates(startDate).Take(5);

            var expected = new DateTime[] {
                new DateTime(2019, 10, 31),
                new DateTime(2019, 11, 30),
                new DateTime(2020, 11, 31),
                new DateTime(2020, 01, 31),
                new DateTime(2020, 02, 29)};

            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}
