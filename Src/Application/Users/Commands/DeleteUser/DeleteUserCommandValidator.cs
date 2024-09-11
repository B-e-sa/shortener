using FluentValidation;
using Shortener.Application.Common;

namespace Shortener.Application.Users.Commands.DeleteUser;

public class DeleteUserByidQueryValidator : AbstractValidator<DeleteUserCommand>
{
    public DeleteUserByidQueryValidator()
    {
        RuleFor(v => v.Id).SetValidator(new IdValidator());
    }
}