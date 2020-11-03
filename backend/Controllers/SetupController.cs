using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using stack.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace stack.Controllers
{
    public class SetupController : ManagementController
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<UserRole> _roleManager;

        public SetupController(UserManager<User> userManager, RoleManager<UserRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> Index()
        {
            if (!Db.UserRoles.Any())
            {
                // Create admin role
                var adminRole = new UserRole { Name = "Admin" };
                var createdAdminRole = await _roleManager.CreateAsync(adminRole);

                // Create user role
                var userRole = new UserRole { Name = "User" };
                var createdUserRole = await _roleManager.CreateAsync(userRole);
            }

            if (!Db.Users.Any())
            {
                // Create admin
                var admin = new User { Firstname = "Burak", Lastname = "Kırbağ", Email = "burak@burakkirbag.com", UserName = "burak@burakkirbag.com" };
                var createdAdmin = await _userManager.CreateAsync(admin, "1234567890");
                if (createdAdmin.Succeeded)
                {
                    await _userManager.AddToRoleAsync(admin, "Admin");
                    await _userManager.AddToRoleAsync(admin, "User");
                }

                // Create user
                var user = new User { Firstname = "Burak", Lastname = "Kırbağ", Email = "burakkirbag@gmail.com", UserName = "burakkirbag@gmail.com" };
                var createdUser = await _userManager.CreateAsync(user, "1234567890");
                if (createdUser.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "User");
                }
            }

            if (!Db.Books.Any())
            {
                // Create books
                await Db.Books.AddRangeAsync(new List<Book> {
                    new Book{ Id = Guid.NewGuid(), Name = "Book 1", Summary = "Book Summary 1"},
                    new Book{ Id = Guid.NewGuid(), Name = "Book 2", Summary = "Book Summary 2"},
                    new Book{ Id = Guid.NewGuid(), Name = "Book 3", Summary = "Book Summary 3"},
                    new Book{ Id = Guid.NewGuid(), Name = "Book 4", Summary = "Book Summary 4"},
                    new Book{ Id = Guid.NewGuid(), Name = "Book 5", Summary = "Book Summary 5"},
                    new Book{ Id = Guid.NewGuid(), Name = "Book 6", Summary = "Book Summary 6"},
                    new Book{ Id = Guid.NewGuid(), Name = "Book 7", Summary = "Book Summary 7"},
                    new Book{ Id = Guid.NewGuid(), Name = "Book 8", Summary = "Book Summary 8"},
                    new Book{ Id = Guid.NewGuid(), Name = "Book 9", Summary = "Book Summary 9"},
                    new Book{ Id = Guid.NewGuid(), Name = "Book 10", Summary = "Book Summary 10"},
                });

                await Db.SaveChangesAsync();
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
