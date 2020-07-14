using Bizportal.WebApi;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Bizportal.Api.NUnit
{
    public class BizportalUnitTests
    {
        private readonly string _url;
        private readonly TestServer _server;
        private readonly HttpClient _client;

        private Uri GetUrl(string baseEntity)
        {
            return new Uri($"{_url}{baseEntity}");
        }
        private HttpContent CreateContent<T>(T entity)
        {
            return new StringContent(JsonSerializer.Serialize(entity), Encoding.UTF8, "application/json");
        }

        private async Task<HttpResponseMessage> SendPostRequestAndGetResponseAsync<T>(T entity)
        {
            var content = CreateContent(entity);

            var uri = GetUrl(entity.GetType().Name);

            var response = await _client.PostAsync(uri, content);

            return response;
        }

        private async Task<T> DeserializeResponse<T>(HttpResponseMessage response) where T : class
        {
            using var result = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);

            var options = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            var baseEntity = await JsonSerializer.DeserializeAsync<T>(result, options);

            return baseEntity;
        }

        public BizportalUnitTests()
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();

            _url = configuration["WebApiUrl"];

            var webHostBuilder = new WebHostBuilder().UseStartup<Startup>();
            webHostBuilder.UseConfiguration(configuration);

            _server = new TestServer(webHostBuilder);
            _client = _server.CreateClient();
        }

        private async Task<Category> CreateCategory()
        {
            var category = new Category() { Name = "Category1" };

            var response = await SendPostRequestAndGetResponseAsync(category);

            if (response.IsSuccessStatusCode)
            {
                var categoryResult = await DeserializeResponse<Category>(response);

                return categoryResult;
            }
            throw new Exception($"Invalid response result with status code {response.StatusCode}");
        }
        [Test]
        public async Task CreateCategoryTest()
        {
            var categoryResult = await CreateCategory();

            Assert.IsNotNull(categoryResult);
            Assert.IsNotNull(categoryResult.Id);
        }

        [Test]
        public async Task CreateProductTest()
        {
            var category = await CreateCategory();
            var product = new Product() { Name = "Product1", Price = 10, CategoryId = category.Id };

            var response = await SendPostRequestAndGetResponseAsync(product);

            if (response.IsSuccessStatusCode)
            {
                var productResult = await DeserializeResponse<Product>(response);

                Assert.IsNotNull(productResult);
                Assert.IsNotNull(productResult.Id);
            }
        }
        [Test]
        public async Task GetAllCategory()
        {
            var uri = GetUrl(nameof(Category));

            var response = await _client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var collection = await DeserializeResponse<ICollection<Category>>(response);
                foreach (var item in collection)
                {
                    Assert.AreEqual(item.GetType().Name, nameof(Category));
                }
            }
        }
    }
}