using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace stack.Controllers
{
    public class HomeController : SecureManagementController
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
