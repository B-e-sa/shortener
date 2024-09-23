using FluentValidation;
using Shortener.Application.Common;

namespace Shortener.Application.Urls.Queries.FindUrlById;

public class FindUrlByIdQueryValidator : AbstractValidator<FindUrlByIdQuery>
{
    public FindUrlByIdQueryValidator()
    {
        RuleFor(v => v.Id).SetValidator(new IdValidator());
    }
}
