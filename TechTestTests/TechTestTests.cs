using System;
using System.Collections.Generic;
using Xunit;
using TechTest;

namespace TechTestTests
{
    public class TechTestTests
    {
        [Fact]
        public void AddOneSecond()
        {
            UtcComponents utcComponents = new UtcComponents();

            UtcComponents resultUtcComponents = DateTimeOperation.Execute("now()+1s", utcComponents);

            Assert.Equal(utcComponents.Seconds + 1, resultUtcComponents.Seconds);
        }

        [Fact]
        public void AddSixtySeconds()
        {
            UtcComponents utcComponents = new UtcComponents();

            UtcComponents resultUtcComponents = DateTimeOperation.Execute("now()+60s", utcComponents);

            Assert.Equal(utcComponents.Seconds, resultUtcComponents.Seconds);
            Assert.Equal(utcComponents.Minutes + 1, resultUtcComponents.Minutes);
        }

        [Fact]
        public void AddOneMinute()
        {
            UtcComponents utcComponents = new UtcComponents();

            UtcComponents resultUtcComponents = DateTimeOperation.Execute("now()+1m", utcComponents);

            Assert.Equal(utcComponents.Minutes + 1, resultUtcComponents.Minutes);
        }

        [Fact]
        public void AddSixtyMinutes()
        {
            UtcComponents utcComponents = new UtcComponents();

            UtcComponents resultUtcComponents = DateTimeOperation.Execute("now()+60m", utcComponents);

            Assert.Equal(utcComponents.Minutes, resultUtcComponents.Minutes);
            Assert.Equal(utcComponents.Hours + 1, resultUtcComponents.Hours);
        }

        [Fact]
        public void AddOneHour()
        {
            UtcComponents utcComponents = new UtcComponents();

            UtcComponents resultUtcComponents = DateTimeOperation.Execute("now()+1h", utcComponents);

            Assert.Equal(utcComponents.Hours + 1, resultUtcComponents.Hours);
        }

        [Fact]
        public void AddTwentyFourHours()
        {
            UtcComponents utcComponents = new UtcComponents();

            UtcComponents resultUtcComponents = DateTimeOperation.Execute("now()+24h", utcComponents);

            Assert.Equal(utcComponents.Hours, resultUtcComponents.Hours);
            Assert.Equal(utcComponents.Day + 1, resultUtcComponents.Day);
        }

        [Fact]
        public void AddOneDay()
        {
            UtcComponents utcComponents = new UtcComponents();

            UtcComponents resultUtcComponents = DateTimeOperation.Execute("now()+1d", utcComponents);

            Assert.Equal(utcComponents.Day + 1, resultUtcComponents.Day);
        }

        [Fact]
        public void AddThirtyOneDaysToJanuary()
        {
            UtcComponents utcComponents = new UtcComponents();

            UtcComponents resultUtcComponents = DateTimeOperation.Execute("now()+31d", utcComponents);

            Assert.Equal(utcComponents.Day, resultUtcComponents.Day);
            Assert.Equal(utcComponents.Month + 1, resultUtcComponents.Month);
        }

        [Fact]
        public void AddThirtyDaysToApril()
        {
            UtcComponents utcComponents = new UtcComponents() { Month = 4 };

            UtcComponents resultUtcComponents = DateTimeOperation.Execute("now()+30d", utcComponents);

            Assert.Equal(utcComponents.Day, resultUtcComponents.Day);
            Assert.Equal(utcComponents.Month + 1, resultUtcComponents.Month);
        }

        [Fact]
        public void AddTwentyEightDaysToNonLeapFebruary()
        {
            Assert.True(false);
        }

        [Fact]
        public void AddTwentyEightDaysToLeapFebruary()
        {
            Assert.True(false);
        }

        [Fact]
        public void AddTwentyNineDaysToLeapFebruary()
        {
            Assert.True(false);
        }

        [Fact]
        public void AddOneMonth()
        {
            UtcComponents utcComponents = new UtcComponents();

            UtcComponents resultUtcComponents = DateTimeOperation.Execute("now()+1mon", utcComponents);

            Assert.Equal(utcComponents.Month + 1, resultUtcComponents.Month);
        }

