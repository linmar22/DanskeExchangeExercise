using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExchangeExercise.ExchangeLib.Enums;

namespace ExchangeExercise.ExchangeLib.DAL
{
    /// <summary>
    /// Class for retrieving exchange rate information. In the DAL namespace because this would logically contact an external data source for this information.
    /// </summary>
    public class ExchangeRateProvider
    {

        private Dictionary<IsoCurrency,decimal> _conversionDictionary = new Dictionary<IsoCurrency, decimal>()
        {
            {IsoCurrency.EUR, 743.94m},
            {IsoCurrency.USD, 663.11m},
            {IsoCurrency.GBP, 852.85m},
            {IsoCurrency.SEK, 76.10m},
            {IsoCurrency.NOK, 78.40m},
            {IsoCurrency.CHF, 683.58m},
            {IsoCurrency.JPY, 5.9740m},
            {IsoCurrency.DKK, 100.00m}
        };

        /// <summary>
        /// Default constructor
        /// </summary>
        public ExchangeRateProvider()
        {

        }

        /// <summary>
        /// Constructur with the ability to override the exchange rate dictionary for testability.
        /// </summary>
        /// <param name="conversionTableDictionary"></param>
        public ExchangeRateProvider(Dictionary<IsoCurrency,decimal> conversionTableDictionary)
        {
            if (conversionTableDictionary.Any())
            {
                _conversionDictionary = conversionTableDictionary;
            }
        }

        /// <summary>
        /// Gets the exchange for two given currencies
        /// </summary>
        /// <param name="sellCurrency">the currency being sold</param>
        /// <param name="buyCurrency">the currency being bought</param>
        /// <returns></returns>
        public decimal GetExchangeRate(IsoCurrency sellCurrency, IsoCurrency buyCurrency)
        {
            try
            {
                var sellCurrencyDkkRate = _conversionDictionary.First(k => k.Key == sellCurrency).Value;
                var buyCurrencyDkkRate = _conversionDictionary.First(k => k.Key == buyCurrency).Value;
                var rate = sellCurrencyDkkRate / buyCurrencyDkkRate;
                return rate;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
