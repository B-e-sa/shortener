using Shortener.Domain.Common.Exceptions.Base;

namespace Shortener.Domain.Common.Exceptions.Users;

public class EmailVerificationNotFoundException()
    : NotFoundException("Verification code was not found.");
