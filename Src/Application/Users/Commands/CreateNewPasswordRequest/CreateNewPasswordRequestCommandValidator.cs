using FluentValidation;

namespace Shortener.Application.Users.Commands.CreateNewPasswordRequest;

public class CreateNewPasswordCommandRequestCommandValidator : AbstractValidator<CreateNewPasswordRequestCommand>
{
    public CreateNewPasswordCommandRequestCommandValidator()
    {
        RuleFor(v => v.Email).EmailAddress();
    }
}
