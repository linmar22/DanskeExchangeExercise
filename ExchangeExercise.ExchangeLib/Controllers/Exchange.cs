using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExchangeExercise.ExchangeLib.DAL;
using ExchangeExercise.ExchangeLib.Enums;
using ExchangeExercise.ExchangeLib.Util;

namespace ExchangeExercise.ExchangeLib.Controllers
{
    /// <summary>
    /// Houses code regarding exchange operations
    /// </summary>
    public class Exchange
    {
        /// <summary>
        /// Exchanges the given currencies of a specific amount
        /// </summary>
        /// <returns>the value at which the amount of currency can be bought</returns>
        public decimal BuySell(IsoCurrency sellCurrency, IsoCurrency buyCurrency, decimal amount)
        {
            try
            {
                var erp = new ExchangeRateProvider();
                var rate = erp.GetExchangeRate(sellCurrency, buyCurrency);
                var exchanged = amount * rate;
                var bankingRounded = decimal.Round(exchanged, 4, MidpointRounding.AwayFromZero);
                if (exchanged < 0.0001m)
                {
                    return exchanged;
                }
                return bankingRounded;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Unexpected error in Exchange.BuySell() with message: \n {e.Message}");
                throw;
            }
        }

        /// <summary>
        /// Exchanges the given currencies of a specific amount
        /// </summary>
        /// <returns>the value at which the amount of currency can be bought</returns>
        public decimal BuySell(string sellCurrency, string buyCurency, decimal amount)
        {
            var sellIso = sellCurrency.AsIsoCurrency();
            var buyIso = buyCurency.AsIsoCurrency();

            if (sellIso == IsoCurrency.Unknown || buyIso == IsoCurrency.Unknown)
            {
                throw new ArgumentException("Error parsing the buy/sell currencies!");
            }

            return BuySell(sellIso, buyIso, amount);
        }

    }
}
