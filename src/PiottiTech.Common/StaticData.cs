using System.Collections.Generic;

namespace PiottiTech.Common
{
    public class StaticData
    {
        /// <summary>
        /// Returns 2 char abbreviations. 51 items. Includes DC.
        /// </summary>
        /// <returns></returns>
        public static List<string> USStateCodes()
        {
            string[] saStates = { "AL", "AK", "AZ", "AR", "CA", "CO", "CT", "DE", "DC", "FL", "GA",
                                  "HI", "ID", "IL", "IN", "IA", "KS", "KY", "LA", "ME", "MD",
                                  "MA", "MI", "MN", "MS", "MO", "MT", "NE", "NV", "NH", "NJ",
                                  "NM", "NY", "NC", "ND", "OH", "OK", "OR", "PA", "RI", "SC",
                                  "SD", "TN", "TX", "UT", "VT", "VA", "WA", "WV", "WI", "WY"};

            return new List<string>(saStates);
        }
    }
}