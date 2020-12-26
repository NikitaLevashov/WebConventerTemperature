using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.IO;
using WebConventerTemperature.Controllers;
using WebConventerTemperature.Models;
using WebConventerTemperature.Services;
using WebConventerTemperature.Util;
using static WebConventerTemperature.Helper.Helper;

namespace WebConventerTemperatureTest
{
    [TestClass]
    public class ValidationServicesTest
    {
        [TestMethod]
        public void IsValidTemperature_AddTemperatureWrongValue_ReturnFalseValue()
        {
            // Arrange
            int firstValueTemperature = 274;
            bool expectedFromFirstValueTemperature = false;
          
            // Act
            var validation = new ValidationServices();
            bool resultFirstValueTemperature = validation.IsValidTemperature(firstValueTemperature);
            
            // Assert
            Assert.AreEqual(firstValueTemperature, expectedFromFirstValueTemperature);
        }
        [TestMethod]
        public void IsValidTemperature_AddTemperatureCorrectValue_ReturnTrueValue()
        {
            // Arrange
            int secondValueTemperature = 10;
            bool expectedFromSecondValueTemperature = true;

            // Act
            var validation = new ValidationServices();
            bool resultFirstValueTemperature = validation.IsValidTemperature(secondValueTemperature);

            // Assert
            Assert.AreEqual(secondValueTemperature, expectedFromSecondValueTemperature);
        }
    }
}
