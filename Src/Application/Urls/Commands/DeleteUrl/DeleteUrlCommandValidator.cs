using FluentValidation;
using Shortener.Application.Common;

namespace Shortener.Application.Urls.Commands.DeleteUrl;

public class DeleteUrlCommandValidator : AbstractValidator<DeleteUrlCommand>
{
    public DeleteUrlCommandValidator()
    {
        RuleFor(v => v.Id).SetValidator(new IdValidator());
    }
}