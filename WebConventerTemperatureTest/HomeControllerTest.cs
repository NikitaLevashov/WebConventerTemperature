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
    public class HomeControllerTest
    {
        [TestMethod]
        public void TemperatureConverter_ConvertTemperature—elsiusFromFahrenheit_ReturnsViewResultFahrenheit()
        {
            // Arrange
            var mockValidationServicesTrue = new Mock<IValidationServices>();
            var mockValidationServicesFalse = new Mock<IValidationServices>();
            TemperatureModel modelTemperature = new TemperatureModel();
            var controller = new HomeController(mockValidationServicesTrue.Object);
            mockValidationServicesTrue.Setup(repo => repo.IsValidTemperature(12)).Returns(true);
            mockValidationServicesFalse.Setup(repo => repo.IsValidTemperature(280)).Returns(false);

            // Act
            var result = controller.TemperatureConverter(modelTemperature);

            // Assert
            mockValidationServicesTrue.Verify(min => min.IsValidTemperature(It.IsAny<int>()), Times.Once);
            mockValidationServicesFalse.Verify(min => min.IsValidTemperature(It.IsAny<int>()), Times.Once);
            var goodRequestResult = result as ViewResult;
            Assert.IsNotNull(goodRequestResult);
        }
        public void About_ConvertTemperature—elsiusFromFahrenheit_ReturnsItAcademyView()
        {
            // Arrange
            var mockValidationServices = new Mock<IValidationServices>();
            TemperatureModel conv = new TemperatureModel();
            var controller = new HomeController(mockValidationServices.Object);

            // Act
            var result = controller.About();

            // Assert
            var goodRequestResult = result as ViewResult;
            Assert.IsNotNull(goodRequestResult);
        }
        public void GetFile_ConvertTemperatureAndReturnFile_ReturnsFileView()
        {
            // Arrange
            var mockValidationServicesTrue = new Mock<IValidationServices>();
            var mockValidationServicesFalse = new Mock<IValidationServices>();
            TemperatureModel modelTemperature = new TemperatureModel();
            var controller = new HomeController(mockValidationServicesTrue.Object);
            mockValidationServicesTrue.Setup(repo => repo.IsValidTemperature(12)).Returns(true);
            mockValidationServicesFalse.Setup(repo => repo.IsValidTemperature(280)).Returns(false);

            // Act
            var result = controller.TemperatureConverter(modelTemperature);

            // Assert
            mockValidationServicesTrue.Verify(min => min.IsValidTemperature(It.IsAny<int>()), Times.Once);
            mockValidationServicesFalse.Verify(min => min.IsValidTemperature(It.IsAny<int>()), Times.Once);
            var goodRequestResult = result as ViewResult;
            Assert.IsNotNull(goodRequestResult);

        }
        public void ActionResultHtml_ConvertTemperature—elsiusFromFahrenheit_ReturnsHtmlResultFahrenheit()
        {
            // Arrange
            var mockValidationServicesTrue = new Mock<IValidationServices>();
            var mockValidationServicesFalse = new Mock<IValidationServices>();
            TemperatureModel modelTemperature = new TemperatureModel();
            var controller = new HomeController(mockValidationServicesTrue.Object);
            mockValidationServicesTrue.Setup(repo => repo.IsValidTemperature(12)).Returns(true);
            mockValidationServicesFalse.Setup(repo => repo.IsValidTemperature(280)).Returns(false);

            // Act
            var result = controller.TemperatureConverter(modelTemperature);

            // Assert
            mockValidationServicesTrue.Verify(min => min.IsValidTemperature(It.IsAny<int>()), Times.Once);
            mockValidationServicesFalse.Verify(min => min.IsValidTemperature(It.IsAny<int>()), Times.Once);
            var goodRequestResult = result as HtmlResult;
            Assert.IsNotNull(goodRequestResult);
        }
     
    }
}
