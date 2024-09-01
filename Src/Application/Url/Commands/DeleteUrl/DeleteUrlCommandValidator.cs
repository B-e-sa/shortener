using FluentValidation;
using Shortener.Src.Application.Common;

namespace Shortener.Src.Application.Url.Commands.DeleteUrl
{
    public class DeleteUrlCommandValidator : AbstractValidator<DeleteUrlCommand>
    {
        public DeleteUrlCommandValidator()
        {
            RuleFor(v => v.Id).SetValidator(new IdValidator());
        }
    }
}