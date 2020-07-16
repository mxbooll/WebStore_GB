using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Linq;
using WebStore_GB.Domain.DTO.Products;
using WebStore_GB.Domain.Entities;
using WebStore_GB.Domain.ViewModels;
using WebStore_GB.Interfaces.Services;
using Assert = Xunit.Assert;

namespace WebStore_GB.Controllers.Tests
{
    [TestClass()]
    public class CatalogControllerTests
    {
        // A - A - A == Arrange - Act - Assert == Подготовка данных и объекта тестирования - действие над объектом - проверка утверждений
        [TestMethod()]
        public void Details_Returns_With_Correct_View()
        {
            // Arrange
            const int EXPECTED_PRODUCT_ID = 1;
            const decimal EXPECTED_PRICE = 10m;

            var expectedName = $"Product id {EXPECTED_PRODUCT_ID}";
            var expectedBrandName = $"Brand of product {EXPECTED_PRODUCT_ID}";

            var fakeProductData = new Mock<IProductData>();
            fakeProductData
                .Setup(p => p.GetProductById(It.IsAny<int>()))
                .Returns<int>(id => new ProductDTO
                {
                    Id = id,
                    Name = $"Product id {id}",
                    ImageUrl = $"Image_id_{id}.png",
                    Order = 1,
                    Price = EXPECTED_PRICE,
                    Brand = new BrandDTO
                    {
                        Id = 1,
                        Name = $"Brand of product {id}"
                    },
                    Section = new SectionDTO
                    {
                        Id = 1,
                        Name = $"Section of product {id}"
                    }
                });

            var controller = new CatalogController(fakeProductData.Object);

            // Act
            var result = controller.Details(EXPECTED_PRODUCT_ID);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<ProductViewModel>(viewResult.Model);

            Assert.Equal(EXPECTED_PRODUCT_ID, model.Id);
            Assert.Equal(expectedName, model.Name);
            Assert.Equal(EXPECTED_PRICE, model.Price);
            Assert.Equal(expectedBrandName, model.Brand);
        }

        [TestMethod()]
        public void Shop_Returns_Correct_View()
        {
            // Arrange
            var products = new[]
            {
                new ProductDTO
                {
                    Id = 1,
                    Name = "Product 1",
                    Order = 0,
                    Price = 10m,
                    ImageUrl = "Product1.png",
                    Brand = new BrandDTO
                    {
                        Id = 1,
                        Name = "Brand of product 1"
                    },
                    Section = new SectionDTO
                    {
                        Id = 1,
                        Name = "Section of product 1"
                    }
                },
                new ProductDTO
                {
                    Id = 2,
                    Name = "Product 2",
                    Order = 0,
                    Price = 20m,
                    ImageUrl = "Product2.png",
                    Brand = new BrandDTO
                    {
                        Id = 2,
                        Name = "Brand of product 2"
                    },
                    Section = new SectionDTO
                    {
                        Id = 2,
                        Name = "Section of product 2"
                    }
                },
            };
            var fakeProductData = new Mock<IProductData>();

            fakeProductData
                .Setup(p => p.GetProducts(It.IsAny<ProductFilter>()))
                .Returns(products);

            var controller = new CatalogController(fakeProductData.Object);

            const int EXPECTED_SECTION_ID = 1;
            const int EXPECTED_BRAND_ID = 5;

            var fakeMapper = new Mock<IMapper>();
            fakeMapper.Setup(mapper => mapper.Map<ProductViewModel>(It.IsAny<Product>()))
               .Returns<Product>(p => new ProductViewModel
               {
                   Id = p.Id,
                   Name = p.Name,
                   Order = p.Order,
                   Price = p.Price,
                   Brand = p.Brand?.Name,
                   ImageUrl = p.ImageUrl
               });

            // Act
            var result = controller.Shop(EXPECTED_SECTION_ID, EXPECTED_BRAND_ID, fakeMapper.Object);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<CatalogViewModel>(viewResult.Model);

            Assert.Equal(products.Length, model.Products.Count());
            Assert.Equal(EXPECTED_BRAND_ID, model.BrandId);
            Assert.Equal(EXPECTED_SECTION_ID, model.SectionId);

            Assert.Equal(products[0].Brand.Name, model.Products.First().Brand);
        }
    }
}