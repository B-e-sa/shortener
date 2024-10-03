using FluentValidation;

namespace Shortener.Application.Users.Commands.Register;

public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator()
    {
        RuleFor(v => v.Email).EmailAddress();

        RuleFor(v => v.Username)
            .MaximumLength(24)
            .MinimumLength(5);

        RuleFor(v => v.Password).Length(8);
    }
}
