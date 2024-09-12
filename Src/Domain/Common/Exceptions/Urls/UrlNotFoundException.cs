namespace Shortener.Domain.Common.Exceptions.Urls
{
    public class UrlNotFoundException() 
        : EntityNotFoundException("Url was not found.")
    {
    }
}
