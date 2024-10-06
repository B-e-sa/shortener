using FluentValidation;

namespace Shortener.Application.Users.Commands.Register;

public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator()
    {
        RuleFor(v => v.Email.Trim()).EmailAddress();

        RuleFor(v => v.Username.Trim())
            .MaximumLength(24)
            .MinimumLength(5);

        RuleFor(v => v.Password.Trim()).Length(8);
    }
}
