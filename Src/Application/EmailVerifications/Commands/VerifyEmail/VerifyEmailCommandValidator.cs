using FluentValidation;

namespace Shortener.Application.EmailVerifications.Commands.VerifyEmail;

public class VerifyEmailCommandValidator : AbstractValidator<VerifyEmailCommand>
{
    public VerifyEmailCommandValidator()
    {
        RuleFor(v => v.Code).Length(6);
    }
}