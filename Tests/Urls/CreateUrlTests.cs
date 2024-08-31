using System.Net;
using FluentAssertions;
using Shortener.Application.Url.Commands.CreateUrl;
using Shortener.Tests.Abstractions;
using Xunit;

namespace Shortener.Tests.Urls
{
    public class CreateUrlTests(FunctionalTestWebAppFactory factory) : BaseFunctionalTest(factory)
    {
        private readonly TestHelper helper = new("url");

        [Fact]
        public async Task Should_ReturnCreated_WhenRequestIsValid()
        {
            // Arrange
            var url = new CreateUrlCommand()
            {
                Title = "New Url",
                Url = "https://www.google.com"
            };

            // Act
            var createdRes = await HttpClient.PostAsJsonAsync(helper.GetApiUrl(), url);

            // Assert
            var createdBody = await helper.DeserializeResponse<Domain.Entities.Url>(createdRes);
            createdRes.StatusCode.Should().Be(HttpStatusCode.Created);

            var foundRes = await HttpClient.GetAsync($"{helper.GetApiUrl()}/{createdBody.ShortUrl}");
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
            var url = new CreateUrlCommand
            {
                Title = "",
                Url = "https://www.google.com"
            };

            // Act
            var res = await HttpClient.PostAsJsonAsync(helper.GetApiUrl(), url);

            // Assert
            res.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Should_ReturnBadRequest_WhenUrlIsInvalid()
        {
            // Arrange
            var url = new CreateUrlCommand
            {
                Title = "New Title",
                Url = "www.google.com"
            };

            // Act
            var res = await HttpClient.PostAsJsonAsync(helper.GetApiUrl(), url);

            // Assert
            res.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}