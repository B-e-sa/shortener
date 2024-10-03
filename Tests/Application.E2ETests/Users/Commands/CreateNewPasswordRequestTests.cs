namespace Shortener.Tests.Application.E2ETests.Users.Commands;

public class CreateNewPasswordRequestTests(FunctionalTestWebAppFactory factory) 
    : BaseFunctionalTest(factory)
{
    private readonly UserHelper helper = new();

    [Fact]
    public async Task Should_ReturnCreated_WhenUserExists()
    {
        // Arrange
        var user = helper.GenerateValidUser();
        await HttpClient.PostAsJsonAsync(helper.GetApiUrl(), user);

        // Act
        var createdRequest = await HttpClient.PostAsJsonAsync(
            $"{helper.GetApiUrl()}/forgot-password", 
            new { user.Email });

        // Assert
        createdRequest.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task Should_ReturnNotFound_WhenUserDoesNotExists()
    {
        // Arrange
        var user = helper.GenerateValidUser();

        // Act
        var createdRequest = await HttpClient.PostAsJsonAsync(
            $"{helper.GetApiUrl()}/forgot-password",
            new { user.Email });

        // Assert
        createdRequest.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task Should_ReturnBadRequest_WhenEmailIsInvalid()
    {
        // Arrange
        var user = helper.GenerateValidUser();
        await HttpClient.PostAsJsonAsync(helper.GetApiUrl(), user);

        var invalidEmail = "invalid@";

        // Act
        var createdRequest = await HttpClient.PostAsJsonAsync(
            $"{helper.GetApiUrl()}/forgot-password",
            new { Email = invalidEmail });

        // Assert
        createdRequest.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
}
