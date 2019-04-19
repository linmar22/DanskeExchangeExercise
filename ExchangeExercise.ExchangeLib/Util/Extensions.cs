using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExchangeExercise.ExchangeLib.Enums;

namespace ExchangeExercise.ExchangeLib.Util
{
    /// <summary>
    /// Houses base type extension methods for the exercise
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Attempts to parse the string into an IsoCurrency Enum.
        /// </summary>
        /// <param name="currencyIso">a string representation of the 3-letter ISO currency</param>
        /// <returns>IsoCurrency if successful, IsoCurrency.Unknown if not</returns>
        public static IsoCurrency AsIsoCurrency(this string currencyIso)
        {
            var parsed = Enum.TryParse(currencyIso.Trim().ToUpper(),true, out IsoCurrency toReturn);

            if (parsed)
            {
                return toReturn;
            }

            return IsoCurrency.Unknown;
        }

        /// <summary>
        /// Checks the string wether it's a decimal. Depends on CultureInfo.CurrentCulture for the decimal separator.
        /// </summary>
        /// <param name="stringToCheck">the string to check</param>
        /// <returns>true if the string is a decimal, otherwise false</returns>
        public static bool IsDecimal(this string stringToCheck)
        {
            var canConvert = decimal.TryParse(stringToCheck.Trim().ToLower(),out var number);

            return canConvert;
        }
    }
}
