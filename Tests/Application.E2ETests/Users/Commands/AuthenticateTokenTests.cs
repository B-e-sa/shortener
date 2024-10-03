using System.Net.Http.Headers;

namespace Shortener.Tests.Application.E2ETests.Users.Commands;

public class AuthenticateTokenTests(FunctionalTestWebAppFactory factory) : BaseFunctionalTest(factory)
{
    private readonly UserHelper helper = new();

    [Fact]
    public async Task Should_ReturnAuthenticatedUser_WhenTokenIsValid()
    {
        // Arrange
        var user = helper.GenerateValidUser();
        var createdUserResponse = await HttpClient.PostAsJsonAsync(helper.GetApiUrl(), user);
        var token = await createdUserResponse.Content.ReadAsStringAsync();

        // Act
        var req = new HttpRequestMessage(HttpMethod.Post, $"{helper.GetApiUrl()}/auth");
        req.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var res = await HttpClient.SendAsync(req);

        // Assert
        res.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}