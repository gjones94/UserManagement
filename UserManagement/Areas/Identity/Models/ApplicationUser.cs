﻿using Microsoft.AspNetCore.Identity;

namespace UserManagement.Areas.Identity.Models
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public DateTime Birthday { get; set; }

    }
}
