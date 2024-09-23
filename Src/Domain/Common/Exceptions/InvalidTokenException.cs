namespace Shortener.Domain.Common.Exceptions;

public class InvalidTokenException() 
    : UnauthorizedAccessException("Invalid or expired user token.");