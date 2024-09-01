namespace Shortener.Src.Domain.Common.Exceptions
{
    public abstract class BadRequestException(string message) : System.Exception(message)
    {
    }
}