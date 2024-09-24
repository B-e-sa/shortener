using Shortener.Application.Common.Models;
using Shortener.Tests.Application.FunctionalTests.Users;
using System.Net.Http.Headers;

namespace Shortener.Tests.Application.FunctionalTests.EmailVerifications.Commands;

public class VerifyEmailTests(FunctionalTestWebAppFactory factory) 
    : BaseFunctionalTest(factory)
{
    private readonly UserHelper userHelper = new();
    private readonly EmailVerificationHelper verificationHelper = new ();

    private async Task<string> CreateUser()
    {
        var user = userHelper.GenerateValidUser();
        var createdUserResponse = await HttpClient.PostAsJsonAsync(userHelper.GetApiUrl(), user);
        return await createdUserResponse.Content.ReadAsStringAsync();
    }

    private async Task<EmailVerification> CreateEmailVerification(string token)
    {
        var authorization = new AuthenticationHeaderValue("Bearer", token);

        var createVerificationReq = new HttpRequestMessage(HttpMethod.Post, verificationHelper.GetApiUrl());
        createVerificationReq.Headers.Authorization = authorization;

        var createVerificationRes = await HttpClient.SendAsync(createVerificationReq);

        return await verificationHelper
            .DeserializeResponse<EmailVerification>(createVerificationRes);
    }

    [Fact]
    public async Task Should_VerifyUserEmail_WhenUserExists()
    {
        // Arrange
        var token = await CreateUser();
        var authorization = new AuthenticationHeaderValue("Bearer", token);

        var emailVerification = await CreateEmailVerification(token);

        // Act
        var verifyReq = new HttpRequestMessage(HttpMethod.Post, $"{verificationHelper.GetApiUrl()}/verify")
        {
            Content = JsonContent.Create(new { emailVerification.Code }),
        };
        verifyReq.Headers.Authorization = authorization;
        
        var verifyRes = await HttpClient.SendAsync(verifyReq);

        // Assert
        verifyRes.StatusCode.Should().Be(HttpStatusCode.OK);

        var authenticationReq = new HttpRequestMessage(HttpMethod.Post, $"{userHelper.GetApiUrl()}/auth");
        authenticationReq.Headers.Authorization = authorization;

        var authRes = await HttpClient.SendAsync(authenticationReq);

        verifyRes.StatusCode.Should().Be(HttpStatusCode.OK);
        
        var authBody = await verificationHelper.DeserializeResponse<UserDTO>(authRes);
        authBody.ConfirmedEmail.Should().BeTrue();
    }

    [Fact]
    public async Task Should_ReturnNotFound_WhenUserDoesNotExists()
    {
        // Arrange
        var token = await CreateUser();

        var deleteReq = new HttpRequestMessage(HttpMethod.Delete, userHelper.GetApiUrl());
        deleteReq.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        await HttpClient.SendAsync(deleteReq);

        // Act
        var req = new HttpRequestMessage(HttpMethod.Post, verificationHelper.GetApiUrl());
        req.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var createdEmailVerificationRes = await HttpClient.SendAsync(req);

        // Assert
        createdEmailVerificationRes.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task Should_ReturnBadRequest_WhenVerificationCodeIsInvalid()
    {
        // Arrange
        var token = await CreateUser();
        var authorization = new AuthenticationHeaderValue("Bearer", token);
        var invalidCode = 12345;

        // Act
        var verifyReq = new HttpRequestMessage(HttpMethod.Post, $"{verificationHelper.GetApiUrl()}/verify")
        {
            Content = JsonContent.Create(new { Code = invalidCode }),
        };
        verifyReq.Headers.Authorization = authorization;

        var verifyRes = await HttpClient.SendAsync(verifyReq);

        // Assert
        verifyRes.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
} 