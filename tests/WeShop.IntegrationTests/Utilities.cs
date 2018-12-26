using System.Collections.Generic;
using WeShop.Domain.Entities;
using WeShop.Infrasture.Data;

namespace WeShop.IntegrationTests
{
    public static class Utilities
    {
        public static void InitializeDbForTests(WeShopDbContext db)
        {
            var brands = GetSeedingCatalogBrands();
            var catalogTypes = GetSeedingCatalogTypes();
            var products = GetSeedingCatalogItems(brands, catalogTypes);
            db.ProductBrands.AddRange(brands);
            db.ProductTypes.AddRange(catalogTypes);
            db.Products.AddRange(products);
            db.SaveChanges();
        }

        public static List<ProductBrand> GetSeedingCatalogBrands()
        {
            return new List<ProductBrand>()
            {
                new ProductBrand("特力屋"),
                new ProductBrand("宜家"),
                new ProductBrand("爱丽丝"),
                new ProductBrand("开普特")
            };
        }

        public static List<ProductType> GetSeedingCatalogTypes()
        {
            return new List<ProductType>()
            {
                new ProductType("水杯"),
                new ProductType("牙刷"),
                new ProductType("毛巾"),
                new ProductType("拖把")
            };
        }

        public static List<Product> GetSeedingCatalogItems(List<ProductBrand> brands,
            List<ProductType> catalogTypes)
        {
            return new List<Product>()
            {
                new Product(
                    "测试水杯1",
                    "测试水杯1",
                    1,
                    "https://www.baidu.com",
                    "测试图片",
                    brands[0].Id,
                    catalogTypes[0].Id,
                    false
                ),
                new Product(
                    "测试水杯2",
                    "测试水杯2",
                    2,
                    "https://www.baidu.com",
                    "测试图片",
                    brands[0].Id,
                    catalogTypes[0].Id,
                    false
                ),
                new Product(
                    "测试水杯3",
                    "测试水杯3",
                    1,
                    "https://www.baidu.com",
                    "测试图片",
                    brands[1].Id,
                    catalogTypes[1].Id,
                    false
                ),
                new Product(
                    "测试水杯1",
                    "测试水杯1",
                    1,
                    "https://www.baidu.com",
                    "测试图片",
                    brands[0].Id,
                    catalogTypes[0].Id,
                    false
                ),
                new Product(
                    "测试水杯1",
                    "测试水杯1",
                    1,
                    "https://www.baidu.com",
                    "测试图片",
                    brands[0].Id,
                    catalogTypes[0].Id,
                    false
                ),
                new Product(
                    "测试水杯1",
                    "测试水杯1",
                    1,
                    "https://www.baidu.com",
                    "测试图片",
                    brands[0].Id,
                    catalogTypes[0].Id,
                    false
                ),
                new Product(
                    "测试水杯1",
                    "测试水杯1",
                    1,
                    "https://www.baidu.com",
                    "测试图片",
                    brands[0].Id,
                    catalogTypes[0].Id,
                    false
                ),
                new Product(
                    "测试水杯1",
                    "测试水杯1",
                    1,
                    "https://www.baidu.com",
                    "测试图片",
                    brands[0].Id,
                    catalogTypes[0].Id,
                    false
                ),
                new Product(
                    "测试水杯1",
                    "测试水杯1",
                    1,
                    "https://www.baidu.com",
                    "测试图片",
                    brands[0].Id,
                    catalogTypes[0].Id,
                    false
                ),
                new Product(
                    "测试水杯1",
                    "测试水杯1",
                    1,
                    "https://www.baidu.com",
                    "测试图片",
                    brands[0].Id,
                    catalogTypes[0].Id,
                    false
                ),
                new Product(
                    "测试水杯1",
                    "测试水杯1",
                    1,
                    "https://www.baidu.com",
                    "测试图片",
                    brands[0].Id,
                    catalogTypes[0].Id,
                    false
                ),
                new Product(
                    "测试水杯1",
                    "测试水杯1",
                    1,
                    "https://www.baidu.com",
                    "测试图片",
                    brands[0].Id,
                    catalogTypes[0].Id,
                    false
                )
            };
        }
    }
}