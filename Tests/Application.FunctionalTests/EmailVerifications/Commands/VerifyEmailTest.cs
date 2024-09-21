using Shortener.Tests.Application.FunctionalTests.Users;
using System.Net.Http.Headers;

namespace Shortener.Tests.Application.FunctionalTests.EmailVerifications.Commands;

public class VerifyEmailTests(FunctionalTestWebAppFactory factory) : BaseFunctionalTest(factory)
{
    private readonly UserHelper userHelper = new();
    private readonly EmailVerificationHelper verificationHelper = new ();

    [Fact]
    public async Task Should_ReturnCreated_WhenRequestIsValid()
    {
        // Arrange
        var user = userHelper.GenerateValidUser();
        var userRes = await HttpClient.PostAsJsonAsync(userHelper.GetApiUrl(), user);
        var userToken = await userRes.Content.ReadAsStringAsync();

        // Act
        var req = new HttpRequestMessage(HttpMethod.Post, verificationHelper.GetApiUrl());
        req.Headers.Authorization = new AuthenticationHeaderValue("Bearer", userToken);

        var createdEmailVerificationRes = await HttpClient.SendAsync(req);

        // Assert
        createdEmailVerificationRes.StatusCode.Should().Be(HttpStatusCode.Created);
    }
} 