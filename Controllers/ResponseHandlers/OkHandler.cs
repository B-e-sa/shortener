using System.Net;

namespace Shortener.Controllers.ResponseHandlers
{
    public class OkHandler<T>(
        T data,
        string title = "Success",
        string message = "Success",
        HttpStatusCode statusCode = HttpStatusCode.OK
        ) : ResponseHandler<T>(title, message, statusCode, data)
    {
    }
}