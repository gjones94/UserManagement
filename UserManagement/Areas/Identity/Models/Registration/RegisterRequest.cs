using Microsoft.AspNetCore.Identity.Data;
using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;

namespace UserManagement.Areas.Identity.Models.Registration
{
    public class RegisterRequest
    {
        public string Email { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;
    }
}
