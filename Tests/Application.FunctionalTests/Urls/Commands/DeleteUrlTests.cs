namespace Shortener.Tests.Application.FunctionalTests.Urls.Commands;

public class DeleteUrlTests(FunctionalTestWebAppFactory factory) : BaseFunctionalTest(factory)
{
    private readonly UrlHelper helper = new();

    [Fact]
    public async Task Should_ReturnOk_WhenUrlExists()
    {
        // Arrange
        var url = helper.GenerateValidUrl();

        var createdRes = await HttpClient.PostAsJsonAsync(helper.GetApiUrl(), url);
        var createdbody = await helper.DeserializeResponse<Url>(createdRes);

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