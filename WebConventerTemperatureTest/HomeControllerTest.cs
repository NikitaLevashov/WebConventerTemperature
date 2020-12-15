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
        public void IndexReturnsAViewResult()
        {
            // Arrange
            var mock1 = new Mock<IWebHostEnvironment>();
            var mock2 = new Mock<IValidationServices>();
            ConventerTemperature conv = new ConventerTemperature();
            
            var controller = new HomeController(mock2.Object, mock1.Object);

            // Act
            var result = controller.Index(conv);

            // Assert
            var badRequestResult = result as ViewResult;
            Assert.IsNotNull(badRequestResult, "BadRequestObjectResult");
            Assert.IsNotInstanceOfType(badRequestResult, typeof(ViewResult));
       
        }

        public void ActionResultIndexHtmlReturnsHtmlResult()
        {
            // Arrange
            var mock1 = new Mock<IWebHostEnvironment>();
            var mock2 = new Mock<IValidationServices>();
            int item = 12;

            var controller = new HomeController(mock2.Object, mock1.Object);

            // Act
            var result = controller.ActionResultIndexHtml(item);

            // Assert
            var badRequestResult = result as HtmlResult;
            Assert.IsNotNull(badRequestResult, "BadRequestObjectResult");
            Assert.IsNotInstanceOfType(badRequestResult, typeof(HtmlResult));

        }

        public void GetFilesReturnsFileView()
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
            var badRequestResult = result as ViewResult;
            Assert.IsNotNull(badRequestResult, "BadRequestObjectResult");
            Assert.IsNotInstanceOfType(badRequestResult, typeof(ViewResult));

        }

        [TestMethod]
        public void AbsolutabsoluteMinimumReturnBool()
        {
            // Arrange
            var mock1 = new Mock<IValidationServices>();
            int item = 12;
            mock1.Setup(x => x.AbsolutabsoluteMinimum(item));

            IValidationServices val = null;

            // Act
            var result = val.AbsolutabsoluteMinimum(item);

            // Assert
            var badRequestResult = result;
            Assert.IsNotNull(badRequestResult, "BadRequestObjectResult");
            Assert.IsNotInstanceOfType(badRequestResult, typeof(bool));

        }
    
    }
}
