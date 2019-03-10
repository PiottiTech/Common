using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiottiTech.Common
{
    public static class Validator
    {
        #region Strings

        public static Result Required(string value, string displayName)
        {
            return (value.IsPopulated()) ? new Result(true) : new Result(false, displayName + " is required.");
        }

        public static Result USStateCodeOptional(string value, string displayName)
        {
            return value.IsPopulated() ? USStateCodeRequired(value, displayName) : new Result(true);
        }

        public static Result USStateCodeRequired(string value, string displayName)
        {
            return (value.IsUSStateCode()) ? new Result(true) : new Result(false, displayName + " is not a valid US State.");
        }

        public static Result ZipCodeShortOptional(string value, string displayName)
        {
            return value.IsPopulated() ? ZipCodeShortRequired(value, displayName) : new Result(true);
        }

        public static Result ZipCodeShortRequired(string value, string displayName)
        {
            return value.IsZipCodeShort() ? new Result(true) : new Result(false, displayName + " is not a valid zip code.");
        }

        public static Result ZipCodeLongRequired(string value, string displayName)
        {
            return value.IsZipCodeLong() ? new Result(true) : new Result(false, displayName + " is not a valid zip code.");
        }

        public static Result PhoneTenDigitOptional(string value, string displayName)
        {
            return value.IsPopulated() ? PhoneTenDigitRequired(value, displayName) : new Result(true);
        }

        public static Result PhoneTenDigitRequired(string value, string displayName)
        {
            return Numeric(value, displayName) + ExactLength(value, displayName, 10);
        }

        public static Result MaxLength(string value, string displayName, int maxLength)
        {
            return value.IsLongerThan(maxLength) ? new Result(false, $"{displayName} cannot be more than {maxLength} characters long.") : new Result(true);
        }

        public static Result RequiredWithMaxLength(string value, string displayName, int maxLength)
        {
            return Required(value, displayName) + MaxLength(value, displayName, maxLength);
        }

        public static Result ExactLength(string value, string displayName, int exactLength)
        {
            return value.IsExactLength(exactLength) ? new Result(true) : new Result(false, $"{displayName}  must have exact length of {exactLength.ToString()}.");
        }

        public static Result Decimal(string value, string displayName)
        {
            return value.IsDecimal() ? new Result(true) : new Result(false, $"{displayName} is not a valid decimal.");
        }

        public static Result DecimalNPlaces(string value, string displayName, int nPlaces)
        {
            return value.IsDecimalNPlaces(nPlaces) ? new Result(true) : new Result(false, $"{displayName} is not a valid decimal containing {nPlaces.ToString()} decimal places.");
        }

        public static Result Numeric(string value, string displayName)
        {
            return value.IsNumeric() ? new Result(true) : new Result(false, $"{displayName} must be numeric.");
        }

        public static Result Alphanumeric(string value, string displayName)
        {
            return value.IsAlphanumeric() ? new Result(true) : new Result(false, $"{displayName} must be alphanumeric.");
        }

        public static Result AlphaSpacesAllowed(string value, string displayName)
        {
            return value.IsAlphaSpacesAllowed() ? new Result(true) : new Result(false, $"{displayName} may only contain letters and spaces.");
        }

        public static Result AlphaDashesAllowed(string value, string displayName)
        {
            return value.IsAlphaDashesAllowed() ? new Result(true) : new Result(false, $"{displayName} may only contain letters and dashes.");
        }

        public static Result AlphanumericSpacesAllowed(string value, string displayName)
        {
            return value.IsAlphanumericSpacesAllowed() ? new Result(true) : new Result(false, $"{displayName} may only contain alphanumerics and spaces.");
        }

        public static Result AlphanumericDashesAllowed(string value, string displayName)
        {
            return value.IsAlphanumericDashesAllowed() ? new Result(true) : new Result(false, $"{displayName} may only contain alphanumerics and dashes.");
        }

        public static Result AlphaSpacesDashesAllowed(string value, string displayName)
        {
            return value.IsAlphaSpacesDashesAllowed() ? new Result(true) : new Result(false, $"{displayName} may only contain letters, spaces, and dashes.");
        }

        public static Result AlphanumericSpacesDashesAllowed(string value, string displayName)
        {
            return value.IsAlphanumericSpacesDashesAllowed() ? new Result(true) : new Result(false, $"{displayName} may only contain alphanumerics, spaces, and dashes.");
        }

        public static Result EmailRequired(string value, string displayName)
        {
            return value.Contains("@") ? RequiredWithMaxLength(value, displayName, 254) : new Result(false, displayName + " is not a valid email address.");
        }

        public static Result EmailOptional(string value, string displayName)
        {
            return value.IsPopulated() ? EmailRequired(value, displayName) : new Result(true);
        }

        #endregion Strings

        #region Integers

        public static Result Required(int value, string displayName)
        {
            return (value == 0) ? new Result(false, displayName + " is required.") : new Result(true);
        }

        public static Result RequiredPositive(int value, string displayName)
        {
            return (value > 0) ? new Result(true) : new Result(false, displayName + " must be greater than zero.");
        }

        public static Result InBetweenInclusive(int value, int minValue, int maxValue, string displayName)
        {
            return ((value >= minValue) && (value <= maxValue)) ? new Result(true) : new Result(false, $"{displayName} must be between {minValue} and {maxValue} inclusive.");
        }

        #endregion Integers

        #region Dates

        public static Result DateFuture(DateTime value, string displayName)
        {
            return (value.Date > DateTime.Now.Date) ? new Result(true) : new Result(false, $"{displayName} must be in the future.");
        }

        public static Result DateFutureOrToday(DateTime value, string displayName)
        {
            return (value.Date >= DateTime.Now.Date) ? new Result(true) : new Result(false, $"{displayName} must be today or in the future.");
        }

        public static Result DatePast(DateTime value, string displayName)
        {
            return (value.Date < DateTime.Now.Date) ? new Result(true) : new Result(false, $"{displayName} must be in the past.");
        }

        public static Result DatePastOrToday(DateTime value, string displayName)
        {
            return (value.Date <= DateTime.Now.Date) ? new Result(true) : new Result(false, $"{displayName} must be today or in the past.");
        }

        public static Result DateBetween(DateTime value, string displayName, DateTime startDate, DateTime endDate, string displayDateFormat)
        {
            return (value.Date.IsBetween(startDate.Date, endDate.Date)) ? new Result(true) : new Result(false, $"{displayName} must be between {startDate.ToString(displayDateFormat)} and {endDate.ToString(displayDateFormat)}.");
        }

        public static Result DateBetweenInclusive(DateTime value, string displayName, DateTime startDate, DateTime endDate, string displayDateFormat)
        {
            return (value.Date.IsBetweenInclusive(startDate.Date, endDate.Date)) ? new Result(true) : new Result(false, $"{displayName} must be between {startDate.ToString(displayDateFormat)} and {endDate.ToString(displayDateFormat)} inclusive.");
        }

        public static Result DateTimeBetween(DateTime value, string displayName, DateTime startDate, DateTime endDate, string displayDateTimeFormat)
        {
            return (value.IsBetween(startDate, endDate)) ? new Result(true) : new Result(false, $"{displayName} must be between {startDate.ToString(displayDateTimeFormat)} and {endDate.ToString(displayDateTimeFormat)}.");
        }

        public static Result DateTimeBetweenInclusive(DateTime value, string displayName, DateTime startDate, DateTime endDate, string displayDateTimeFormat)
        {
            return (value.IsBetweenInclusive(startDate, endDate)) ? new Result(true) : new Result(false, $"{displayName} must be between {startDate.ToString(displayDateTimeFormat)} and {endDate.ToString(displayDateTimeFormat)} inclusive.");
        }

        public static Result DateTimeValidSqlServerDate(DateTime value, string displayName, string displayDateTimeFormat)
        {
            return (value.IsValidSqlServerValue()) ? new Result(true) : new Result(false, $"{displayName} is not a valid date.");
        }

        public static Result DateTimeValidSqlServerRange(DateTime startDateTime, string startDateTimeDisplayName, DateTime endDateTime, string endDateTimeDisplayName, string displayDateTimeFormat)
        {
            Result result = DateTimeValidSqlServerDate(startDateTime, startDateTimeDisplayName, displayDateTimeFormat) +
                            DateTimeValidSqlServerDate(endDateTime, endDateTimeDisplayName, displayDateTimeFormat);
            if (!result.Success) { return result; }
            //DEVNOTE: If dates are valid SqlServer Dates, Validate further.
            return (endDateTime < startDateTime) ? new Result(false, $"{endDateTimeDisplayName} cannot be less than {startDateTimeDisplayName}.") : new Result(true);
        }

        #endregion Dates

        #region Guids

        public static Result Required(Guid value, string displayName)
        {
            return (value.IsValued()) ? new Result(true) : new Result(false, displayName + " is required.");
        }
        #endregion

    }
}
