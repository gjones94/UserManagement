using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Net;
using UserManagement.Areas.Identity.Models.Users;

namespace UserManagement.APIs
{
    [Route("[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class RegisterController : ControllerBase
    {
   

        UserManager<ApplicationUser> _userManager;

        public RegisterController(UserManager<ApplicationUser> userManager, ILogger<RegisterController> logger)
        {
            _userManager = userManager;
        }

       

        /*
         * TODO
         * 1) Create a custom register api method to add custom data to app user class. 
         * 2) F12 the AddIdentityMap in program.cs to inspect current register api 
         * 3) Make sure that the program can access the api in UserManagement project
         * 4) put controller in it's own API folder
         * 
         */
    }


}

