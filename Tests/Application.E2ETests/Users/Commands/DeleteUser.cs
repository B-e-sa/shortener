using System.Net.Http.Headers;

namespace Shortener.Tests.Application.E2ETests.Users.Commands;

public class DeleteUserTests(FunctionalTestWebAppFactory factory) 
    : BaseFunctionalTest(factory)
{
    private readonly UserHelper helper = new();

    private async Task<string> CreateUser()
    {
        var user = helper.GenerateValidUser();
        var createdUserResponse = await HttpClient.PostAsJsonAsync(helper.GetApiUrl(), user);
        return await createdUserResponse.Content.ReadAsStringAsync();
    }

    private async Task<HttpResponseMessage> DeleteUser(string token)
    {
        var req = new HttpRequestMessage(HttpMethod.Delete, helper.GetApiUrl());
        req.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        return await HttpClient.SendAsync(req);
    }

    [Fact]
    public async Task Should_ReturnOk_WhenUserExists()
    {
        // Arrange
        var token = await CreateUser();

        // Act
        var deletedRes = await DeleteUser(token);

        // Assert
        deletedRes.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task Should_ReturnUnauthorized_WhenUserDoesNotExists()
    {
        // Arrange
        var token = await CreateUser();
        await DeleteUser(token);

        // Act
        var deletedRes = await DeleteUser(token);

        // Assert
        deletedRes.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }
}