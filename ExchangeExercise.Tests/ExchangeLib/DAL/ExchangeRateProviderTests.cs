using System;
using System.Collections.Generic;
using ExchangeExercise.ExchangeLib.DAL;
using ExchangeExercise.ExchangeLib.Enums;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExchangeExercise.Tests.ExchangeLib.DAL
{
    [TestClass]
    public class ExchangeRateProviderTests
    {
        [TestMethod]
        public void GetExchangeRate_ValidInput_ValidOutput()
        {
            //ARRANGE
            var exchangeRateProv = new ExchangeRateProvider();

            var sellInput = IsoCurrency.CHF;
            var buyInput = IsoCurrency.GBP;

            //ACT
            var result = -1m;

            Action a = () => { result = exchangeRateProv.GetExchangeRate(sellInput, buyInput); };
            a.Invoke();

            //ASSERT
            result.Should().BeOfType(typeof(decimal), "unless something malformed it, it should end up as a decimal");
            result.Should().BePositive("an exchange rate can never be negative, only higher or lower than 1");
            a.Should().NotThrow("the input was valid, so it should not throw anything");
        }

        [TestMethod]
        public void GetExchangeRate_BadInput_Throws()
        {
            //ARRANGE
            var _conversionDictionaryMissingCurrency = new Dictionary<IsoCurrency, decimal>()
            {
                {IsoCurrency.GBP, 743.94m}
            };

            var exchangeRateProv = new ExchangeRateProvider(_conversionDictionaryMissingCurrency);

            var sellInput = IsoCurrency.EUR;
            var buyInput = IsoCurrency.GBP;

            //ACT
            Action a = () => { exchangeRateProv.GetExchangeRate(sellInput, buyInput); };
            

            //ASSERT
            a.Should().Throw<InvalidOperationException>("one of the currencies was missing, so it should throw an exception");
        }
    }
}