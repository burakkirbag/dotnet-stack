using Microsoft.AspNetCore.Http;

namespace stack.Helper
{
    public static class HttpContextHelper
    {
        public static bool IsApiCall(this HttpContext context)
        {
            return context.Request.Path.StartsWithSegments("/api");
        }
    }
}
