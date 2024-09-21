using Shortener.Domain.Common.Exceptions.Base;

namespace Shortener.Domain.Common.Exceptions.Urls
{
    public class UrlNotFoundException() 
        : NotFoundException("Url was not found.")
    {
    }
}
