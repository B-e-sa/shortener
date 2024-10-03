using Shortener.Application.Users.Commands.Register;

namespace Shortener.Tests.Application.E2ETests.Users
{
    public class UserHelper() : TestHelper("user")
    {
        public RegisterCommand GenerateValidUser()
        {
            return new RegisterCommand()
            {
                Email = faker.Internet.Email(),
                Username = faker.Random.String(5, 24),
                Password = faker.Random.String(8)
            };
        }
    }
}
