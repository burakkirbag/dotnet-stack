using Microsoft.AspNetCore.Mvc;
using stack.Models;
using System.Collections.Generic;

namespace stack
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ApiController : DbController
    {
        [NonAction]
        protected IActionResult Success<T>(T data, string message = "") =>
           StatusCode(200, new ApiReturn<T>
           {
               Code = 200,
               Success = true,
               Data = data,
               Message = message
           });

        [NonAction]
        protected IActionResult SuccessPaged<T>(List<T> data, int page, int perPage, int totalPages, string message) =>
            StatusCode(200, new PagedApiReturn<T>
            {
                Code = 200,
                Success = true,
                Data = data,
                Message = message,
                Page = page,
                PerPage = perPage,
                TotalPages = totalPages
            });

        [NonAction]
        protected IActionResult BadRequest<T>(T data) => StatusCode(400, new ApiReturn<T> { Code = 400, Success = false, Data = data });

        [NonAction]
        protected IActionResult BadRequest<T>(T data, string message) => StatusCode(400, new ApiReturn<T> { Code = 400, Success = false, Data = data, Message = message });

        [NonAction]
        protected IActionResult Unauthorized<T>(T data) =>
            StatusCode(401, new ApiReturn<T> { Code = 401, Success = false, Data = data });

        [NonAction]
        protected IActionResult Forbidden<T>(T data) => StatusCode(403, new ApiReturn<T> { Code = 403, Success = false, Data = data });

        [NonAction]
        protected IActionResult NotFound<T>(T data) => StatusCode(404, new ApiReturn<T> { Code = 404, Success = false, Data = data });

        [NonAction]
        protected IActionResult Error(string message, string[] errors) => StatusCode(500,
            new ApiReturn<string[]> { Code = 500, Success = false, Data = errors, Message = message });
    }
}
