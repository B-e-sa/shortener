using Shortener.Domain.Common.Exceptions.Base;

namespace Shortener.Domain.Common.Exceptions.Users;

public class DuplicatedUserCredentialsException(string field)
    : ConflictException($"{field} already taken.");