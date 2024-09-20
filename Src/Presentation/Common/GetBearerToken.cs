using Microsoft.AspNetCore.Http;

namespace Shortener.Presentation.Common
{
    public sealed class GetBearerToken
    {
        public static string FromHeader(HttpContext context)
        {
            return context.Request
                .Headers
                .Authorization
                .ToString()
                .Replace("Bearer ", "");
        }
    }
}
