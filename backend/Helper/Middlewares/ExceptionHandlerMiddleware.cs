using Microsoft.AspNetCore.Http;
using stack.Models;
using stack.Models.View;
using System;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;

namespace stack.Helper.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                if (context.IsApiCall())
                {
                    var result = new ApiReturn<object>()
                    {
                        Code = (int)HttpStatusCode.InternalServerError,
                        Success = false,
                        Message = ex.Message
                    };

                    context.Response.ContentType = "application/json";
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    await context.Response.WriteAsync(result.ToString());
                }
                else
                {
                    var error = new ErrorVM
                    {
                        RequestId = Activity.Current?.Id ?? context.TraceIdentifier,
                        Message = ex.Message,
                        DetailMessage = ex.StackTrace
                    };

                    context.Response.Redirect("/Error/Index");
                }
            }
        }
    }
}
