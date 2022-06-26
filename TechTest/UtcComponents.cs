using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechTest
{
    public class UtcComponents : ICloneable
    {
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

        public int Year { get { return values[UnitDefinitions.Years]; } set { values[UnitDefinitions.Years] = value; } }

        public int Month { get { return values[UnitDefinitions.Months]; } set { values[UnitDefinitions.Months] = value; } }

        public int Day { get { return values[UnitDefinitions.Days]; } set { values[UnitDefinitions.Days] = value; } }

        public int Hours { get { return values[UnitDefinitions.Hours]; } set { values[UnitDefinitions.Hours] = value; } }

        public int Minutes { get { return values[UnitDefinitions.Minutes]; } set { values[UnitDefinitions.Minutes] = value; } }

        public int Seconds { get { return values[UnitDefinitions.Seconds]; } set { values[UnitDefinitions.Seconds] = value; } }

        public int Milliseconds { get { return values[UnitDefinitions.Milliseconds]; } set { values[UnitDefinitions.Milliseconds] = value; } }

        private static int GetMaxDaysForMonth(int month, int year)
        {
            switch (month)
            {
                case 1:
                case 3:
                case 5:
                case 7:
                case 8:
                case 10:
                case 12:
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

        private int GetCountInOneWholeUnit(UnitDefinitions unitDefinition)
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
                    return 24;
                case UnitDefinitions.Minutes:
                case UnitDefinitions.Seconds:
                    return 60;
                case UnitDefinitions.Milliseconds:
                    return 1000;
                default:
                    throw new Exception();
            }
        }

        public void Add(UnitDefinitions unitDefinition, int count)
        {
            ProcessOperation(OperatorDefinitions.Add, unitDefinition, count);
        }

        public void Subtract(UnitDefinitions unitDefinition, int count)
        {
            ProcessOperation(OperatorDefinitions.Subtract, unitDefinition, count);
        }

        private void ProcessOperation(OperatorDefinitions operatorDefinition, UnitDefinitions unitDefinition, int count)
        {
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

                ClampValues(unitDefinition);
            }
        }

        private void ClampValues(UnitDefinitions unitDefinition)
        {
            int minValue = GetMinValue(unitDefinition);
            int maxValue = GetMaxValue(unitDefinition);
            int totalUnits = GetCountInOneWholeUnit(unitDefinition);

            switch (unitDefinition)
            {
                case UnitDefinitions.Years:
                case UnitDefinitions.Months:
                case UnitDefinitions.Days:
                case UnitDefinitions.Hours:
                case UnitDefinitions.Minutes:
                case UnitDefinitions.Seconds:
                case UnitDefinitions.Milliseconds:

                    if (values[unitDefinition] > maxValue)
                    {
                        if (unitDefinition != UnitDefinitions.Years)
                        {
                            UnitDefinitions parentUnitDefinition = (UnitDefinitions)((int)unitDefinition + 1);

                            values[parentUnitDefinition] += values[unitDefinition] / totalUnits;
                        }

                        values[unitDefinition] = minValue;
                    }
                    else if (values[unitDefinition] < minValue)
                    {
                        if (unitDefinition != UnitDefinitions.Years)
                        {
                            UnitDefinitions parentUnitDefinition = (UnitDefinitions)((int)unitDefinition + 1);

                            values[parentUnitDefinition] -= values[unitDefinition] / totalUnits;
                        }

                        values[unitDefinition] = maxValue;
                    }

                    break;
            }
        }

        public override string ToString()
        {
            return $"{Year.ToString("D4")}-{Month.ToString("D2")}-{Day.ToString("D2")}T{Hours.ToString("D2")}:{Minutes.ToString("D2")}:{Seconds.ToString("D2")}.{Milliseconds.ToString("D2")}";
        }

        public object Clone()
        {
            return new UtcComponents {
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
