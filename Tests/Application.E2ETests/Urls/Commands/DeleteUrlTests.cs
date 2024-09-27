using Shortener.Tests.Application.E2ETests.Users;
using System.Net.Http.Headers;

namespace Shortener.Tests.Application.E2ETests.Urls.Commands;

public class DeleteUrlTests(FunctionalTestWebAppFactory factory) : BaseFunctionalTest(factory)
{
    private readonly UrlHelper urlHelper = new();
    private readonly UserHelper userHelper = new();

    private async Task<string> CreateUser()
    {
        var user = userHelper.GenerateValidUser();
        var createdUserResponse = await HttpClient.PostAsJsonAsync(userHelper.GetApiUrl(), user);
        return await createdUserResponse.Content.ReadAsStringAsync();
    }

    private async Task<Url> CreateUrl(string? token = null)
    {
        var url = urlHelper.GenerateValidUrl();

        var req = new HttpRequestMessage(HttpMethod.Post, urlHelper.GetApiUrl())
        {
            Content = JsonContent.Create(url)
        };

        if (token != null)
        {
            req.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        var res = await HttpClient.SendAsync(req);

        return await urlHelper.DeserializeResponse<Url>(res);
    }

    private async Task<HttpResponseMessage> DeleteUrl(string id, string token)
    {
        var req = new HttpRequestMessage(HttpMethod.Delete, $"{urlHelper.GetApiUrl()}/{id}");
        req.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        return await HttpClient.SendAsync(req);
    }

    [Fact]
    public async Task Should_ReturnOk_WhenUrlExists()
    {
        // Arrange
        var token = await CreateUser();
        var url = await CreateUrl(token);

        // Act
        var deletedRes = await DeleteUrl(url.Id.ToString(), token);

        // Assert
        deletedRes.StatusCode.Should().Be(HttpStatusCode.OK);

        var foundRes = await HttpClient.GetAsync($"{urlHelper.GetApiUrl()}/{url.ShortUrl}");
        foundRes.StatusCode.Should().BeOneOf(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task Should_ReturnNotFound_WhenUrlDoesNotExists()
    {
        // Arrange
        var token = await CreateUser();
        await CreateUrl(token);

        var inexistentId = 123;

        // Act
        var deletedRes = await DeleteUrl(inexistentId.ToString(), token);

        // Assert
        deletedRes.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task Should_ReturnBadRequest_WhenUrlIdIsInvalid()
    {
        // Arrange
        var token = await CreateUser();

        var invalidId = "1abc";

        // Act
        var deletedRes = await DeleteUrl(invalidId, token);

        // Assert
        deletedRes.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task Should_ReturnUnauthorized_WhenUserIsNotTheUrlOwner()
    {
        // Arrange
        var urlCreatorToken = await CreateUser();
        var url = await CreateUrl(urlCreatorToken);

        var invalidToken = await CreateUser();

        // Act
        var deletedRes = await DeleteUrl(url.Id.ToString(), invalidToken);

        // Assert
        deletedRes.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task Should_ReturnUnauthorized_WhenUrlWasAnonymouslyCreated()
    {
        // Arrange
        var url = await CreateUrl();
        var token = await CreateUser();

        // Act
        var deletedRes = await DeleteUrl(url.Id.ToString(), token);

        // Assert
        deletedRes.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }
}