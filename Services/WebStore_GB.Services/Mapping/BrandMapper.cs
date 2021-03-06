﻿using WebStore_GB.Domain.DTO.Products;
using WebStore_GB.Domain.Entities;

namespace WebStore_GB.Services.Mapping
{
    public static class BrandMapper
    {
        public static BrandDTO ToDTO(this Brand Brand) => Brand is null
            ? null
            : new BrandDTO
            {
                Id = Brand.Id,
                Name = Brand.Name,
            };

        public static Brand FromDTO(this BrandDTO Brand) => Brand is null
            ? null
            : new Brand
            {
                Id = Brand.Id,
                Name = Brand.Name,
            };
    }
}
