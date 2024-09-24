using Shortener.Application.Users.Commands.CreateUser;
using Shortener.Application.Users.Commands.Login;

namespace Shortener.Tests.Application.FunctionalTests.Users.Commands;

public class LoginTests(FunctionalTestWebAppFactory factory) : BaseFunctionalTest(factory)
{
    private readonly UserHelper helper = new();

    [Fact]
    public async Task Should_ReturnToken_WhenLoginIsValid()
    {
        // Arrange
        var user = helper.GenerateValidUser();
        await HttpClient.PostAsJsonAsync(helper.GetApiUrl(), user);

        var login = new LoginCommand()
        {
            Email = user.Email,
            Password = user.Password
        };

        // Act
        var loginRes = await HttpClient.PostAsJsonAsync($"{helper.GetApiUrl()}/login", login);

        // Assert
        loginRes.StatusCode.Should().Be(HttpStatusCode.OK);
        loginRes.Content.Should().BeOfType<StreamContent>();
    }

    [Fact]
    public async Task Should_ReturnNotFound_WhenUserEmailDoesNotExists()
    {
        // Arrange
        var user = helper.GenerateValidUser();
        var login = new LoginCommand()
        {
            Email = user.Email,
            Password = user.Password
        };

        // Act
        var res = await HttpClient.PostAsJsonAsync($"{helper.GetApiUrl()}/login", login);

        // Assert
        res.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task Should_ReturnUnauthorized_WhenUserPasswordDoesNotMatch()
    {
        // Arrange
        var user = helper.GenerateValidUser();
        await HttpClient.PostAsJsonAsync(helper.GetApiUrl(), user);

        var login = new LoginCommand()
        {
            Email = user.Email,
            Password = "invalidPassw0rd!"
        };

        // Act
        var res = await HttpClient.PostAsJsonAsync($"{helper.GetApiUrl()}/login", login);

        // Assert
        res.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }
}