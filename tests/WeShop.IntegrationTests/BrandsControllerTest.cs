using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using WeShop.Domain.Commands.Catalog.ProductBrandCommands;
using WeShop.Infrasture.Common;
using Xunit;

namespace WeShop.IntegrationTests
{
    public class BrandsControllerTest : IClassFixture<TestServerFixture>
    {
        private readonly HttpClient _client;
        private readonly TestServerFixture _fixture;

        public BrandsControllerTest(TestServerFixture fixture)
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
    }
}