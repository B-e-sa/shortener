using Shortener.Application.Urls.Commands.CreateUrl;
using Shortener.Tests.Application.E2ETests.Users;
using System.Net.Http.Headers;

namespace Shortener.Tests.Application.E2ETests.Urls.Commands;

public class CreateUrlTests(FunctionalTestWebAppFactory factory) : BaseFunctionalTest(factory)
{
    private readonly UrlHelper helper = new();
    private readonly UserHelper userHelper = new();

    [Fact]
    public async Task Should_ReturnCreated_WhenUserIsNotAuthenticated()
    {
        // Arrange
        var url = helper.GenerateValidUrl();

        // Act
        var createdRes = await HttpClient.PostAsJsonAsync(helper.GetApiUrl(), url);

        // Assert
        createdRes.StatusCode.Should().Be(HttpStatusCode.Created);
    }

    [Fact]
    public async Task Should_ReturnCreated_WhenUserIsAuthenticated()
    {
        // Arrange
        var url = helper.GenerateValidUrl();

        var user = userHelper.GenerateValidUser();
        var createdUserResponse = await HttpClient
            .PostAsJsonAsync(userHelper.GetApiUrl(), user);

        var tokenAsString = await createdUserResponse.Content.ReadAsStringAsync();

        var req = new HttpRequestMessage(HttpMethod.Post, helper.GetApiUrl())
        {
            Content = JsonContent.Create(url)
        };
        req.Headers.Authorization = new AuthenticationHeaderValue("Bearer", tokenAsString);

        // Act
        var createdRes = await HttpClient.SendAsync(req);

        // Assert
        createdRes.StatusCode.Should().Be(HttpStatusCode.Created);
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