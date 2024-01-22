using Microsoft.AspNetCore.Identity;

namespace UserManagement.Areas.Identity.Models
{
    public class ApplicationRole : IdentityRole<Guid>
    {
        public string Description { get; set; }

    }
}
