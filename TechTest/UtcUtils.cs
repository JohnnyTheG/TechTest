using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TechTest
{
    public static class UtcUtils
    {
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
        public static UtcComponents UtcNowComponents
        {
            get
            {
                Match utcRegexMatch = utcRegex.Match(UtcNowFormatted);

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
}