        [Fact]
        public void AddTwelveMonths()
        {
            UtcComponents utcComponents = new UtcComponents();

            UtcComponents resultUtcComponents = DateTimeOperation.Execute("now()+12mon", utcComponents);

            Assert.Equal(utcComponents.Month, resultUtcComponents.Month);
            Assert.Equal(utcComponents.Year + 1, resultUtcComponents.Year);
        }

        [Fact]
        public void AddOneYear()
        {
            UtcComponents utcComponents = new UtcComponents();

            UtcComponents resultUtcComponents = DateTimeOperation.Execute("now()+1y", utcComponents);

            Assert.Equal(utcComponents.Year + 1, resultUtcComponents.Year);
        }

        //[Fact]
        //public void AddSeconds()
        //{
        //    UtcComponents utcComponents = UtcUtils.UtcNowComponents;

        //    UtcComponents resultUtcComponents = DateTimeOperation.Execute("now()+1s");

        //    Assert.Equal(utcComponents.Seconds + 1, resultUtcComponents.Seconds);
        //}

        //[Fact]
        //public void SecondsOverflowsToMinutes()
        //{
        //    UtcComponents utcComponents = UtcUtils.UtcNowComponents;

        //    UtcComponents resultUtcComponents = DateTimeOperation.Execute("now()+60s");

        //    Assert.Equal(utcComponents.Minutes + 1, resultUtcComponents.Minutes);
        //}

        //[Fact]
        //public void AddMinutes()
        //{
        //    UtcComponents utcComponents = UtcUtils.UtcNowComponents;

        //    UtcComponents resultUtcComponents = DateTimeOperation.Execute("now()+1m");

        //    Assert.Equal(utcComponents.Minutes + 1, resultUtcComponents.Minutes);
        //}

        //[Fact]
        //public void MinutesRoundsToHours()
        //{
        //    UtcComponents utcComponents = UtcUtils.UtcNowComponents;

        //    UtcComponents resultUtcComponents = DateTimeOperation.Execute("now()+60m");

        //    Assert.Equal(utcComponents.Hours + 1, resultUtcComponents.Hours);
        //}

        //[Fact]
        //public void AddHours()
        //{
        //    UtcComponents utcComponents = UtcUtils.UtcNowComponents;

        //    UtcComponents resultUtcComponents = DateTimeOperation.Execute("now()+1h");

        //    Assert.Equal(utcComponents.Hours + 1, resultUtcComponents.Hours);
        //}

        //[Fact]
        //public void HoursRoundsToDays()
        //{
        //    UtcComponents utcComponents = UtcUtils.UtcNowComponents;

        //    UtcComponents resultUtcComponents = DateTimeOperation.Execute("now()+24h");

        //    Assert.True(utcComponents.Day + 1 == resultUtcComponents.Day || utcComponents.Month + 1 == resultUtcComponents.Month && resultUtcComponents.Day == 1);
        //}

        //[Fact]
        //public void AddDays()
        //{
        //    UtcComponents utcComponents = UtcUtils.UtcNowComponents;

        //    UtcComponents resultUtcComponents = DateTimeOperation.Execute("now()+1d");

        //    Assert.Equal(utcComponents.Day + 1, resultUtcComponents.Day);
        //}

        //[Fact]
        //public void DaysRoundsToMonths()
        //{
        //    UtcComponents utcComponents = UtcUtils.UtcNowComponents;

        //    UtcComponents resultUtcComponents = DateTimeOperation.Execute("now()+31d");

        //    //Assert.True(utcComponents
        //}

        //[Fact]
        //public void AddMonths()
        //{
        //    UtcComponents utcComponents = UtcUtils.UtcNowComponents;

        //    UtcComponents resultUtcComponents = DateTimeOperation.Execute("now()+1mon");

        //    Assert.True((utcComponents.Month + 1 == resultUtcComponents.Month) || (utcComponents.Year + 1 == resultUtcComponents.Year && resultUtcComponents.Month == 1));
        //}

        //[Fact]
        //public void AddYears()
        //{
        //    UtcComponents utcComponents = UtcUtils.UtcNowComponents;

        //    UtcComponents resultUtcComponents = DateTimeOperation.Execute("now()+1y");

        //    Assert.Equal(utcComponents.Year + 1, resultUtcComponents.Year);
        //}
    }
}