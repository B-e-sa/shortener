using Shortener.Application.Users.Commands.Register;

namespace Shortener.Tests.Application.E2ETests.Users.Commands;

public class RegisterTests(FunctionalTestWebAppFactory factory) : BaseFunctionalTest(factory)
{
    private readonly UserHelper helper = new();

    [Fact]
    public async Task Should_ReturnCreated_WhenRequestIsValid()
    {
        // Arrange
        var user = helper.GenerateValidUser();

        // Act
        var createdRes = await HttpClient.PostAsJsonAsync(helper.GetApiUrl(), user);

        // Assert
        createdRes.StatusCode.Should().Be(HttpStatusCode.Created);
    }

    [Fact]
    public async Task Should_ReturnBadRequest_WhenEmailIsInvalid()
    {
        // Arrange
        var user = helper.GenerateValidUser();
        RegisterCommand invalidUser = new()
        {
            Email = "invalidemail.com",
            Password = user.Password,
            Username = user.Username
        };

        // Act
        var res = await HttpClient.PostAsJsonAsync(helper.GetApiUrl(), invalidUser);

        // Assert
        res.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task Should_ReturnBadRequest_WhenUsernameIsInvalid()
    {
        // Arrange
        var user = helper.GenerateValidUser();
        RegisterCommand invalidUser = new()
        {
            Email = user.Email,
            Password = user.Password,
            Username = "name"
        };

        // Act
        var res = await HttpClient.PostAsJsonAsync(helper.GetApiUrl(), invalidUser);

        // Assert
        res.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task Should_ReturnBadRequest_WhenPasswordIsInvalid()
    {
        // Arrange
        var user = helper.GenerateValidUser();
        RegisterCommand invalidUser = new()
        {
            Email = user.Email,
            Password = "word",
            Username = user.Username
        };

        // Act
        var res = await HttpClient.PostAsJsonAsync(helper.GetApiUrl(), invalidUser);

        // Assert
        res.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
}