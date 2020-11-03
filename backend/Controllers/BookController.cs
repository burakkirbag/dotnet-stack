using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace stack.Controllers
{
    public class BookController : SecureManagementController
    {
        public async Task<IActionResult> Index()
        {
            var books = await Db.Books.ToListAsync();

            return View(books);
        }
    }
}
