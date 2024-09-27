using Shortener.Application.Users.Commands.CreateUser;

namespace Shortener.Tests.Application.E2ETests.Users
{
    public class UserHelper() : TestHelper("user")
    {
        public CreateUserCommand GenerateValidUser()
        {
            return new CreateUserCommand()
            {
                Email = faker.Internet.Email(),
                Username = faker.Random.String(5, 24),
                Password = "ValidPassw0rd!"
            };
        }
    }
}
