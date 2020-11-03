using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using stack.Models;
using stack.Models.View;
using System.Collections.Generic;
using System.Diagnostics;

namespace stack.Helper.Filters
{
    public class ValidationFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var errors = new List<string>();
                foreach (var modelError in context.ModelState)
                {
                    foreach (var error in modelError.Value.Errors)
                    {
                        errors.Add(error.ErrorMessage);
                    }
                }

                if (context.HttpContext.IsApiCall())
                {
                    var result = new ApiReturn<List<string>>
                    {
                        Code = 400,
                        Success = false,
                        Message = "Lütfen girmiş olduğunuz bilgileri kontrol edin.",
                        Data = errors
                    };

                    context.Result = new BadRequestObjectResult(result);
                }
                else
                {
                    var error = new ErrorVM
                    {
                        RequestId = Activity.Current?.Id ?? context.HttpContext.TraceIdentifier,
                        Message = "Lütfen girmiş olduğunuz bilgileri kontrol edin.",
                        DetailMessage = "",
                        Errors = errors
                    };

                    var actionDescriptor = (Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor)context.ActionDescriptor;
                    var controller = (Controller)context.Controller;

                    controller.TempData.TryAdd("Type", "Warning");
                    controller.TempData.TryAdd("Message", error.Message);
                    controller.TempData.TryAdd("Errors", error.Errors);

                    context.Result = new RedirectToActionResult(actionDescriptor.ActionName, actionDescriptor.ControllerName, null);
                }
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {

        }
    }
}
