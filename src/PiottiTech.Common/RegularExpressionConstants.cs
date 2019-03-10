namespace PiottiTech.Common
{
    public class RegularExpressionConstants
    {
        public const string REGEX_ALPHANUMERIC = @"^\w+$";
        public const string REGEX_NUMERIC = @"^\d+$";
        public const string REGEX_ALPHA_SPACES_ALLOWED = @"^[a-zA-Z' ']+$";
        public const string REGEX_ALPHA_SPACES_AND_DASHES_ALLOWED = @"^[a-zA-Z' '-]+$";
        public const string REGEX_ALPHA_DASHES_ALLOWED = @"^([\w-])+$";

        public const string REGEX_ALPHANUMERIC_SPACES_ALLOWED = @"^[a-zA-Z0-9' ']+$";
        public const string REGEX_ALPHANUMERIC_DASHES_ALLOWED = @"^[a-zA-Z0-9-]+$";
        public const string REGEX_ALPHANUMERIC_SPACES_AND_DASHES_ALLOWED = @"^[a-zA-Z0-9' '\-]+$";
        public const string REGEX_DECIMAL = @"^\d{0,9}\.\d{1,2}$";
        public const string REGEX_ZIP_CODE_SHORT = @"^\d{5}$";
        public const string REGEX_ZIP_CODE_LONG = @"^\d{5}-\d{4}$";

        internal const string REGEX_DECIMAL_N_PLACES = @"^\d{0,9}\.\d{[n]}$";
    }
}