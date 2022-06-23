using System;
using System.Collections.Generic;
using Xunit;

namespace TechTestTests
{
    public class TechTestTests
    {
        [Fact]
        public void ExceptionOperationEmpty()
        {
            // Empty string.
            Assert.Throws<TechTest.InvalidOperationException>(() => TechTest.ParseOperation(string.Empty));
        }

        [Fact]
        public void ExceptionOperationInvalid()
        {
            // Completely incorrect string.
            Assert.Throws<TechTest.InvalidOperationException>(() => TechTest.ParseOperation("ThisStringIsInvalid"));
        }

        [Fact]
        public void ExceptionOperationMissingNow()
        {
            // Missing brackets on "now()".
            Assert.Throws<TechTest.InvalidOperationException>(() => TechTest.ParseOperation("+1d"));
        }

        [Fact]
        public void ExceptionOperationMissingNowBrackets()
        {
            // Missing brackets on "now()".
            Assert.Throws<TechTest.InvalidOperationException>(() => TechTest.ParseOperation("now+1d"));
        }

        [Fact]
        public void ExceptionOperationMissingOperator()
        {
            // Missing operator.
            Assert.Throws<TechTest.InvalidOperationException>(() => TechTest.ParseOperation("now()1d"));
        }

        [Fact]
        public void ExceptionOperationMissingCount()
        {
            // Missing count.
            Assert.Throws<TechTest.InvalidOperationException>(() => TechTest.ParseOperation("now()+d"));
        }

        [Fact]
        public void ExceptionOperationMissingUnits()
        {
            // Missing count.
            Assert.Throws<TechTest.InvalidOperationException>(() => TechTest.ParseOperation("now()+1"));
        }

        [Fact]
        public void PlusOperatorSupported()
        {
            TechTest.ParseOperationResult parseOperationResult = TechTest.ParseOperation("now()+3m");

            Assert.Equal("+", parseOperationResult.Operator);
        }

        [Fact]
        public void SubtractOperatorSupported()
        {
            TechTest.ParseOperationResult parseOperationResult = TechTest.ParseOperation("now()-1d");

            Assert.Equal("-", parseOperationResult.Operator);
        }

        [Fact]
        public void CanParseUnitCount()
        {
            TechTest.ParseOperationResult parseOperationResult = TechTest.ParseOperation("now()-1d");

            Assert.Equal("1", parseOperationResult.Count);

            parseOperationResult = TechTest.ParseOperation("now()-1000d");

            Assert.Equal("1000", parseOperationResult.Count);
        }

        [Fact]
        public void SecondsSupported()
        {
            TechTest.ParseOperationResult parseOperationResult = TechTest.ParseOperation("now()-1s");

            Assert.Equal("s", parseOperationResult.Unit);
        }

        [Fact]
        public void MinutesSupported()
        {
            TechTest.ParseOperationResult parseOperationResult = TechTest.ParseOperation("now()-1m");

            Assert.Equal("m", parseOperationResult.Unit);
        }

        [Fact]
        public void HoursSupported()
        {
            TechTest.ParseOperationResult parseOperationResult = TechTest.ParseOperation("now()-1h");

            Assert.Equal("h", parseOperationResult.Unit);
        }

        [Fact]
        public void DaysSupported()
        {
            TechTest.ParseOperationResult parseOperationResult = TechTest.ParseOperation("now()-1d");

            Assert.Equal("d", parseOperationResult.Unit);
        }

        [Fact]
        public void MonthsSupported()
        {
            TechTest.ParseOperationResult parseOperationResult = TechTest.ParseOperation("now()-1mon");

            Assert.Equal("mon", parseOperationResult.Unit);
        }

        [Fact]
        public void YearsSupported()
        {
            TechTest.ParseOperationResult parseOperationResult = TechTest.ParseOperation("now()-1y");

            Assert.Equal("y", parseOperationResult.Unit);
        }

        [Fact]
        public void AddSecond()
        {
            string utcNow = DateTime.UtcNow.ToString();

            string result = TechTest.Execute("now()+1s");

            Assert.NotEqual(string.Empty, result);
        }

        [Fact]
        public void AddSeconds()
        {
            string utcNow = DateTime.UtcNow.ToString();

            string result = TechTest.Execute("now()+1s");

            Assert.NotEqual(string.Empty, result);
        }

        [Fact]
        public void AddMinutes()
        {
            string utcNow = DateTime.UtcNow.ToString();

            string result = TechTest.Execute("now()+1m");

            Assert.NotEqual(string.Empty, result);
        }

        [Fact]
        public void AddHours()
        {
            string utcNow = DateTime.UtcNow.ToString();

            string result = TechTest.Execute("now()+1h");

            Assert.NotEqual(string.Empty, result);
        }

        [Fact]
        public void AddDays()
        {
            string utcNow = DateTime.UtcNow.ToString();

            string result = TechTest.Execute("now()+1d");

            Assert.NotEqual(string.Empty, result);
        }

        [Fact]
        public void AddMonths()
        {
            string utcNow = DateTime.UtcNow.ToString();

            string result = TechTest.Execute("now()+1mon");

            Assert.NotEqual(string.Empty, result);
        }

        [Fact]
        public void AddYears()
        {
            string utcNow = DateTime.UtcNow.ToString();

            string result = TechTest.Execute("now()+1y");

            Assert.NotEqual(string.Empty, result);
        }
    }
}