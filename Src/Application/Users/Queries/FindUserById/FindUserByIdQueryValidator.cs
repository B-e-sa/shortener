using FluentValidation;
using Shortener.Application.Common;

namespace Shortener.Application.Users.Queries.FindUserById;

public class FindUserByidQueryValidator : AbstractValidator<FindUserByIdQuery>
{
    public FindUserByidQueryValidator()
    {
        RuleFor(v => v.Id).SetValidator(new IdValidator());
    }
}
