using Shortener.Domain.Common.Exceptions.Base;

namespace Shortener.Domain.Common.Exceptions;

public class EntityNotFoundException : NotFoundException
{ 
    public EntityNotFoundException()
        : base("Entity was not found.")
    {
    }

    public EntityNotFoundException(string message)
        : base(string.IsNullOrEmpty(message) ? "Entity was not found." : message)
    {
    }
}
