using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;
using WebStore_GB.Domain.DTO.Products;
using WebStore_GB.Domain.Entities;
using WebStore_GB.Domain.Models;
using WebStore_GB.Domain.ViewModels;
using WebStore_GB.Interfaces.Services;
using WebStore_GB.Services.Products;
using Assert = Xunit.Assert;

namespace WebStore_GB.Services.Tests.Products
{
    [TestClass()]
    public class CartServiceTests
    {
        private Cart _cart;

        private Mock<IProductData> _productDataMock;
        private Mock<ICartStore> _cartStoreMock;

        private ICartService _cartService;

        [TestInitialize]
        public void TestInitialize()
        {
            _cart = new Cart
            {
                Items = new List<CartItem>
                {
                    new CartItem { ProductId = 1, Quantity = 1 },
                    new CartItem { ProductId = 2, Quantity = 3 }
                }
            };
            _productDataMock = new Mock<IProductData>();
            _productDataMock
               .Setup(c => c.GetProducts(It.IsAny<ProductFilter>()))
               .Returns(new List<ProductDTO>
                {
                    new ProductDTO
                    {
                        Id = 1,
                        Name = "Product 1",
                        Price = 1.1m,
                        Order = 0,
                        ImageUrl = "Product1.png",
                        Brand = new BrandDTO { Id = 1, Name = "Brand 1" },
                        Section = new SectionDTO { Id = 1, Name = "Section 1"}
                    },
                    new ProductDTO
                    {
                        Id = 2,
                        Name = "Product 2",
                        Price = 2.2m,
                        Order = 0,
                        ImageUrl = "Product2.png",
                        Brand = new BrandDTO { Id = 2, Name = "Brand 2" },
                        Section = new SectionDTO { Id = 2, Name = "Section 2"}
                    },
                });
            _cartStoreMock = new Mock<ICartStore>();
            _cartStoreMock.Setup(c => c.Cart).Returns(_cart);
            _cartService = new CartService(_productDataMock.Object, _cartStoreMock.Object);
        }

        [TestMethod()]
        public void Cart_Class_ItemsCount_returns_Correct_Quantity()
        {
            var cart = new Cart
            {
                Items = new List<CartItem>
                {
                    new CartItem { ProductId = 1, Quantity = 1 },
                    new CartItem { ProductId = 2, Quantity = 3 },
                }
            };
            const int expected_count = 4;

            var actual_count = cart.ItemsCount;

            Assert.Equal(expected_count, actual_count);
        }

        [TestMethod()]
        public void CartViewModel_Returns_Correct_ItemsCount()
        {
            var cart_view_model = new CartViewModel
            {
                Items = new[]
                {
                    ( new ProductViewModel {Id = 1, Name = "Product 1", Price = 0.5m}, 1 ),
                    ( new ProductViewModel {Id = 2, Name = "Product 2", Price = 1.5m}, 3 ),
                }
            };
            const int expected_count = 4;

            var actual_count = cart_view_model.ItemsCount;

            Assert.Equal(expected_count, actual_count);
        }

        [TestMethod()]
        public void CartService_AddToCart_WorkCorrect()
        {
            _cart.Items.Clear();

            const int EXPECTED_ID = 5;

            _cartService.AddToCart(EXPECTED_ID);

            Assert.Equal(1, _cart.ItemsCount);

            Assert.Single(_cart.Items);
            Assert.Equal(EXPECTED_ID, _cart.Items[0].ProductId);
        }

        [TestMethod()]
        public void CartService_RemoveFromCart_Remove_Correct_Item()
        {
            const int ITEM_ID = 1;

            _cartService.RemoveFromCart(ITEM_ID);

            Assert.Single(_cart.Items);
            Assert.Equal(2, _cart.Items[0].ProductId);
        }

        [TestMethod()]
        public void CartService_RemoveAll_ClearCart()
        {
            _cartService.RemoveAll();

            Assert.Empty(_cart.Items);
        }

        [TestMethod()]
        public void CartService_Decrement_Correct()
        {
            const int ITEM_ID = 2;

            _cartService.DecrementFromCart(ITEM_ID);

            Assert.Equal(3, _cart.ItemsCount);
            Assert.Equal(2, _cart.Items.Count);
            Assert.Equal(ITEM_ID, _cart.Items[1].ProductId);
            Assert.Equal(2, _cart.Items[1].Quantity);
        }

        [TestMethod()]
        public void CartService_Remove_Item_When_Decrement_to_0()
        {
            const int ITEM_ID = 1;

            _cartService.DecrementFromCart(ITEM_ID);

            Assert.Equal(3, _cart.ItemsCount);
            Assert.Single(_cart.Items);
        }

        [TestMethod()]
        public void CartService_TransformFromCart_WorkCorrect()
        {
            var result = _cartService.TransformFromCart();

            Assert.Equal(4, result.ItemsCount);
            Assert.Equal(1.1m, result.Items.First().Product.Price);
        }
    }
}
