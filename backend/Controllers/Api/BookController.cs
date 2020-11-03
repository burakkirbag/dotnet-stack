using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using stack.Models;
using stack.Models.DTOs;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace stack.Controllers.Api
{
    public class BookController : ApiController
    {
        public BookController()
        {
        }

        [HttpGet]
        [ProducesResponseType(typeof(ApiReturn<List<BookDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiReturn<List<BookDto>>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get()
        {
            var books = await Db.Books
                .Select(x => new BookDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Summary = x.Summary
                })
                .OrderBy(x => x.Name)
                .ToListAsync();

            return Success<List<BookDto>>(books, "Kitaplar listelenmektedir.");
        }
    }
}
