using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using stack.Models;
using stack.Models.View;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;

namespace stack.Helper.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.HttpContext.IsApiCall())
            {
                var result = new ApiReturn<object>()
                {
                    Code = (int)HttpStatusCode.InternalServerError,
                    Success = false,
                    Message = context.Exception.Message,
                    Data = context.Exception
                };

                context.HttpContext.Response.ContentType = "application/json";
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.HttpContext.Response.WriteAsync(result.ToString());
            }
            else
            {
                var error = new ErrorVM
                {
                    RequestId = Activity.Current?.Id ?? context.HttpContext.TraceIdentifier,
                    Message = context.Exception.Message,
                    DetailMessage = context.Exception.StackTrace,
                    Errors = new List<string>()
                };

                if (context.Exception.InnerException != null)
                {
                    error.Errors.Add(context.Exception.InnerException.Message);
                    error.Errors.Add(context.Exception.InnerException.StackTrace);
                }

                context.ExceptionHandled = true;
                context.Result = new RedirectToActionResult("Index", "Error", error);
            }
        }
    }
}
