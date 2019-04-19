using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExchangeExercise.Tests.ExchangeExercise
{
    [TestClass]
    public class BootstrapperTests
    {
        [TestMethod]
        public void PerformExchange_ValidArgs_OutputIsValid()
        {
            //Arrange
            SetCulture();

            var bootstrapper = new WorkBootstrapper();
            var args = new string[]{"DKK/SEK","152,55"};
            
            //Act
            var result = string.Empty;

            Action a = () =>
            {
                result = bootstrapper.Work(args);

            };

            //Assert
            a.Should().NotThrow("the input was valid");
            result.Should().NotBe(string.Empty);
            result.Should().Be("152,55 DKK = 200,4599 SEK");
        }

        [TestMethod]
        public void PerformExchange_MissingAmount_OutputIsExchangeRate()
        {
            //Arrange
            SetCulture();

            var bootstrapper = new WorkBootstrapper();
            var args = new string[]{"DKK/SEK"};
            
            //Act
            var result = string.Empty;

            Action a = () =>
            {
                result = bootstrapper.Work(args);
            };

            //Assert
            a.Should().NotThrow("the input was valid");
            result.Should().NotBe(string.Empty);
            result.Should().Be("1 DKK = 1,3141 SEK");
        }

        [TestMethod]
        public void PerformExchange_NoInput_OutputIsUsageString()
        {
            //Arrange
            SetCulture();

            var bootstrapper = new WorkBootstrapper();
            var args = new string[]{};
            
            //Act
            var result = string.Empty;

            Action a = () =>
            {
                result = bootstrapper.Work(args);
            };

            //Assert
            a.Should().NotThrow("the input was valid");
            string.IsNullOrWhiteSpace(result).Should().BeFalse("the application should output a usage string");
            result.StartsWith("Usage").Should().BeTrue("the application should output a usage string");
        }

        [TestMethod]
        public void PerformExchange_BadArgs_OutputIsUsage()
        {
            //Arrange
            SetCulture();

            var bootstrapper = new WorkBootstrapper();
            var argList = new List<string[]>()
            {
                new string[]{"DKKSEK","12345"},
                new string[]{"12345"},
                new string[]{@"DKK\SEK"},
                new string[]{"this isn't even close to what the application expects"}
            };
            
            //Act

            Action a = () =>
            {
                foreach (var args in argList)
                {
                    var result = bootstrapper.Work(args);

                    string.IsNullOrWhiteSpace(result).Should().BeFalse("the application should output an error");
                    result.StartsWith("Unexpected").Should().BeTrue();
                }
            };

            //Assert
            a.Should().Throw<ArgumentException>("the input was valid");
        }

        [TestMethod]
        public void PerformExchange_InvalidDecimalSeparator_Throws()
        {
            //Arrange
            SetCulture();

            var bootstrapper = new WorkBootstrapper();
            var args = new string[]{"USD/CHF","123.78"};
            
            //Act
            
            Action a = () =>
            {
                bootstrapper.Work(args);
            };

            //Assert
            a.Should().Throw<ArgumentException>("the amount argument had a wrong delimiter").WithMessage("Unrecognised character in amount argument");
        }

        [TestMethod]
        public void PerformExchange_DkCultureMachine_ValidOutput()
        {
            //Arrange
            SetCulture("da-DK");

            var bootstrapper = new WorkBootstrapper();
            var args = new string[]{"USD/CHF","123.78"};
            
            //Act
            var result = string.Empty;
            Action a = () =>
            {
                result = bootstrapper.Work(args);
            };
            a.Invoke();

            //Assert
            a.Should().NotThrow("the current culture uses '.' as a decimal delimiter");
            result.Should().NotBe(string.Empty);
            result.Should().Be("12378 USD = 12007,3372 CHF");
        }

        [TestMethod]
        public void PerformExchange_UsCultureMachine_ValidOutput()
        {
            //Arrange
            SetCulture("en-US");

            var bootstrapper = new WorkBootstrapper();
            var args = new string[]{"USD/CHF","12,233.78"};
            
            //Act
            var result = string.Empty;
            Action a = () =>
            {
                result = bootstrapper.Work(args);
            };
            a.Invoke();

            //Assert
            a.Should().NotThrow("the current culture uses '.' as a decimal delimiter");
            result.Should().NotBe(string.Empty);
            result.Should().Be("12233.78 USD = 11867.4359 CHF");
        }

        /// <summary>
        /// Sets the culture for the current process
        /// </summary>
        /// <param name="cultureString"></param>
        private void SetCulture(string cultureString="lt-LT")
        {
            var cultureInfo = new CultureInfo(cultureString);
            Thread.CurrentThread.CurrentCulture = cultureInfo;
            Thread.CurrentThread.CurrentUICulture = cultureInfo;
        }
    }
}
