using Shortener.Domain.Common.Exceptions.Base;

namespace Shortener.Domain.Common.Exceptions.Users;

public class UserNotFoundException() : NotFoundException("User was not found.");