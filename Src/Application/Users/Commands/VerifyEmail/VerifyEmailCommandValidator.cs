using FluentValidation;

namespace Shortener.Application.Users.Commands.VerifyEmail;

public class VerifyEmailCommandValidator : AbstractValidator<VerifyEmailCommand>
{
    public VerifyEmailCommandValidator()
    {
        RuleFor(v => v.Code).Length(6);
    }
}