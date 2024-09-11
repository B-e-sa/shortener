using Shortener.Application.Urls.Commands.CreateUrl;

namespace Shortener.Tests.Application.FunctionalTests.Urls.Commands;

public class CreateUrlTests(FunctionalTestWebAppFactory factory) : BaseFunctionalTest(factory)
{
    private readonly UrlHelper helper = new();

    [Fact]
    public async Task Should_ReturnCreated_WhenRequestIsValid()
    {
        // Arrange
        var url = helper.GenerateValidUrl();

        // Act
        var createdRes = await HttpClient.PostAsJsonAsync(helper.GetApiUrl(), url);

        // Assert
        var createdBody = await helper.DeserializeResponse<Url>(createdRes);
        createdRes.StatusCode.Should().Be(HttpStatusCode.Created);

        var foundRes = await HttpClient.GetAsync($"{helper.GetApiUrl()}/{createdBody.ShortUrl}");
        foundRes.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task Should_ReturnBadRequest_WhenTitleIsInvalid()
    {
        // Arrange
        var url = helper.GenerateValidUrl();
        CreateUrlCommand invalidUrl = new()
        {
            Title = "",
            Url = url.Url
        };

        // Act
        var res = await HttpClient.PostAsJsonAsync(helper.GetApiUrl(), invalidUrl);

        // Assert
        res.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task Should_ReturnBadRequest_WhenUrlIsInvalid()
    {
        // Arrange
        var url = helper.GenerateValidUrl();
        CreateUrlCommand invalidUrl = new()
        {
            Title = url.Title,
            Url = "invalid-url.com"
        };

        // Act
        var res = await HttpClient.PostAsJsonAsync(helper.GetApiUrl(), invalidUrl);

        // Assert
        res.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
}