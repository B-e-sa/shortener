using FluentValidation;

namespace Shortener.Application.Users.Commands.CreateNewPassword;

public class CreateNewPasswordCommandValidator : AbstractValidator<CreateNewPasswordCommand>
{
    public CreateNewPasswordCommandValidator()
    {
        RuleFor(v => v.NewPassword)
            .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$");
    }
}
