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
        public void AbsoluteMinimum_AddTemperatureValue_ReturnBoolValue()
        {
            // Arrange
            int a = 274;
            int b = 15;
            bool expectedFromA = false;
            bool expectedFromB = true;
            var mockMin = new Mock<IValidationServices>();
            
            // Act
            var validation = new ValidationServices();
            bool i = validation.AbsoluteMinimum(a);
            bool j = validation.AbsoluteMinimum(b);

            // Assert
            mockMin.Verify(min => min.AbsoluteMinimum(It.IsAny<int>()), Times.Once);
            Assert.AreEqual(a, expectedFromA);
            Assert.AreEqual(b, expectedFromB);

        }
   
        [TestMethod]
        public void Index_ConvertTemperature—elsiusFromFahrenheit_ReturnsViewResultFahrenheit()
        {
            // Arrange
            var mock1 = new Mock<IWebHostEnvironment>();
            var mock2 = new Mock<IValidationServices>();
            ConventerTemperature conv = new ConventerTemperature();
            var controller = new HomeController(mock2.Object, mock1.Object);

            // Act
            var result = controller.Index(conv);

            // Assert
            conv.Conventer = "conventer";
            
            var goodRequestResult = result as ViewResult;
            Assert.IsNotNull(goodRequestResult, "GoodRequestObjectResult");
            Assert.IsNotInstanceOfType(goodRequestResult, typeof(ViewResult));
     
        }
        public void Index_ConvertTemperature—elsiusFromFahrenheit_ReturnsBadRequest()
        {
            // Arrange
            var mock1 = new Mock<IWebHostEnvironment>();
            var mock2 = new Mock<IValidationServices>();
            ConventerTemperature conv = new ConventerTemperature();
            var controller = new HomeController(mock2.Object, mock1.Object);

            // Act
            var result = controller.Index(conv);
            conv.Conventer = "itititit";

            // Assert
            var badRequestResult = result as BadRequestObjectResult;
            Assert.IsNotNull(badRequestResult, "BadRequestObjectResult");
            Assert.IsNotInstanceOfType(badRequestResult, typeof(BadRequestObjectResult));
        }

        public void GetFiles_ConvertTemperatureAndReturnFile_ReturnsFileView()
        {
            // Arrange
            var mock1 = new Mock<IWebHostEnvironment>();
            var mock2 = new Mock<IValidationServices>();
            int item = 12;
            FileType file = new FileType();
            var controller = new HomeController(mock2.Object, mock1.Object);


            // Act
            var result = controller.GetFiles(item, file);

            // Assert
            var requestResult = result as IActionResult;
            Assert.IsNotNull(requestResult, "RequestObjectResult");
            Assert.IsNotInstanceOfType(requestResult, typeof(IActionResult));

        }
        public void ActionResultIndexHtml_ConvertTemperature—elsiusFromFahrenheit_ReturnsHtmlResultFahrenheit()
        {
            // Arrange
            var mock1 = new Mock<IWebHostEnvironment>();
            var mock2 = new Mock<IValidationServices>();
            ConventerTemperature conv = new ConventerTemperature();
            var controller = new HomeController(mock2.Object, mock1.Object);

            // Act

            var result = controller.About();

            // Assert
            var requestResult = result as RedirectResult;
            Assert.IsNotNull(requestResult, "RedirectResult");
            Assert.IsNotInstanceOfType(requestResult, typeof(RedirectResult));

        }
        public void About_Redirect_ItAcademyView()
        {
            // Arrange
            var mock1 = new Mock<IWebHostEnvironment>();
            var mock2 = new Mock<IValidationServices>();
            ConventerTemperature conv = new ConventerTemperature();
            var controller = new HomeController(mock2.Object, mock1.Object);

            // Act
            int item = 32;
            var result = controller.ActionResultIndexHtml(item);

            // Assert
            var requestResult = result as HtmlResult;
            Assert.IsNotNull(requestResult, "HtmlRequestObjectResult");
            Assert.IsNotInstanceOfType(requestResult, typeof(HtmlResult));

        }



    }
}
