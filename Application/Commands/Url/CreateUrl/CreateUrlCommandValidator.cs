using FluentValidation;

namespace Shortener.Application.Commands.Url.CreateUrl
{

    public class CreateTodoItemCommandValidator : AbstractValidator<CreateUrlCommand>
    {
        public CreateTodoItemCommandValidator()
        {
            RuleFor(v => v.Title)
                .MaximumLength(40)
                .MinimumLength(5);

            RuleFor(v => v.Url)
                .Matches(@"https?:\/\/(www\.)?[-a-zA-Z0-9@:%._\+~#=]{1,256}\.[a-zA-Z0-9()]{1,6}\b([-a-zA-Z0-9()@:%_\+.~#?&\/\/=]*)");
        }
    }
}