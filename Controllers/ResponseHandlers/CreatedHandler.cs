using System.Net;

namespace Shortener.Controllers.ResponseHandlers
{
    public class CreatedHandler<T>(
        T data,
        string message = "Entity created with success",
        HttpStatusCode statusCode = HttpStatusCode.Created
        ) : ResponseHandler<T>(message, statusCode, data)
    {
    }
}