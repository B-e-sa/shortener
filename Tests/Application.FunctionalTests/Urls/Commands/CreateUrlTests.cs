using Newtonsoft.Json;
using Shortener.Application.Urls.Commands.CreateUrl;
using Shortener.Tests.Application.FunctionalTests.Users;
using System;
using System.Net.Http.Headers;
using System.Text;

namespace Shortener.Tests.Application.FunctionalTests.Urls.Commands;

public class CreateUrlTests(FunctionalTestWebAppFactory factory) : BaseFunctionalTest(factory)
{
    private readonly UrlHelper helper = new();
    private readonly UserHelper userHelper = new();

    [Fact]
    public async Task Should_ReturnCreatedWithoutUserId_WhenUserIsNotAuthenticated()
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

        var foundBody = await helper.DeserializeResponse<Url>(createdRes);
        foundBody.UserId.Should().BeNull();
    }

    [Fact]
    public async Task Should_ReturnCreatedWithUserId_WhenUserIsAuthenticated()
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
        var createdBody = await helper.DeserializeResponse<Url>(createdRes);
        createdRes.StatusCode.Should().Be(HttpStatusCode.Created);

        var foundRes = await HttpClient.GetAsync($"{helper.GetApiUrl()}/{createdBody.ShortUrl}");
        foundRes.StatusCode.Should().Be(HttpStatusCode.OK);

        var foundBody = await helper.DeserializeResponse<Url>(createdRes);
        foundBody.UserId.Should().NotBeNull();
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