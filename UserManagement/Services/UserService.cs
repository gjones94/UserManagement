using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UserManagement.Areas.Identity.Models.Users;
using UserManagement.Data;

namespace UserManagement.Services
{
    public class UserService
    {
        public readonly UserManager<ApplicationUser> UserManager;
        public readonly RoleManager<ApplicationRole> RoleManager;
        public readonly UserDbContext UserDbContext;

        public UserService(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, UserDbContext dbContext) 
        {
            UserManager = userManager;
            RoleManager = roleManager;
            UserDbContext = dbContext;
        }
    }
}
