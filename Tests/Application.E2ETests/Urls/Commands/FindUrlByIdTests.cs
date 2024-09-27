using Shortener.Tests.Application.E2ETests.Urls;

namespace Shortener.Tests.Application.E2ETests.Urls.Commands;

public class FindUrlByIdTests(FunctionalTestWebAppFactory factory) : BaseFunctionalTest(factory)
{
    private readonly UrlHelper helper = new();

    [Fact]
    public async Task Should_ReturnOk_WhenUrlExists()
    {
        // Arrange
        var url = helper.GenerateValidUrl();

        var createdRes = await HttpClient.PostAsJsonAsync(helper.GetApiUrl(), url);
        var createdBody = await helper.DeserializeResponse<Url>(createdRes);

        // Act
        var foundRes = await HttpClient.GetAsync($"{helper.GetApiUrl()}/find/{createdBody?.Id}");

        // Assert
        var foundBody = await helper.DeserializeResponse<Url>(foundRes);
        foundRes.StatusCode.Should().Be(HttpStatusCode.OK);
        foundBody.Title.Should().Be(url.Title);
    }

    [Fact]
    public async Task Should_ReturnNotFound_WhenUrlDoesNotExists()
    {
        // Arrange
        var id = "10";

        // Act
        var visitRes = await HttpClient.GetAsync($"{helper.GetApiUrl()}/find/{id}");

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
        var firstVisitRes = await HttpClient.GetAsync($"{helper.GetApiUrl()}/find/{id1}");
        var secondVisitRes = await HttpClient.GetAsync($"{helper.GetApiUrl()}/find/{id2}");

        // Assert
        firstVisitRes.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        secondVisitRes.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
}