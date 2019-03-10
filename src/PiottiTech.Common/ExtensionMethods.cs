using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace PiottiTech.Common
{
    public static class ExtensionMethods
    {
        #region Guid Extension Methods

        public static bool IsValued(this Guid value)
        {
            return (value != Guid.Empty);
        }

        #endregion Guid Extension Methods

        #region String Extensions

        public static string Remove(this string s, string stringToRemove)
        {
            return s.Replace(stringToRemove, String.Empty);
        }

        public static string Remove(this string s, string[] stringsToRemove)
        {
            for (int i = 0; i < stringsToRemove.Length; i++)
            {
                s = s.Remove(stringsToRemove[i]);
            }
            return s;
        }

        public static string Truncate(this string s, int maxLength)
        {
            if (s.Length > maxLength)
            {
                s = s.Substring(0, maxLength);
            }
            return s;
        }

        public static string ToStringJson(this Object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        public static string ToConcatenatedString(this List<string> list, string delimiter)
        {
            string s = String.Empty;
            foreach (string item in list)
            {
                s = s + item + delimiter;
            }
            return s;
        }

        public static IEnumerable<string> ToCleanWhiteList(this string whitelist)
        {
            return whitelist.ToLower().Replace(" ", "").Replace("\r", "").Replace("\n", "").Replace("\t", "").TrimEnd(',').Split(',');
        }

        public static bool IsPopulated(this string value)
        {
            return (value.Length > 0);
        }

        public static bool IsUSStateCode(this string value)
        {
            return StaticData.USStateCodes().Contains(value);
        }

        public static bool IsZipCodeShort(this string value)
        {
            return Regex.IsMatch(value, RegularExpressionConstants.REGEX_ZIP_CODE_SHORT);
        }

        public static bool IsZipCodeLong(this string value)
        {
            return Regex.IsMatch(value, RegularExpressionConstants.REGEX_ZIP_CODE_LONG);
        }

        public static bool IsLongerThan(this string value, int maxLength)
        {
            return (value.Length > maxLength);
        }

        public static bool IsExactLength(this string value, int exactLength)
        {
            return (value.Length == exactLength);
        }

        public static bool IsDecimal(this string value)
        {
            return Regex.IsMatch(value, RegularExpressionConstants.REGEX_DECIMAL);
        }

        public static bool IsDecimalNPlaces(this string value, int nPlaces)
        {
            string reg = RegularExpressionConstants.REGEX_DECIMAL_N_PLACES.Replace("[n]", nPlaces.ToString());
            return Regex.IsMatch(value, reg);
        }

        public static bool IsNumeric(this string value)
        {
            return Regex.IsMatch(value, RegularExpressionConstants.REGEX_NUMERIC);
        }

        public static bool IsAlphanumeric(this string value)
        {
            return Regex.IsMatch(value, RegularExpressionConstants.REGEX_ALPHANUMERIC);
        }

        public static bool IsAlphaSpacesAllowed(this string value)
        {
            return Regex.IsMatch(value, RegularExpressionConstants.REGEX_ALPHA_SPACES_ALLOWED);
        }

        public static bool IsAlphaDashesAllowed(this string value)
        {
            return Regex.IsMatch(value, RegularExpressionConstants.REGEX_ALPHA_DASHES_ALLOWED);
        }

        public static bool IsAlphanumericSpacesAllowed(this string value)
        {
            return Regex.IsMatch(value, RegularExpressionConstants.REGEX_ALPHANUMERIC_SPACES_ALLOWED);
        }

        public static bool IsAlphanumericDashesAllowed(this string value)
        {
            return Regex.IsMatch(value, RegularExpressionConstants.REGEX_ALPHANUMERIC_DASHES_ALLOWED);
        }

        public static bool IsAlphaSpacesDashesAllowed(this string value)
        {
            return Regex.IsMatch(value, RegularExpressionConstants.REGEX_ALPHA_SPACES_AND_DASHES_ALLOWED);
        }

        public static bool IsAlphanumericSpacesDashesAllowed(this string value)
        {
            return Regex.IsMatch(value, RegularExpressionConstants.REGEX_ALPHANUMERIC_SPACES_AND_DASHES_ALLOWED);
        }

        #endregion String Extensions

        #region Date Extensions

        public static DateTime FirstDayOfQuarter(this DateTime dayInQuestion)
        {
            int quarter = Quarter(dayInQuestion);
            int firstMonthOfQuarter = (quarter * 3) - 2;
            return new DateTime(dayInQuestion.Year, firstMonthOfQuarter, 1);
        }

        public static int Quarter(this DateTime dayInQuestion)
        {
            int month = dayInQuestion.Month - 1;
            return Math.Abs(month / 3) + 1;
        }

        public static int DayOfQuarter(this DateTime dayInQuestion)
        {
            return (dayInQuestion - dayInQuestion.FirstDayOfQuarter()).Days + 1;
        }

        public static DateTime FirstDayOfYear(this DateTime dayInQuestion)
        {
            return new DateTime(dayInQuestion.Year, 1, 1);
        }

        public static DateTime LastDayOfYear(this DateTime dayInQuestion)
        {
            return new DateTime(dayInQuestion.Year, 12, 31);
        }

        public static bool IsBetween(this DateTime value, DateTime startDate, DateTime endDate)
        {
            return (value > startDate && value < endDate);
        }

        public static bool IsBetweenInclusive(this DateTime value, DateTime startDate, DateTime endDate)
        {
            return (value >= startDate && value <= endDate);
        }

        public static bool IsValidSqlServerValue(this DateTime value)
        {
            return (value >= new DateTime(1753, 1, 1));
        }

        #endregion Date Extensions

        #region Integer Extensions

        public static bool IsBetween(this int num, int lower, int upper, bool inclusive = false)
        {
            return inclusive ? lower <= num && num <= upper : lower < num && num < upper;
        }

        #endregion Integer Extensions
    }
}