using Common.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using UserManagement.Areas.Identity.Models.Users;

namespace UserManagement.Services
{
    public class UserSeedService : ISeedService
    {
        private readonly UserService _userService;

        public UserSeedService(UserService userService)
        {
            _userService = userService;
        }

        public async virtual Task Seed()
        {
            string adminEmail = "gerald.h.jones94@gmail.com";

            ApplicationUser adminUser = new ApplicationUser()
            {
                FirstName = "Admin",
                LastName = "User",
                Email = adminEmail,
                UserName = adminEmail
            };

            IdentityResult result = await _userService.UserManager.CreateAsync(adminUser, "Admin!1234");

            if(result.Succeeded)
            {
                Console.WriteLine("Created Default Admin User");
            }
        }
    }
}
