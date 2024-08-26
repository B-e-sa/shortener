using System.Net;
using FluentAssertions;
using Shortener.Tests.Abstractions;
using Shortener.Web.Controllers.Url;
using Xunit;

namespace Shortener.Tests.Urls
{
    public class DeleteUrlTests(FunctionalTestWebAppFactory factory) : BaseFunctionalTest(factory)
    {
        private readonly TestHelper helper = new();

        [Fact]
        public async Task Should_ReturnOk_WhenUrlExists()
        {
            // Arrange
            var url = new UrlCreateRequest("New Url", "https://www.google.com");
            var createdRes = await HttpClient.PostAsJsonAsync("http://localhost:5229", url);
            var createdbody = await helper.DeserializeResponse<UrlCreateResponse>(createdRes);
            var createdUrl = createdbody.Data;

            // Act
            var deletedRes = await HttpClient.DeleteAsync($"http://localhost:5229/{createdUrl?.Id}");

            // Assert
            deletedRes.StatusCode.Should().Be(HttpStatusCode.OK);

            var foundRes = await HttpClient.GetAsync($"http://localhost:5229/{createdUrl?.ShortUrl}");
            foundRes.StatusCode.Should().BeOneOf(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Should_ReturnNotFound_WhenUrlDoesNotExists()
        {
            // Arrange
            var urlId = "125";

            // Act
            var deletedRes = await HttpClient.DeleteAsync($"http://localhost:5229/{urlId}");

            // Assert
            deletedRes.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Should_ReturnBadRequest_WhenUrlIdIsInvalid()
        {
            // Arrange
            var urlId = "1abc";

            // Act
            var deleteRes = await HttpClient.DeleteAsync($"http://localhost:5229/{urlId}");

            // Assert
            deleteRes.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}