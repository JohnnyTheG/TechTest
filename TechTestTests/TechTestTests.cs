using System;
using System.Collections.Generic;
using Xunit;

namespace TechTestTests
{
    public class TechTestTests
    {
        [Fact]
        public void AddSeconds()
        {
            UtcUtils.UtcComponents utcComponents = UtcUtils.UtcNowComponents;

            Assert.NotNull(utcComponents);

            string result = TechTest.Execute("now()+1s");

            Assert.NotEqual(utcComponents.ToString(), result);
        }

        [Fact]
        public void AddMinutes()
        {
            string result = TechTest.Execute("now()+1m");

            Assert.NotEqual(string.Empty, result);
        }

        [Fact]
        public void AddHours()
        {
            string result = TechTest.Execute("now()+1h");

            Assert.NotEqual(string.Empty, result);
        }

        [Fact]
        public void AddDays()
        {
            string result = TechTest.Execute("now()+1d");

            Assert.NotEqual(string.Empty, result);
        }

        [Fact]
        public void AddMonths()
        {
            string result = TechTest.Execute("now()+1mon");

            Assert.NotEqual(string.Empty, result);
        }

        [Fact]
        public void AddYears()
        {
            string result = TechTest.Execute("now()+1y");

            Assert.NotEqual(string.Empty, result);
        }
    }
}