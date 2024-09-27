namespace Shortener.Tests.Application.E2ETests.Urls.Commands;

public class VisitUrlTests(FunctionalTestWebAppFactory factory) : BaseFunctionalTest(factory)
{
    private readonly UrlHelper helper = new();

    [Fact]
    public async Task Should_ReturnOk_WhenUrlExists()
    {
        // Arrange
        var url = helper.GenerateValidUrl();

        var createdRes = await HttpClient.PostAsJsonAsync(helper.GetApiUrl(), url);
        var createdBody = await helper.DeserializeResponse<Url>(createdRes);
        var shortUrl = $"{helper.GetApiUrl()}/{createdBody?.ShortUrl}";

        // Act
        var foundRes = await HttpClient.GetAsync(shortUrl);

        // Assert
        foundRes.StatusCode.Should().BeOneOf(HttpStatusCode.OK);
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