using Shortener.Application.Users.Commands.CreateUser;

namespace Shortener.Tests.Application.FunctionalTests.Users.Commands;

public class CreateUserTests(FunctionalTestWebAppFactory factory) : BaseFunctionalTest(factory)
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
        createdRes.Content.Should().BeOfType<StreamContent>();
    }

    [Fact]
    public async Task Should_ReturnBadRequest_WhenEmailIsInvalid()
    {
        // Arrange
        var user = helper.GenerateValidUser();
        CreateUserCommand invalidUser = new()
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
        CreateUserCommand invalidUser = new()
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
        CreateUserCommand invalidUser = new()
        {
            Email = user.Email,
            Password = "!password1",
            Username = user.Username
        };

        // Act
        var res = await HttpClient.PostAsJsonAsync(helper.GetApiUrl(), invalidUser);

        // Assert
        res.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
}