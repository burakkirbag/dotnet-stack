using Microsoft.AspNetCore.Mvc;
using stack.Models.View;

namespace stack.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Index(ErrorVM error)
        {
            return View(error);
        }
    }
}
