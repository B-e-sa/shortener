using Shortener.Application.Users.Commands.CreateUser;
using Bogus.Extensions;


namespace Shortener.Tests.Application.FunctionalTests.Users
{
    public class UserHelper() : TestHelper("user")
    {
        public CreateUserCommand GenerateValidUser()
        {
            return new CreateUserCommand()
            {
                Email = faker.Internet.Email(),
                Username = faker.Random.String(5, 24),
                Password = faker.Internet.Password()
            };
        }
    }
}
