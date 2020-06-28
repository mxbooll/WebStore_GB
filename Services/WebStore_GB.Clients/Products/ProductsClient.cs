using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Net.Http;
using WebStore_GB.Clients.Base;
using WebStore_GB.Domain;
using WebStore_GB.Domain.DTO.Products;
using WebStore_GB.Domain.Entities;
using WebStore_GB.Interfaces.Services;

namespace WebStore_GB.Clients.Products
{
    public class ProductsClient : BaseClient, IProductData
    {
        public ProductsClient(IConfiguration configuration) : base(configuration, WebApi.PRODUCTS) { }

        public IEnumerable<Section> GetSections() => Get<IEnumerable<Section>>($"{_serviceAddress}/sections");

        public IEnumerable<Brand> GetBrands() => Get<IEnumerable<Brand>>($"{_serviceAddress}/brands");

        public IEnumerable<ProductDTO> GetProducts(ProductFilter filter = null) =>
            Post(_serviceAddress, filter ?? new ProductFilter())
                .Content
                .ReadAsAsync<IEnumerable<ProductDTO>>()
                .Result;

        public ProductDTO GetProductById(int id) => Get<ProductDTO>($"{_serviceAddress}/{id}");
    }
}
