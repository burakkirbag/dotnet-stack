using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using stack.Data;

namespace stack
{
    public class DbController : Controller
    {
        private StackDbContext _db;
        protected StackDbContext Db => _db ?? HttpContext?.RequestServices.GetRequiredService<StackDbContext>();
    }
}
