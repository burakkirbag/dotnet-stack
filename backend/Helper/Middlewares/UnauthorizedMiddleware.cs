using Microsoft.AspNetCore.Http;
using stack.Models;
using System.Net;
using System.Threading.Tasks;

namespace stack.Helper.Middlewares
{
    public class UnauthorizedMiddleware
    {
        private readonly RequestDelegate _next;
        public UnauthorizedMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            await _next(context);

            if (context.Response.StatusCode == (int)HttpStatusCode.Unauthorized)
            {
                if (context.IsApiCall())
                {
                    var result = new ApiReturn<object>
                    {
                        Code = (int)HttpStatusCode.Unauthorized,
                        Success = false,
                        Message = "Token geçersiz. Lütfen geçerli bir token kullanarak tekrar deneyin."
                    };

                    context.Response.ContentType = "application/json";
                    context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
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
