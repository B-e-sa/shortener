using System.Net;
using FluentAssertions;
using Shortener.Application.Url.Commands.CreateUrl;
using Shortener.Tests.Abstractions;
using Xunit;

namespace Shortener.Tests.Urls
{
    public class DeleteUrlTests(FunctionalTestWebAppFactory factory) : BaseFunctionalTest(factory)
    {
        private readonly TestHelper helper = new("url");

        [Fact]
        public async Task Should_ReturnOk_WhenUrlExists()
        {
            // Arrange
            var url = new CreateUrlCommand()
            {
                Title = "New Url",
                Url = "https://www.google.com"
            };

            var createdRes = await HttpClient.PostAsJsonAsync(helper.GetApiUrl(), url);
            var createdbody = await helper.DeserializeResponse<Domain.Entities.Url>(createdRes);

            // Act
            var deletedRes = await HttpClient.DeleteAsync($"{helper.GetApiUrl()}/{createdbody?.Id}");

            // Assert
            deletedRes.StatusCode.Should().Be(HttpStatusCode.OK);

            var foundRes = await HttpClient.GetAsync($"{helper.GetApiUrl()}/{createdbody?.ShortUrl}");
            foundRes.StatusCode.Should().BeOneOf(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Should_ReturnNotFound_WhenUrlDoesNotExists()
        {
            // Arrange
            var urlId = "125";

            // Act
            var deletedRes = await HttpClient.DeleteAsync($"{helper.GetApiUrl()}/{urlId}");

            // Assert
            deletedRes.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task Should_ReturnBadRequest_WhenUrlIdIsInvalid()
        {
            // Arrange
            var urlId = "1abc";

            // Act
            var deleteRes = await HttpClient.DeleteAsync($"{helper.GetApiUrl()}/{urlId}");

            // Assert
            deleteRes.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}