using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechTest
{
    public class UtcComponents : ICloneable
    {
        /// <summary>
        /// The values for the utc representation.
        /// </summary>
        private Dictionary<UnitDefinitions, int> values = new Dictionary<UnitDefinitions, int>
        {
            { UnitDefinitions.Years, 0 },
            { UnitDefinitions.Months, 1 },
            { UnitDefinitions.Days, 1 },
            { UnitDefinitions.Hours, 0 },
            { UnitDefinitions.Minutes, 0 },
            { UnitDefinitions.Seconds, 0 },
            { UnitDefinitions.Milliseconds, 0 },
        };

        /// <summary>
        /// The value of the year.
        /// </summary>
        public int Year { get { return values[UnitDefinitions.Years]; } set { values[UnitDefinitions.Years] = value; } }

        /// <summary>
        /// The value of the month.
        /// </summary>
        public int Month { get { return values[UnitDefinitions.Months]; } set { values[UnitDefinitions.Months] = value; } }

        /// <summary>
        /// The value of the day.
        /// </summary>
        public int Day { get { return values[UnitDefinitions.Days]; } set { values[UnitDefinitions.Days] = value; } }

        /// <summary>
        /// The number of hours represented.
        /// </summary>
        public int Hours { get { return values[UnitDefinitions.Hours]; } set { values[UnitDefinitions.Hours] = value; } }
        
        /// <summary>
        /// The number of minutes represented.
        /// </summary>
        public int Minutes { get { return values[UnitDefinitions.Minutes]; } set { values[UnitDefinitions.Minutes] = value; } }

        /// <summary>
        /// The number of minutes seconds.
        /// </summary>
        public int Seconds { get { return values[UnitDefinitions.Seconds]; } set { values[UnitDefinitions.Seconds] = value; } }

        /// <summary>
        /// The number of milliseconds represented.
        /// </summary>
        public int Milliseconds { get { return values[UnitDefinitions.Milliseconds]; } set { values[UnitDefinitions.Milliseconds] = value; } }

        /// <summary>
        /// Get the number of days in the currently represented month and year.
        /// </summary>
        /// <param name="month"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        private static int GetMaxDaysForMonth(int month, int year)
        {
            switch (month)
            {
                // When days roll backwards, the month will be 0 when checked (then clamped afterwards to 12 (December)).
                case 0:
                case 1:
                case 3:
                case 5:
                case 7:
                case 8:
                case 10:
                case 12:
                // When days roll over, the month will be 13 when checked (then clamped afterwards to 1 (January)).
                case 13:
                    return 31;
                case 4:
                case 6:
                case 9:
                case 11:
                    return 30;
                case 2:
                    return IsLeapYear(year) ? 29 : 28;
                default:
                    return 0;
            }
        }

        /// <summary>
        /// Determines whether the specified year is a leap year.
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        private static bool IsLeapYear(int year)
        {
            if (year % 4 != 0)
            {
                return false;
            }

            if (year % 100 != 0)
            {
                return true;
            }

            if (year % 400 == 0)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Get the minimum possible value for the specified unit type.
        /// </summary>
        /// <param name="unitDefinition"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private static int GetMinValue(UnitDefinitions unitDefinition)
        {
            switch (unitDefinition)
            {
                case UnitDefinitions.Years:
                case UnitDefinitions.Hours:
                case UnitDefinitions.Minutes:
                case UnitDefinitions.Seconds:
                case UnitDefinitions.Milliseconds:
                    return 0;
                case UnitDefinitions.Months:
                case UnitDefinitions.Days:
                    return 1;
                default:
                    throw new Exception();
            }
        }

        /// <summary>
        /// Get the maximum possible value for the specified unit type.
        /// </summary>
        /// <param name="unitDefinition"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public int GetMaxValue(UnitDefinitions unitDefinition)
        {
            switch (unitDefinition)
            {
                case UnitDefinitions.Years:
                    return int.MaxValue;
                case UnitDefinitions.Months:
                    return 12;
                case UnitDefinitions.Days:
                    return GetMaxDaysForMonth(Month, Year);
                case UnitDefinitions.Hours:
                    return 23;
                case UnitDefinitions.Minutes:
                case UnitDefinitions.Seconds:
                    return 59;
                case UnitDefinitions.Milliseconds:
                    return 999;
                default:
                    throw new Exception();
            }
        }

        /// <summary>
        /// Perform an addition using the specified unit type and count.
        /// </summary>
        /// <param name="unitDefinition"></param>
        /// <param name="count"></param>
        public void Add(UnitDefinitions unitDefinition, int count)
        {
            ProcessOperation(OperatorDefinitions.Add, unitDefinition, count);
        }

        /// <summary>
        /// Perform a subtraction using the specified unit type and count.
        /// </summary>
        /// <param name="unitDefinition"></param>
        /// <param name="count"></param>
        public void Subtract(UnitDefinitions unitDefinition, int count)
        {
            ProcessOperation(OperatorDefinitions.Subtract, unitDefinition, count);
        }

        /// <summary>
        /// Interal processing method to perform operations on the values represented by this instance.
        /// </summary>
        /// <param name="operatorDefinition"></param>
        /// <param name="unitDefinition"></param>
        /// <param name="count"></param>
        private void ProcessOperation(OperatorDefinitions operatorDefinition, UnitDefinitions unitDefinition, int count)
        {
            // Break down the count into single steps.
            for (int i = 0; i < count; i++)
            {
                switch (operatorDefinition)
                {
                    case OperatorDefinitions.Add:

                        values[unitDefinition]++;

                        break;

                    case OperatorDefinitions.Subtract:

                        values[unitDefinition]--;

                        break;
                }

                // Once the count has been changed, ensure that there have not been any values which should roll over.
                // e.g. If 60 minutes, this should add an hour and reset minutes back to zero.
                foreach (UnitDefinitions unit in Enum.GetValues(typeof(UnitDefinitions)))
                {
                    if (unit != UnitDefinitions.Undefined)
                    {
                        ClampValues(unit);
                    }
                }
            }
        }

        /// <summary>
        /// Ensures that the specified unit type stays within its acceptable range.
        /// </summary>
        /// <param name="unitDefinition"></param>
        private void ClampValues(UnitDefinitions unitDefinition)
        {
            int minValue = GetMinValue(unitDefinition);
            int maxValue = GetMaxValue(unitDefinition);

            // If the value has crossed its max value (e.g. Seconds or minutes has hit 60)
            if (values[unitDefinition] > maxValue)
            {
                // Reset back to its min value.
                values[unitDefinition] = minValue;

                // No parent unit type of years is supported (centuries, millenia etc.) so years never rolls over in this implementation.
                if (unitDefinition != UnitDefinitions.Years)
                {
                    // Increase the value of the parent unit by 1.
                    UnitDefinitions parentUnitDefinition = (UnitDefinitions)((int)unitDefinition + 1);

                    values[parentUnitDefinition] += 1;
                }
            }
            // If the value has crossed its max value (e.g. Seconds or minutes has hit -1)
            else if (values[unitDefinition] < minValue)
            {
                // Reset back to its max value.
                values[unitDefinition] = maxValue;

                if (unitDefinition != UnitDefinitions.Years)
                {
                    UnitDefinitions parentUnitDefinition = (UnitDefinitions)((int)unitDefinition + 1);

                    // Decrease parent unit by 1.
                    values[parentUnitDefinition] -= 1;
                }
            }
        }

        /// <summary>
        /// Snap the specified units back to their minimum values.
        /// </summary>
        /// <param name="snappedUnitDefinitions"></param>
        public void Snap(IEnumerable<UnitDefinitions> snappedUnitDefinitions)
        {
            foreach (UnitDefinitions snappedUnitDefinition in snappedUnitDefinitions)
            {
                values[snappedUnitDefinition] = GetMinValue(snappedUnitDefinition);
            }
        }

        /// <summary>
        /// Returns a string in the format specified for this test.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{Year.ToString("D4")}-{Month.ToString("D2")}-{Day.ToString("D2")}T{Hours.ToString("D2")}:{Minutes.ToString("D2")}:{Seconds.ToString("D2")}.{Milliseconds.ToString("D2")}Z";
        }

        /// <summary>
        /// IClonable implentation.
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            return new UtcComponents
            {
                Year = Year,
                Month = Month,
                Day = Day,
                Hours = Hours,
                Minutes = Minutes,
                Seconds = Seconds,
                Milliseconds = Milliseconds,
            };
        }
    }

}
