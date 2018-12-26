using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using WeShop.Domain.Commands.Catalog.ProductBrandCommands;
using WeShop.Domain.Commands.Catalog.ProductCommands;
using WeShop.Domain.Commands.Catalog.ProductTypeCommands;
using WeShop.Infrasture.Common;
using WeShop.Queries.Dtos;
using Xunit;

namespace WeShop.IntegrationTests
{
    public class ProductControllerTest : IClassFixture<TestServerFixture>
    {
        private readonly HttpClient _client;
        private readonly TestServerFixture _fixture;

        public ProductControllerTest(TestServerFixture fixture)
        {
            _fixture = fixture;
            _client = fixture.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });
        }

        [Fact]
        public async Task Insert_A_Brand()
        {
            var testName = "雕牌" + DateTime.Now;
            var response = await _client.PostAsJsonAsync($"api/product/brands", new CreateBrandCommand(testName));
            Assert.True(response.IsSuccessStatusCode);

            var content = await response.Content.ReadAsAsync<Result<string>>();

            Assert.True(content.Success);
            Assert.NotNull(content.Value);
            Assert.NotEmpty(content.Value);
            Console.WriteLine($"Insert BrandId: {content.Value}");
        }

        [Fact]
        public async Task Insert_A_ProductType()
        {
            var testName = "毛巾" + DateTime.Now;
            var response = await _client.PostAsJsonAsync($"api/product/categories", new CreateTypeCommand(testName));
            Assert.True(response.IsSuccessStatusCode);

            var content = await response.Content.ReadAsAsync<Result<string>>();

            Assert.True(content.Success);
            Assert.NotNull(content.Value);
            Assert.NotEmpty(content.Value);
            Console.WriteLine($"Insert TypeId: {content.Value}");
        }

        [Fact]
        public async Task Insert_A_Product()
        {
            var brands = await GetBrandDtosAsync();
            var brandId = Convert.ToInt64(brands.First().BrandId);
            var types = await GetCategoryDtosAsync();
            var typeId = Convert.ToInt64(types.First().CategoryId);
            var testProductName = "肥皂" + DateTime.Now;
            var testProductDesc = "这就是一块肥皂";
            var price = 1.1m;
            var picName = "helloworld.png";
            var picUri = "https://www.baidu.com";

            var createProductCommand = new CreateProductCommand(testProductName, testProductDesc, price, picName,
                picUri, brandId, typeId, true);
            var response = await _client.PostAsJsonAsync($"api/product", createProductCommand);
            Assert.True(response.IsSuccessStatusCode);

            var content = await response.Content.ReadAsAsync<Result<string>>();

            Assert.True(content.Success);
            Assert.NotNull(content.Value);
            Assert.NotEmpty(content.Value);
            Console.WriteLine($"Insert ProductId: {content.Value}");
        }


        private async Task<List<BrandDto>> GetBrandDtosAsync()
        {
            var response = await _client.GetAsync("/api/product/brands?pageIndex=1&&pageSize=10");
            return await response.Content.ReadAsAsync<List<BrandDto>>();
        }

        private async Task<List<CategoryDto>> GetCategoryDtosAsync()
        {
            var response = await _client.GetAsync("/api/product/categories?pageIndex=1&&pageSize=10");
            return await response.Content.ReadAsAsync<List<CategoryDto>>();
        }

        private async Task<List<ProductDto>> GetProductDtosAsync()
        {
            var response = await _client.GetAsync("/api/product?pageIndex=1&&pageSize=10");
            return await response.Content.ReadAsAsync<List<ProductDto>>();
        }
    }
}