using System.Net;
using FluentAssertions;
using Shortener.Controllers.Url;
using Shortener.Tests.Abstractions;
using Xunit;

namespace Shortener.Tests.Urls
{
    public class CreateUrlTests(FunctionalTestWebAppFactory factory) : BaseFunctionalTest(factory)
    {
        private readonly TestHelper helper = new();

        [Fact]
        public async Task Should_ReturnCreated_WhenRequestIsValid()
        {
            // Arrange
            var url = new UrlCreateRequest("New Url", "https://www.google.com");

            // Act
            var createdRes = await HttpClient.PostAsJsonAsync("http://localhost:5229", url);

            // Assert
            var createdBody = await helper.DeserializeResponse<UrlCreateResponse>(createdRes);
            createdRes.StatusCode.Should().Be(HttpStatusCode.Created);

            var foundRes = await HttpClient.GetAsync($"http://localhost:5229/{createdBody.Data?.ShortUrl}");
            var expectedValues = new[]
            {
                HttpStatusCode.Redirect,
                HttpStatusCode.OK
            };
            foundRes.StatusCode.Should().BeOneOf(expectedValues);
        }

        [Fact]
        public async Task Should_ReturnBadRequest_WhenTitleIsInvalid()
        {
            // Arrange
            var url = new UrlCreateRequest("", "https://www.google.com");

            // Act
            var res = await HttpClient.PostAsJsonAsync("http://localhost:5229", url);

            // Assert
            res.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Should_ReturnBadRequest_WhenUrlIsInvalid()
        {
            // Arrange
            var url = new UrlCreateRequest("New Title", "www.google.com");

            // Act
            var res = await HttpClient.PostAsJsonAsync("http://localhost:5229", url);

            // Assert
            res.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}