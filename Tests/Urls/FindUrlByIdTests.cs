using System.Net;
using FluentAssertions;
using Shortener.Controllers.Url;
using Shortener.Tests.Abstractions;
using Xunit;

namespace Shortener.Tests.Urls
{
    public class FindUrlByIdTests(FunctionalTestWebAppFactory factory) : BaseFunctionalTest(factory)
    {
        private readonly TestHelper helper = new();

        [Fact]
        public async Task Should_ReturnOk_WhenUrlExists()
        {
            // Arrange
            var url = new UrlCreateRequest("New Url", "https://www.google.com");
            var createdRes = await HttpClient.PostAsJsonAsync("http://localhost:5229", url);
            var createdBody = await helper.DeserializeResponse<UrlCreateResponse>(createdRes);

            // Act
            var foundRes = await HttpClient.GetAsync($"http://localhost:5229/find/{createdBody.Data?.Id}");

            // Assert
            var foundBody = await helper.DeserializeResponse<UrlCreateResponse>(foundRes);
            foundRes.StatusCode.Should().Be(HttpStatusCode.OK);
            foundBody.Data?.Title.Should().Be("New Url");
        }

        [Fact]
        public async Task Should_ReturnNotFound_WhenUrlDoesNotExists()
        {
            // Arrange
            var id = "10";

            // Act
            var visitRes = await HttpClient.GetAsync($"http://localhost:5229/find/{id}");

            // Assert
            visitRes.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Should_ReturnBadRequest_WhenIdIsInvalid()
        {
            // Arrange
            var id1 = "abc";
            var id2 = "abcde";

            // Act
            var firstVisitRes = await HttpClient.GetAsync($"http://localhost:5229/find/{id1}");
            var secondVisitRes = await HttpClient.GetAsync($"http://localhost:5229/find/{id2}");

            // Assert
            firstVisitRes.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            secondVisitRes.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}