using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebStore_GB.Controllers;
using System;
using System.Collections.Generic;
using System.Text;
using Assert = Xunit.Assert;
using Moq;
using WebStore_GB.Interfaces.TestApi;
using Microsoft.AspNetCore.Mvc;

namespace WebStore_GB.Controllers.Tests
{
    [TestClass()]
    public class WebApiTestControllerTests
    {
        [TestMethod()]
        public void Index_Returns_View_with_Values()
        {
            // Arrange
            var expectedResult = new[] { "1", "2", "3" };

            var fakeValueService = new Mock<IValueService>();
            fakeValueService
                .Setup(service => service.Get())
                .Returns(expectedResult);

            var controller = new WebApiTestController(fakeValueService.Object);

            // Act
            var result = controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<string>>(viewResult.Model);

            Assert.Equal(expectedResult, model);

            fakeValueService.Verify(service => service.Get());
            fakeValueService.VerifyNoOtherCalls();
        }
    }
}