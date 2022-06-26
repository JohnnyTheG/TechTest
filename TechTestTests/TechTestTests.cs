using System;
using System.Collections.Generic;
using Xunit;
using TechTest;

namespace TechTestTests
{
    public class TechTestTests
    {
        [Fact]
        public void AddSeconds()
        {
            
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