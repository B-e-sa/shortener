using System.Net;

namespace Shortener.Controllers.ResponseHandlers.ErrorHandlers
{
    public class BadRequestHandler(
        string title = "Bad Request",
        string message = "Invalid user request",
        HttpStatusCode statusCode = HttpStatusCode.BadRequest
        ) : ResponseHandler<int?>(title, message, statusCode, null)
    {
    }
}