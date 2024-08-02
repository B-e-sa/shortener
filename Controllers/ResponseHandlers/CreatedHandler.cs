using System.Net;

namespace Shortener.Controllers.ResponseHandlers
{
    public class CreatedHandler<T>(
        T data,
        string title = "Created",
        string message = "Entity created with success",
        HttpStatusCode statusCode = HttpStatusCode.Created
        ) : ResponseHandler<T>(title, message, statusCode, data)
    {
    }
}