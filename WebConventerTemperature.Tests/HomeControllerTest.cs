using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using WebConventerTemperature.Controllers;
using WebConventerTemperature.Models;
using WebConventerTemperature.Services;
using static WebConventerTemperature.Helper.Helper;

namespace WebConventerTemperature.Tests
{

    [TestClass]
    public class HomeControllerTest
    {
        HomeController _homeController;

        Mock<IValidationServices> _mockValidationServ;
        Mock<IWebHostEnvironment> _mockWebHostEnv;
        Mock<IRepository> _repository;

        // Act
//        var result = await controller.Index(newSession);

//        // Assert
//        var badRequestResult = result as BadRequestObjectResult;
//        Assert.IsNotNull(badRequestResult, "Expected BadRequestObjectResult");
//Assert.IsInstanceOfType(badRequestResult.Value, typeof(SerializableError));

        [TestMethod]
        public void IndexReturnsAViewResult()
        {
            // Arrange
            var mock = new Mock<IRepository>();
            var mock1 = new Mock<IWebHostEnvironment>();
            var mock2 = new Mock<IValidationServices>();
            ConventerTemperature conv = new ConventerTemperature();

            
            var controller = new HomeController(/*mock.Object, */mock2.Object,mock1.Object);

            // Act
            var result = controller.Index(conv);

            // Assert
            var badRequestResult = result as ViewResult;
            Assert.IsNotNull(badRequestResult, "BadRequestObjectResult");
            Assert.IsNotInstanceOfType(badRequestResult, typeof(SerializableError));
            //var model = Assert.IsAssignableFrom<IEnumerable<User>>(viewResult.Model);
            //Assert.Equal(GetTestUsers().Count, model.Count());
        }

        //[TestInitialize]
        //public void Initialize()
        //{
        //    _mockValidationServ = new Mock<IValidationServices>();
        //    _mockWebHostEnv = new Mock<IWebHostEnvironment>();
        //    _repository = new Mock<IRepository>();

        //    _homeController = new HomeController(_repository.Object,_mockValidationServ.Object, _mockWebHostEnv.Object);
        //}
        //[TestMethod]
        //public void HomeController_Index_ValidationServices()
        //{
        //    _mockValidationServ.Setup(x => x.AbsolutabsoluteMinimum(It.IsAny<int>()));
        //}

        //[TestMethod]
        //public void HomeController_Index_ValidationServices1()
        //{
        //    _repository.Setup(repo => repo.Index(It.IsAny<ConventerTemperature>())).Returns(ViewResult);
        //}

        //public void IndexReturnsAViewResultWithAListOfUsers()
        //{
        //    // Arrange
        //    var mock = new Mock<IRepository>();
        //    mock.Setup(repo => repo.Index(It.IsAny<ConventerTemperature>())).Returns(GetTestUsers());
        //    var controller = new HomeController(mock.Object);

        //    // Act
        //    var result = controller.Index();

        //    // Assert
        //    var viewResult = Assert.IsType<ViewResult>(result);
        //    var model = Assert.IsAssignableFrom<IEnumerable<User>>(viewResult.Model);
        //    Assert.Equal(GetTestUsers().Count, model.Count());
        //}

        //private IActionResult ViewResult()
        //{
        //  IActionResult act = null;
        //  return act;
        //}
    }
}
