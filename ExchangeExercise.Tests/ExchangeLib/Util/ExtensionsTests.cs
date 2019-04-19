using System;
using ExchangeExercise.ExchangeLib.Enums;
using ExchangeExercise.ExchangeLib.Util;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExchangeExercise.Tests.ExchangeLib.Util
{
    [TestClass]
    public class ExtensionsTests
    {
        [TestMethod]
        public void AsIsoCurrency_ValidInput_ReturnsIsoCurrency()
        {
            //Arrange
            var toConvet = "DKK";
            var expectedResult = IsoCurrency.DKK;

            //Act
            IsoCurrency result = IsoCurrency.Unknown;

            Action a = () => { result = toConvet.AsIsoCurrency(); };
            a.Invoke();

            //Assert
            result.Should().Be(expectedResult, "the input was valid");
            a.Should().NotThrow("the input was valid");
        }

        [TestMethod]
        public void AsIsoCurrency_InvalidInput_ReturnsUnknown()
        {
            //Arrange
            var toConvet = "DKKa";
            var expectedResult = IsoCurrency.Unknown;

            //Act
            IsoCurrency result = IsoCurrency.DKK;

            Action a = () => { result = toConvet.AsIsoCurrency(); };
            a.Invoke();

            //Assert
            result.Should().Be(expectedResult, "the input was not a real currency");
        }

        [TestMethod]
        public void IsDecimal_ValidInput_ReturnsTrue()
        {
            //Arrange
            var toCheck = new string[]
            {
                "1", "-1","-1,0", "1,0","-1,000000001","1,000000001","0,1","-0,1",",1"
            };

            //Act
            Action a = () =>
            {
                foreach (var s in toCheck)
                {
                    //Assert
                    var result = s.IsDecimal();
                    result.Should().BeTrue("the provided values are decimals");
                }
            };

            a.Should().NotThrow("all provided values are decimals");
        }

        [TestMethod]
        public void IsDecimal_BadInput_ReturnsFalse()
        {
            //Arrange
            var toCheck = new string[]
            {
                "1.0",".1","a",",",".","-asdf","$"
            };

            //Act
            Action a = () =>
            {
                foreach (var s in toCheck)
                {
                    //Assert
                    var result = s.IsDecimal();
                    result.Should().BeFalse("the provided values are not decimals");
                }
            };

            a.Should().NotThrow("all provided values are not decimals");
        }
    }
}
