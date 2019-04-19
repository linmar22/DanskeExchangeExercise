using System;
using ExchangeExercise.ExchangeLib.Controllers;
using ExchangeExercise.ExchangeLib.Enums;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExchangeExercise.Tests.ExchangeLib.Controllers
{
    [TestClass]
    public class ExchangeTests
    {
        [TestMethod]
        public void BuySell_ValidEnumInput_ValidOutput()
        {
            //Arrange
            var exchange = new Exchange();

            var sellCurrency = IsoCurrency.USD;
            var buyCurrency = IsoCurrency.DKK;
            var amountToExchange = 100m;
            var expectedResult = 663.11m;

            //Act
            var result = -1m;

            Action act = () =>
            {
                result = exchange.BuySell(sellCurrency, buyCurrency, amountToExchange);
            };
            act.Invoke();

            //Assert
            result.Should().BePositive("an exchange cannot return negative values");
            act.Should().NotThrow("all input is valid");
            result.Should().Be(expectedResult);
        }

        [TestMethod]
        public void BuySell_ValidStringInput_ValidOutput()
        {
            //Arrange
            var exchange = new Exchange();

            var sellCurrency = "EUR";
            var buyCurrency = "DKK";
            var amountToExchange = 100m;
            var expectedResult = 743.94m;

            //Act
            var result = -1m;

            Action act = () =>
            {
                result = exchange.BuySell(sellCurrency, buyCurrency, amountToExchange);
            };
            act.Invoke();

            //Assert
            result.Should().BePositive("an exchange cannot return negative values");
            act.Should().NotThrow("all input is valid");
            result.Should().Be(expectedResult);
        }
        
        [TestMethod]
        public void BuySell_InvalidStringInput_Throws()
        {
            //Arrange
            var exchange = new Exchange();

            var sellCurrency = "LTL";
            var buyCurrency = "DKK";
            var amountToExchange = 100m;

            //Act
            Action act = () =>
            {
                exchange.BuySell(sellCurrency, buyCurrency, amountToExchange);
            };

            //Assert
            act.Should().Throw<ArgumentException>("because one of the currencies does not exist in the table");
        }

        [TestMethod]
        public void BuySell_InvalidInput_Throws()
        {

        }
    }
}
