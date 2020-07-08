using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using WebStore_GB.Controllers;
using Assert = Xunit.Assert;

namespace WebStore_GB.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTests
    {
        [TestMethod]
        public void Index_Returns_View()
        {
            // Arrange
            var controller = new HomeController();

            // Act
            var result = controller.Index();

            // Assert
            Assert.IsType<ViewResult>(result);
        }

        [TestMethod]
        public void Blog_Returns_View()
        {
            // Arrange
            var controller = new HomeController();

            // Act
            var result = controller.Blog();

            // Assert
            Assert.IsType<ViewResult>(result);
        }

        [TestMethod]
        public void BlogSingle_Returns_View()
        {
            // Arrange
            var controller = new HomeController();

            // Act
            var result = controller.BlogSingle();

            // Assert
            Assert.IsType<ViewResult>(result);
        }

        [TestMethod]
        public void ContactUs_Returns_View()
        {
            // Arrange
            var controller = new HomeController();

            // Act
            var result = controller.ContactUs();

            // Assert
            Assert.IsType<ViewResult>(result);
        }

        [TestMethod]
        public void Error404_Returns_View()
        {
            // Arrange
            var controller = new HomeController();

            // Act
            var result = controller.Error404();

            // Assert
            Assert.IsType<ViewResult>(result);
        }

        [TestMethod]
        public void Throw_throw_Exception()
        {
            // Arrange
            var controller = new HomeController();
            Exception exception = null;

            // Act
            try
            {
                controller.Throw("Error");
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            // Assert
            Assert.IsType<ApplicationException>(exception);
        }

        [TestMethod, ExpectedException(typeof(ApplicationException))]
        public void Throw_throw_ApplicationException_via_Attribute()
        {
            // Arrange
            var controller = new HomeController();

            // Act
            // Assert
            controller.Throw("Error");
        }

        [TestMethod]
        public void Throw_throw_ApplicationException_via_Assert()
        {
            // Arrange
            var controller = new HomeController();
            const string expectedMessage = "Error";

            // Act
            // Assert
            var exception = Assert.Throws<ApplicationException>(() => controller.Throw(expectedMessage));
            Assert.Equal(expectedMessage, exception.Message);
        }

        [TestMethod]
        public void ErrorStatus_404_RedirectTo_Error404()
        {
            // Arrange
            var controller = new HomeController();
            const string status404 = "404";

            // Act
            var result = controller.ErrorStatus(status404);

            // Assert
            //Assert.NotNull(result);
            var redirectToAction = Assert.IsType<RedirectToActionResult>(result);
            Assert.Null(redirectToAction.ControllerName);
            Assert.Equal(nameof(HomeController.Error404), redirectToAction.ActionName);
        }
    }
}
