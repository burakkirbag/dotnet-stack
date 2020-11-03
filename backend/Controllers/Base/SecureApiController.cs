using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using stack.Data;
using System;

namespace stack
{
    [Authorize(Roles = "User")]
    public class SecureApiController : ApiController
    {
        private UserManager<User> _userManager = null;
        public UserManager<User> UserManager
        {
            get
            {
                if (_userManager == null)
                    _userManager = HttpContext?.RequestServices.GetRequiredService<UserManager<User>>();

                return _userManager;
            }
        }

        public Guid UserId
        {
            get
            {
                var userId = UserManager.GetUserId(User);
                return Guid.Parse(userId);
            }
        }
    }
}
