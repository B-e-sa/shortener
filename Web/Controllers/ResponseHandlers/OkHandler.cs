using System.Net;

namespace Shortener.Controllers.ResponseHandlers
{
    public class OkHandler<T>(
        T data,
        string message = "Success",
        HttpStatusCode statusCode = HttpStatusCode.OK
        ) : ResponseHandler<T>(message, statusCode, data)
    {
    }
}