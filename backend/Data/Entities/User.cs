using Microsoft.AspNetCore.Identity;
using System;

namespace stack.Data
{
    public class User : IdentityUser<Guid>
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
    }
}
