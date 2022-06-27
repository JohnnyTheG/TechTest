using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechTest
{
    public static class DateTimeOperationUtils
    {
        public static UnitDefinitions ParseUnitDefinition(string unit)
        {
            switch (unit.ToLower())
            {
                case "y":

                    return UnitDefinitions.Years;

                case "mon":

                    return UnitDefinitions.Months;

                case "d":

                    return UnitDefinitions.Days;

                case "h":

                    return UnitDefinitions.Hours;

                case "m":

                    return UnitDefinitions.Minutes;

                case "s":

                    return UnitDefinitions.Seconds;

                default:

                    return UnitDefinitions.Undefined;
            }
        }

    }
}
