using System.Net;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;

namespace Shortener.Controllers.ResponseHandlers
{
    public class ResponseHandler<T>(
        string message,
        HttpStatusCode statusCode,
        T? data
        )
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public T? Data {get; set;} = data;
        public string Message { get; set; } = message;
        public HttpStatusCode StatusCode { get; set; } = statusCode;
    }
}