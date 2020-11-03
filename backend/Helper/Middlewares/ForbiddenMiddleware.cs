using Microsoft.AspNetCore.Http;
using stack.Models;
using System.Net;
using System.Threading.Tasks;

namespace stack.Helper.Middlewares
{
    public class ForbiddenMiddleware
    {
        private readonly RequestDelegate _next;
        public ForbiddenMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            await _next(context);

            if (context.Response.StatusCode == (int)HttpStatusCode.Forbidden)
            {
                if (context.IsApiCall())
                {
                    var result = new ApiReturn<object>
                    {
                        Code = (int)HttpStatusCode.Forbidden,
                        Success = false,
                        Message = "Bu işlemi gerçekleştirmek için yetkiniz bulunmuyor."
                    };

                    context.Response.ContentType = "application/json";
                    context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                    await context.Response.WriteAsync(result.ToString());
                }
                else
                {
                    context.Response.Redirect("/Auth/Login");
                }
            }
        }
    }
}
