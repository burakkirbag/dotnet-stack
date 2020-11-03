using Microsoft.AspNetCore.Authorization;

namespace stack
{
    [Authorize(Roles = "Admin")]
    public class SecureManagementController : ManagementController
    {
    }
}
