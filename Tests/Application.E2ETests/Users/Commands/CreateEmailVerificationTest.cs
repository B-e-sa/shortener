using System.Net.Http.Headers;

namespace Shortener.Tests.Application.E2ETests.Users.Commands;

public class CreateEmailVerificationTest(FunctionalTestWebAppFactory factory)
    : BaseFunctionalTest(factory)
{
    private readonly UserHelper helper = new();

    private async Task<string> CreateUser()
    {
        var user = helper.GenerateValidUser();
        var createdUserResponse = await HttpClient.PostAsJsonAsync(helper.GetApiUrl(), user);
        return await createdUserResponse.Content.ReadAsStringAsync();
    }

    [Fact]
    public async Task Should_ReturnCreated_WhenUserIsAuthorized()
    {
        // Arrange
        var token = await CreateUser();
        var authorization = new AuthenticationHeaderValue("Bearer", token);

        // Act
        var createVerificationReq = new HttpRequestMessage(HttpMethod.Post, $"{helper.GetApiUrl()}/verification");
        createVerificationReq.Headers.Authorization = authorization;

        var createdVerificationRes = await HttpClient.SendAsync(createVerificationReq);

        // Assert
        createdVerificationRes.StatusCode.Should().Be(HttpStatusCode.Created);
    }
}