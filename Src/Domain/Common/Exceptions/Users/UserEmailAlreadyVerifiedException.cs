using Shortener.Domain.Common.Exceptions.Base;

namespace Shortener.Domain.Common.Exceptions.Users;

public class UserEmailAlreadyVerifiedException() 
    : ConflictException("User email is already verified");
