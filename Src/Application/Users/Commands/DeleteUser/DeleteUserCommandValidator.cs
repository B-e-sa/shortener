using FluentValidation;
using Shortener.Application.Common;

namespace Shortener.Application.Users.Commands.DeleteUser;

public class DeleteUserByIdQueryValidator : AbstractValidator<DeleteUserCommand>
{
    public DeleteUserByIdQueryValidator()
    {
        RuleFor(v => v.Id).SetValidator(new IdValidator());
    }
}