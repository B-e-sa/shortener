using Shortener.Tests.Application.FunctionalTests.Users;
using System.Net.Http.Headers;

namespace Shortener.Tests.Application.FunctionalTests.EmailVerifications.Commands;

public class CreateEmailVerificationTest(FunctionalTestWebAppFactory factory)
    : BaseFunctionalTest(factory)
{
    private readonly UserHelper userHelper = new();
    private readonly EmailVerificationHelper verificationHelper = new();

    private async Task<string> CreateUser()
    {
        var user = userHelper.GenerateValidUser();
        var createdUserResponse = await HttpClient.PostAsJsonAsync(userHelper.GetApiUrl(), user);
        return await createdUserResponse.Content.ReadAsStringAsync();
    }

    [Fact]
    public async Task Should_CreateEmailVerification_WhenRequestIsValid()
    {
        // Arrange
        var token = await CreateUser();
        var authorization = new AuthenticationHeaderValue("Bearer", token);

        // Act
        var createVerificationReq = new HttpRequestMessage(HttpMethod.Post, verificationHelper.GetApiUrl());
        createVerificationReq.Headers.Authorization = authorization;

        var createdVerificationRes = await HttpClient.SendAsync(createVerificationReq);

        // Assert
        createdVerificationRes.StatusCode.Should().Be(HttpStatusCode.Created);
    }
}