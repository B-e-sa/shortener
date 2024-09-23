using Shortener.Domain.Common.Exceptions.Base;

namespace Shortener.Domain.Common.Exceptions.EmailVerifications;

public class EmailVerificationNotFoundException() 
    : NotFoundException("Verification code was not found.");
