using System.Net;
using FluentAssertions;
using Shortener.Application.Url.Commands.CreateUrl;
using Shortener.Tests.Abstractions;
using Xunit;

namespace Shortener.Tests.Urls
{
    public class VisitUrlTests(FunctionalTestWebAppFactory factory) : BaseFunctionalTest(factory)
    {
        private readonly TestHelper helper = new("url");

        [Fact]
        public async Task Should_RedirectAndIncreaseVisitCount_WhenUrlExists()
        {
            // Arrange
            var url = new CreateUrlCommand()
            {
                Title = "New Url",
                Url = "https://www.google.com"
            };

            var createdRes = await HttpClient.PostAsJsonAsync(helper.GetApiUrl(), url);
            var createdBody = await helper.DeserializeResponse<Domain.Entities.Url?>(createdRes);
            var shortUrl = $"{helper.GetApiUrl()}/{createdBody?.ShortUrl}";

            // Act
            var foundRes = await HttpClient.GetAsync(shortUrl);
            var foundBody = await helper.DeserializeResponse<Domain.Entities.Url?>(foundRes);

            // Assert
            var expectedValues = new[]
            {
                HttpStatusCode.Redirect,
                HttpStatusCode.OK
            };
            foundRes.StatusCode.Should().BeOneOf(expectedValues);
            foundBody?.Visits.Should().Be(1);
        }

        [Fact]
        public async Task Should_ReturnNotFound_WhenUrlDoesNotExists()
        {
            // Arrange
            var shortUrl = "abcd";

            // Act
            var visitRes = await HttpClient.GetAsync($"{helper.GetApiUrl()}/{shortUrl}");

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
            var firstVisitRes = await HttpClient.GetAsync($"{helper.GetApiUrl()}/{id1}");
            var secondVisitRes = await HttpClient.GetAsync($"{helper.GetApiUrl()}/{id2}");

            // Assert
            firstVisitRes.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            secondVisitRes.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}