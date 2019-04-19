using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExchangeExercise.ExchangeLib.Controllers;
using ExchangeExercise.ExchangeLib.Util;

namespace ExchangeExercise
{
    /// <summary>
    /// Wraps the application operations
    /// </summary>
    public class WorkBootstrapper
    {
        /// <summary>
        /// Performs the exchange application's main body of work 
        /// </summary>
        /// <param name="args">the startup parameters. Format is "[currency pair] [amount]" or "EUR/USD 100"</param>
        /// <returns>a human-readable result as a string</returns>
        public string Work(string[] args = null)
        {
            var toReturn = string.Empty;

            var exchange = new Exchange();

            if (args!=null && args.Any())
            {
                var malformedCurrencyPairEx = new ArgumentException("Unexpected argument or malformed currency pair.");
                var invalidCharInAmountExt = new ArgumentException("Unrecognised character in amount argument");

                var currencyPairs = args[0];

                if (currencyPairs != null)
                {
                    var currencyPairSplit = currencyPairs.Split('/');
                    if (currencyPairSplit.Length != 2)
                    {
                        throw malformedCurrencyPairEx;
                    }

                    var amountToExchangeString = "1";

                    if (args.Length == 2)
                    {
                        if (args[1].IsDecimal())
                        {
                            amountToExchangeString = args[1];
                        }
                        else
                        {
                            throw invalidCharInAmountExt;
                        }
                        
                    }


                    var amountAsDecimal = Convert.ToDecimal(amountToExchangeString, CultureInfo.CurrentCulture);

                    var exchanged = exchange.BuySell(currencyPairSplit[0], currencyPairSplit[1], amountAsDecimal);

                    toReturn = $"{amountAsDecimal} {currencyPairSplit[0]} = {exchanged} {currencyPairSplit[1]}";

                    return toReturn;
                }
                else
                {
                    return malformedCurrencyPairEx.Message;
                }
            }

            return "Usage  : <currency pair> <amount to exchange> \nExample: DKK/SEK 455.5512\nInfo   : Takes the current CultureInfo into account as the decimal separator";
        }
    }
}
