namespace Shortener.Domain.Common.Exceptions.Users;

public class InvalidUserPasswordException() 
    : UnauthorizedAccessException("User password does not match.");