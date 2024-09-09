using FluentValidation;

namespace Shortener.Application.Users.Commands.CreateUser;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(v => v.Email)
            .EmailAddress();

        RuleFor(v => v.Username)
            .MaximumLength(24)
            .MinimumLength(5);

        RuleFor(v => v.Password)
            .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$");
    }
}
