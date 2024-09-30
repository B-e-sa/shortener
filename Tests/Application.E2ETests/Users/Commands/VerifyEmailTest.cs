using System.Net.Http.Headers;

namespace Shortener.Tests.Application.E2ETests.Users.Commands;

public class VerifyEmailTests(FunctionalTestWebAppFactory factory)
    : BaseFunctionalTest(factory)
{
    static private readonly UserHelper helper = new();
    private readonly string verificationRoute = $"{helper.GetApiUrl()}/verify)";

    private async Task<string> CreateUser()
    {
        var user = helper.GenerateValidUser();
        var createdUserResponse = await HttpClient.PostAsJsonAsync(helper.GetApiUrl(), user);
        return await createdUserResponse.Content.ReadAsStringAsync();
    }

    private async Task<EmailVerification> CreateEmailVerification(string token)
    {
        var authorization = new AuthenticationHeaderValue("Bearer", token);

        var createVerificationReq = new HttpRequestMessage(HttpMethod.Post, verificationRoute);
        createVerificationReq.Headers.Authorization = authorization;

        var createVerificationRes = await HttpClient.SendAsync(createVerificationReq);

        return await helper
            .DeserializeResponse<EmailVerification>(createVerificationRes);
    }

    [Fact]
    public async Task Should_ReturnOk_WhenUserIsAuthorized()
    {
        // Arrange
        var token = await CreateUser();
        var authorization = new AuthenticationHeaderValue("Bearer", token);

        var emailVerification = await CreateEmailVerification(token);

        // Act
        var verifyReq = new HttpRequestMessage(HttpMethod.Post, verificationRoute)
        {
            Content = JsonContent.Create(new { emailVerification.Code }),
        };
        verifyReq.Headers.Authorization = authorization;

        var verifyRes = await HttpClient.SendAsync(verifyReq);

        // Assert
        verifyRes.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task Should_ReturnUnauthorized_WhenUserDoesNotExists()
    {
        // Arrange
        var token = await CreateUser();

        var deleteReq = new HttpRequestMessage(HttpMethod.Delete, helper.GetApiUrl());
        deleteReq.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        await HttpClient.SendAsync(deleteReq);

        // Act
        var req = new HttpRequestMessage(HttpMethod.Post, $"{helper.GetApiUrl()}/verification");
        req.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var createdEmailVerificationRes = await HttpClient.SendAsync(req);

        // Assert
        createdEmailVerificationRes.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task Should_ReturnBadRequest_WhenVerificationCodeIsInvalid()
    {
        // Arrange
        var token = await CreateUser();
        var authorization = new AuthenticationHeaderValue("Bearer", token);
        var invalidCode = 12345;

        // Act
        var verifyReq = new HttpRequestMessage(HttpMethod.Post, verificationRoute)
        {
            Content = JsonContent.Create(new { Code = invalidCode }),
        };
        verifyReq.Headers.Authorization = authorization;

        var verifyRes = await HttpClient.SendAsync(verifyReq);

        // Assert
        verifyRes.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
}