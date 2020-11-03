using Microsoft.AspNetCore.Identity;
using stack.Data;
using stack.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace stack.Services
{
    public interface IUserService
    {
        Task<ServiceReturn<User>> Create(User user, string password);
        Task<ServiceReturn<bool>> CheckAndInsertRoles(params string[] roles);
        Task<ServiceReturn<string>> AddToRole(User user, string role);
    }

    public class UserService : BaseService, IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<UserRole> _roleManager;

        public UserService(StackDbContext db, UserManager<User> userManager, RoleManager<UserRole> roleManager) : base(db)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<ServiceReturn<User>> Create(User user, string password)
        {
            var createdUser = await _userManager.CreateAsync(user, password);
            if (!createdUser.Succeeded)
            {
                var fail = Failed<User>("Kullanıcı oluşturulamadı.");
                foreach (var error in createdUser.Errors)
                {
                    fail.Errors.Add(error.Description);
                }
                return fail;
            }

            return Success<User>(user);
        }

        public async Task<ServiceReturn<bool>> CheckAndInsertRoles(params string[] roles)
        {
            var notFound = new List<string>();

            foreach (var role in roles)
            {
                var exists = await _roleManager.RoleExistsAsync(role);
                if (!exists) notFound.Add(role);
            }

            if (notFound.Count > 0)
            {
                foreach (var r in notFound)
                {
                    var role = new UserRole
                    {
                        Name = r
                    };

                    var createdRole = await _roleManager.CreateAsync(role);
                    if (!createdRole.Succeeded)
                    {
                        var fail = Failed<bool>("Rol oluşturulamadı.");
                        foreach (var error in createdRole.Errors)
                        {
                            fail.Errors.Add(error.Description);
                        }
                        return fail;
                    }
                }
            }

            return Success<bool>("Roller geçerli, işleminize devam edebilirsiniz.");
        }

        public async Task<ServiceReturn<string>> AddToRole(User user, string role)
        {
            var addedRole = await _userManager.AddToRoleAsync(user, role);

            if (!addedRole.Succeeded)
            {
                var fail = Failed<string>("Kullanıcıya rol atanırken bir hata oluştu.");
                foreach (var error in addedRole.Errors)
                {
                    fail.Errors.Add(error.Description);
                }
                return fail;
            }

            return Success<string>(role, "Kullanıcıya rol atama işlemi başarıyla tamamlandı.");
        }
    }
}
