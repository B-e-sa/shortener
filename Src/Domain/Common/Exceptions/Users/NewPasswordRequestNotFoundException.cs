using Shortener.Domain.Common.Exceptions.Base;

namespace Shortener.Domain.Common.Exceptions.Users;

public class NewPasswordRequestNotFoundException()
    : NotFoundException("Password reset code was not found.");
