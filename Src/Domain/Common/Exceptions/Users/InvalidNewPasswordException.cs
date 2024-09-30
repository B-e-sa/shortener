using Shortener.Domain.Common.Exceptions.Base;

namespace Shortener.Domain.Common.Exceptions.Users;

public class InvalidNewPasswordException()
    : BadRequestException("New password cannot be your old password.");