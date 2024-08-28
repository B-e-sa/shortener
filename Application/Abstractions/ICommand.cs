using MediatR;

namespace Shortener.Application.Abstractions
{
    public interface ICommand<out TResponse> : IRequest<TResponse>
    {
    }
}