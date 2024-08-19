using System.Net;

namespace Shortener.Controllers.ResponseHandlers
{
    public class NotFoundHandler(
        string message = "The searched resource was not found",
        HttpStatusCode statusCode = HttpStatusCode.NotFound
        ) : ResponseHandler<int?>(message, statusCode, null)
    {
    }
}