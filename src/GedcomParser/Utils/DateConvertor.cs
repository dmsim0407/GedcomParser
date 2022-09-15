using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;

namespace GedcomParser.Utils
{
    public static class DateConvertor
    {
        public static DateOnly? ConvertToDate(string dateAsString)
        {
            var monthAbbreviations = new Dictionary<string, int>{
                {"JAN", 1},
                {"FEB", 2},
                {"MAR", 3},
                {"APR", 4},
                {"MAY", 5},
                {"JUN", 6},
                {"JUL", 7},
                {"AUG", 8},
                {"SEP", 9},
                {"OCT", 10},
                {"NOV", 11},
                {"DEC", 12},
                {"JANUARY", 1},
                {"FEBRUARY", 2},
                {"MARCH", 3},
                {"APRIL", 4},
                {"JUNE", 6},
                {"JULY", 7},
                {"AUGUST", 8},
                {"SEPTEMBER", 9},
                {"SEPT", 9},
                {"OCTOBER", 10},
                {"NOVEMBER", 11},
                {"DECEMBER", 12},
            };

            // Year only e.g. 2020
            if (Regex.IsMatch(dateAsString, @"^\d{4}$")) {
                return new DateOnly(Convert.ToInt32(dateAsString.Substring(0, 4)), 1, 1);
            }

            // Year only e.g. 0? ___ 2020
            if (Regex.IsMatch(dateAsString, @"^0\?\s___\s\d{4}$")) {
                return new DateOnly(Convert.ToInt32(dateAsString.Substring(7, 4)), 1, 1);
            }

            // Approx year e.g. Abt. 2020 or cir 2020 or C 2020 or C2020
            var approxYearMatch = Regex.Match(dateAsString, @"^(C|cir|Abt.|BEF|AFT|CAL)\s*?(\d{4})$", RegexOptions.IgnoreCase);
            // The expected groups are 0=whole string, 1=C or cir or Abt., 2=year
            if (approxYearMatch.Groups.Count == 3) {
                return new DateOnly(Convert.ToInt32(approxYearMatch.Groups[2].Value), 1, 1);
            }

            // Year and month e.g. Aug 2020
            var monthAndYearMatch = Regex.Match(dateAsString, @"^(\w{3,})\s(\d{4})$");
            // The expected groups are 0=whole string, 1=month, 2=year
            if (monthAndYearMatch?.Groups.Count == 3) {
                return new DateOnly(
                    Convert.ToInt32(monthAndYearMatch.Groups[2].Value),
                    monthAbbreviations[monthAndYearMatch.Groups[1].Value.ToUpper()],
                    1);
            }

            // Year and month e.g. 0? Aug 2020
            var monthAndYearWithInvalidDayMatch = Regex.Match(dateAsString, @"^0\?\s(\w{3})\s(\d{4})$", RegexOptions.IgnoreCase);
            // The expected groups are 0=whole string, 1=month, 2=year
            if (monthAndYearWithInvalidDayMatch?.Groups.Count == 3) {
                return new DateOnly(
                    Convert.ToInt32(monthAndYearWithInvalidDayMatch.Groups[2].Value),
                    DateTime.ParseExact(monthAndYearWithInvalidDayMatch.Groups[1].Value, "MMM", CultureInfo.InvariantCulture).Month,
                    1);
            }

            // Full date e.g. 26 AUG 2020
            var fullDateMatchOne = Regex.Match(dateAsString, @"^(\d{1,2})\s(\w{3,})\s(\d{4})$");
            // The expected groups are 0=whole string, 1=day, 2=month, 3=year
            if (fullDateMatchOne?.Groups.Count == 4 && !fullDateMatchOne.Groups[2].Value.Contains('_')) {
                try {
                    return new DateOnly(
                        Convert.ToInt32(fullDateMatchOne.Groups[3].Value),
                        monthAbbreviations[fullDateMatchOne.Groups[2].Value.ToUpper()],
                        Convert.ToInt32(fullDateMatchOne.Groups[1].Value));
                } catch (ArgumentOutOfRangeException) {
                    return null;
                }
            }

            // Full date e.g. 26/AUG/2020
            var fullDateMatchTwo = Regex.Match(dateAsString, @"^(\d{1,2})/(\w{3,})/(\d{4})$");
            // The expected groups are 0=whole string, 1=day, 2=month, 3=year
            if (fullDateMatchTwo?.Groups.Count == 4 && !fullDateMatchTwo.Groups[2].Value.Contains('_')) {
                try {
                    return new DateOnly(
                        Convert.ToInt32(fullDateMatchTwo.Groups[3].Value),
                        monthAbbreviations[fullDateMatchTwo.Groups[2].Value.ToUpper()],
                        Convert.ToInt32(fullDateMatchTwo.Groups[1].Value));
                } catch (ArgumentOutOfRangeException) {
                    return null;
                }
            }

            // Full date e.g. 26-AUG-2020
            var fullDateMatchThree = Regex.Match(dateAsString, @"^(\d{1,2})-(\w{3,})-(\d{4})$");
            // The expected groups are 0=whole string, 1=day, 2=month, 3=year
            if (fullDateMatchThree?.Groups.Count == 4 && !fullDateMatchThree.Groups[2].Value.Contains('_')) {
                try {
                    return new DateOnly(
                        Convert.ToInt32(fullDateMatchThree.Groups[3].Value),
                        monthAbbreviations[fullDateMatchThree.Groups[2].Value.ToUpper()],
                        Convert.ToInt32(fullDateMatchThree.Groups[1].Value));
                } catch (ArgumentOutOfRangeException) {
                    return null;
                }
            }

            // Full date e.g. 26-8-2020
            var fullDateMatchFour = Regex.Match(dateAsString, @"^(\d{1,2})-(\d{1,2})-(\d{4})$");
            // The expected groups are 0=whole string, 1=day, 2=month, 3=year
            if (fullDateMatchFour?.Groups.Count == 4) {
                try {
                    return new DateOnly(
                        Convert.ToInt32(fullDateMatchFour.Groups[3].Value),
                        Convert.ToInt32(fullDateMatchFour.Groups[2].Value),
                        Convert.ToInt32(fullDateMatchFour.Groups[1].Value));
                } catch (ArgumentOutOfRangeException) {
                    return null;
                }
            }

            // Full date e.g. 2020-8-26
            var fullDateMatchFive = Regex.Match(dateAsString, @"^(\d{4})-(\d{1,2})-(\d{1,2})$");
            // The expected groups are 0=whole string, 1=year, 2=month, 3=day
            if (fullDateMatchFive?.Groups.Count == 4) {
                try {
                    return new DateOnly(
                        Convert.ToInt32(fullDateMatchFive.Groups[1].Value),
                        Convert.ToInt32(fullDateMatchFive.Groups[2].Value),
                        Convert.ToInt32(fullDateMatchFive.Groups[3].Value));
                } catch (ArgumentOutOfRangeException) {
                    return null;
                }
            }

            return null;
        }
    }
}
