using System.Net;

namespace Shortener.Controllers.ResponseHandlers.ErrorHandlers
{
    public class BadRequestHandler(
        string message = "Invalid user request",
        HttpStatusCode statusCode = HttpStatusCode.BadRequest
        ) : ResponseHandler<int?>(message, statusCode, null)
    {
    }
}