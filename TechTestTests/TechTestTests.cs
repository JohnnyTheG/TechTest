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
            UtcComponents utcComponents = new UtcComponents() { Month = 2, Year = 2022 };

            UtcComponents resultUtcComponents = DateTimeOperation.Execute("now()+28d", utcComponents);

            Assert.Equal(utcComponents.Day, resultUtcComponents.Day);
            Assert.Equal(utcComponents.Month + 1, resultUtcComponents.Month);
        }

        [Fact]
        public void AddTwentyEightDaysToLeapFebruary()
        {
            UtcComponents utcComponents = new UtcComponents() { Month = 2, Year = 2020 };

            UtcComponents resultUtcComponents = DateTimeOperation.Execute("now()+28d", utcComponents);

            Assert.Equal(29, resultUtcComponents.Day);
            Assert.Equal(utcComponents.Month, resultUtcComponents.Month);
        }

        [Fact]
        public void AddTwentyNineDaysToLeapFebruary()
        {
            UtcComponents utcComponents = new UtcComponents() { Month = 2, Year = 2020 };

            UtcComponents resultUtcComponents = DateTimeOperation.Execute("now()+29d", utcComponents);

            //Assert.Equal(utcComponents.Day + 1, resultUtcComponents.Day);
            Assert.Equal(utcComponents.Day, resultUtcComponents.Day);
            Assert.Equal(utcComponents.Month + 1, resultUtcComponents.Month);
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

        [Fact]
        public void ExampleAddOneDay()
        {
            UtcComponents utcComponents = new UtcComponents() { Year = 2022, Day = 8, Hours = 9 };

            UtcComponents resultUtcComponents = DateTimeOperation.Execute("now()+1d", utcComponents);

            Assert.Equal("2022-01-09T09:00:00.00Z", resultUtcComponents.ToString());
        }

        [Fact]
        public void ExampleSubtractOneDay()
        {
            UtcComponents utcComponents = new UtcComponents() { Year = 2022, Day = 8, Hours = 9 };

            UtcComponents resultUtcComponents = DateTimeOperation.Execute("now()-1d", utcComponents);

            Assert.Equal("2022-01-07T09:00:00.00Z", resultUtcComponents.ToString());
        }

        [Fact]
        public void ExampleNoOperationAndSnap()
        {
            UtcComponents utcComponents = new UtcComponents() { Year = 2022, Day = 8, Hours = 9 };

            UtcComponents resultUtcComponents = DateTimeOperation.Execute("now()@d", utcComponents);

            Assert.Equal("2022-01-08T00:00:00.00Z", resultUtcComponents.ToString());
        }

        [Fact]
        public void ExampleOperationAndSnap()
        {
            UtcComponents utcComponents = new UtcComponents() { Year = 2022, Day = 8, Hours = 9 };

            UtcComponents resultUtcComponents = DateTimeOperation.Execute("now()-1y@mon", utcComponents);

            Assert.Equal("2021-01-01T00:00:00.00Z", resultUtcComponents.ToString());
        }

        [Fact]
        public void ExampleMultipleOperations()
        {
            UtcComponents utcComponents = new UtcComponents() { Year = 2022, Day = 8, Hours = 9 };

            UtcComponents resultUtcComponents = DateTimeOperation.Execute("now()+10d+12h", utcComponents);

            Assert.Equal("2022-01-18T21:00:00.00Z", resultUtcComponents.ToString());
        }

        [Fact]
        public void ExampleMultipleOperationsAndSnap()
        {
            UtcComponents utcComponents = new UtcComponents() { Year = 2022, Day = 8, Hours = 9 };

            UtcComponents resultUtcComponents = DateTimeOperation.Execute("now()+10d+12h@d", utcComponents);

            Assert.Equal("2022-01-18T00:00:00.00Z", resultUtcComponents.ToString());
        }

        [Fact]
        public void DateTimeMultipleOperations()
        {
            DateTime utcNow = DateTime.UtcNow;

            utcNow = utcNow.AddDays(10).AddHours(12);

            string operationResult = DateTimeOperation.Execute("now()+10d+12h");

            string utcNowToString = utcNow.ToString("yyyy-MM-ddTHH:mm:ss.ffZ");

            Assert.Equal(utcNowToString, operationResult);
        }

        [Fact]
        public void AddCovidIsolationPeriod()
        {
            UtcComponents utcComponents = new UtcComponents { Year = 2022, Day = 1, Month = 1, Hours = 0 };

            UtcComponents resultUtcComponent = DateTimeOperation.Execute("now()+1C", utcComponents);

            Assert.Equal(utcComponents.Day + 10, resultUtcComponent.Day);
            Assert.Equal(utcComponents.Hours + 12, resultUtcComponent.Hours);
        }

        [Fact]
        public void AddTwoCovidIsolationPeriods()
        {
            UtcComponents utcComponents = new UtcComponents { Year = 2022, Day = 1, Month = 1, Hours = 0 };

            UtcComponents resultUtcComponent = DateTimeOperation.Execute("now()+2C", utcComponents);

            Assert.Equal(utcComponents.Day + 21, resultUtcComponent.Day);
            Assert.Equal(utcComponents.Hours, resultUtcComponent.Hours);
        }
    }
}