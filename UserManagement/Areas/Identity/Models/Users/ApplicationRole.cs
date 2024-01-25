using Microsoft.AspNetCore.Identity;

namespace UserManagement.Areas.Identity.Models.Users
{
    public class ApplicationRole : IdentityRole<Guid>
    {
        public string Description { get; set; } = string.Empty;

    }
}
