using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

public static class UtcUtils
{
    public class UtcComponents
    {
        private Dictionary<UnitDefinitions, int> values = new Dictionary<UnitDefinitions, int>
        {
            { UnitDefinitions.Years, 0 },
            { UnitDefinitions.Months, 0 },
            { UnitDefinitions.Days, 0 },
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

        public void Add(UnitDefinitions unitDefinition, int count)
        {
            values[unitDefinition] += count;
        }

        public void Subtract(UnitDefinitions unitDefinition, int count)
        {
            Add(unitDefinition, count * -1);
        }

        public override string ToString()
        {
            return $"{Year}-{Month}-{Day}T{Hours}:{Minutes}:{Seconds}.{Milliseconds}";
        }
    }

    /// <summary>
    /// Get UtcNow in correct format for test.
    /// </summary>
    private static string UtcNowFormatted { get { return DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.ffZ"); } }

    /// <summary>
    /// Regex to extract values from utc now.
    /// </summary>
    private static Regex utcRegex = new Regex("(?<year>\\d{4})-(?<month>\\d{2})-(?<day>\\d{2})T(?<hours>\\d{2}):(?<minutes>\\d{2}):(?<seconds>\\d{2}).(?<milliseconds>\\d{2})Z");

    /// <summary>
    /// Get current utc time broken down into components.
    /// </summary>
    public static UtcComponents? UtcNowComponents
    {
        get
        {
            Match utcRegexMatch = utcRegex.Match(UtcNowFormatted);

            if (!utcRegexMatch.Success)
            {
                return null;
            }

            UtcComponents utcComponents = new UtcComponents
            {
                Year = int.Parse(utcRegexMatch.Groups["year"].Value),
                Month = int.Parse(utcRegexMatch.Groups["month"].Value),
                Day = int.Parse(utcRegexMatch.Groups["day"].Value),
                Hours = int.Parse(utcRegexMatch.Groups["hours"].Value),
                Minutes = int.Parse(utcRegexMatch.Groups["minutes"].Value),
                Seconds = int.Parse(utcRegexMatch.Groups["seconds"].Value),
                Milliseconds = int.Parse(utcRegexMatch.Groups["milliseconds"].Value),
            };

            return utcComponents;
        }
    }

}