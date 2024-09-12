namespace Shortener.Domain.Common.Exceptions.Users;

public class UserNotFoundException()
    : EntityNotFoundException("User was not found.")
{
}
