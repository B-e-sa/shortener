using FluentValidation;
using Shortener.Application.Common;

namespace Shortener.Application.Users.Queries.FindUserById;

public class FindUserByIdQueryValidator : AbstractValidator<FindUserByIdQuery>
{
    public FindUserByIdQueryValidator()
    {
        RuleFor(v => v.Id).SetValidator(new IdValidator());
    }
}
