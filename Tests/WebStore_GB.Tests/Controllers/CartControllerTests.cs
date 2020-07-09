using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebStore_GB.Domain.DTO.Order;
using WebStore_GB.Domain.Entities.Orders;
using WebStore_GB.Domain.ViewModels;
using WebStore_GB.Interfaces.Services;
using Assert = Xunit.Assert;

namespace WebStore_GB.Controllers.Tests
{
    [TestClass()]
    public class CartControllerTests
    {
        [TestMethod()]
        public async Task CheckOut_ModelState_Invalid_Returns_ViewModel()
        {
            // Arrange
            var fakeCartService = new Mock<ICartService>();
            var fakeOrderService = new Mock<IOrderService>();

            var controller = new CartController(fakeCartService.Object);
            controller.ModelState.AddModelError("error", "InvalidModel");

            const string EXPECTED_MODEL_NAME = "Test order";

            // Act
            var result = await controller.CheckOut(
                new OrderViewModel { Name = EXPECTED_MODEL_NAME },
                fakeOrderService.Object);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<CartOrderViewModel>(viewResult.Model);

            Assert.Equal(EXPECTED_MODEL_NAME, model.Order.Name);
        }

        [TestMethod()]
        public async Task CheckOut_Calls_Service_and_Return_Redirect()
        {
            // Arrange
            var fakeCartService = new Mock<ICartService>();

            fakeCartService
                .Setup(c => c.TransformFromCart())
                .Returns(() => new CartViewModel
                {
                    Items = new[] { (new ProductViewModel { Name = "Product" }, 1) }
                });

            const int EXPECTED_ORDER_ID = 1;
            
            var fakeOrderService = new Mock<IOrderService>();
            fakeOrderService
               .Setup(c => c.CreateOrder(It.IsAny<string>(), It.IsAny<CreateOrderModel>()))
               .ReturnsAsync(new OrderDTO
               {
                   Id = EXPECTED_ORDER_ID
               });

            var controller = new CartController(fakeCartService.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext
                    {
                        User = new ClaimsPrincipal(new ClaimsIdentity(new[] { new Claim(ClaimTypes.NameIdentifier, "TestUser") }))
                    }
                }
            };

            var orderModel = new OrderViewModel
            {
                Name = "Test order",
                Address = "Test address",
                Phone = "+1(234)567-89-00"
            };

            // Act
            var result = await controller.CheckOut(orderModel, fakeOrderService.Object);

            // Assert
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);

            Assert.Null(redirectResult.ControllerName);
            Assert.Equal(nameof(CartController.OrderConfirmed), redirectResult.ActionName);

            Assert.Equal(EXPECTED_ORDER_ID, redirectResult.RouteValues["id"]);
        }
    }
}