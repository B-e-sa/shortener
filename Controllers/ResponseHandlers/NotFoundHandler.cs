using System.Net;

namespace Shortener.Controllers.ResponseHandlers
{
    public class NotFoundHandler(
        string title = "Not found",
        string message = "The searched resource was not found",
        HttpStatusCode statusCode = HttpStatusCode.NotFound
        ) : ResponseHandler<int?>(title, message, statusCode, null)
    {
    }
}