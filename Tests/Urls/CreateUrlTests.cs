using System.Net;
using FluentAssertions;
using Shortener.Tests.Abstractions;
using Xunit;

namespace Shortener.Tests.Urls
{
    public class CreateUrlTests(FunctionalTestWebAppFactory factory) : BaseFunctionalTest(factory)
    {
        [Fact]
        public async Task Should_ReturnCreated_WhenRequestIsValid()
        {
            // Arrange
            var title = "New Url";
            var url = "https://www.google.com";

            // Act
            var res = await HttpClient.PostAsJsonAsync("http://localhost:5229", new { title, url });

            // Assert
            res.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        [Fact]
        public async Task Should_ReturnBadRequest_WhenTitleIsInvalid()
        {
            // Arrange
            var url = "https://www.google.com";

            // Act
            var res = await HttpClient.PostAsJsonAsync("http://localhost:5229", new { url });

            // Assert
            res.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Should_ReturnBadRequest_WhenUrlIsInvalid()
        {
            // Arrange
            var title = "New Url";
            var url = "www.google.com";

            // Act
            var res = await HttpClient.PostAsJsonAsync("http://localhost:5229", new { title, url });

            // Assert
            res.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}