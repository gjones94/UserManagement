using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Plugins;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using UserManagement.Areas.Identity.Models.Users;
using UserManagement.Data;
using UserManagement.Helpers;

namespace UserManagement.APIs
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        UserDbContext _userContext;
        SignInManager<ApplicationUser> _signInManager;
        UserManager<ApplicationUser> _userManager;

        public UserController(UserDbContext context, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _userContext = context;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<Results<Ok, ValidationProblem>> Register([FromBody] UserManagement.Areas.Identity.Models.Registration.RegisterRequest registration, [FromServices] IServiceProvider serviceProvider)
        {
            var userStore = serviceProvider.GetRequiredService<IUserStore<ApplicationUser>>();
            var emailStore = (IUserEmailStore<ApplicationUser>)userStore;
            var email = registration.Email;

            if (string.IsNullOrEmpty(email) || !new EmailAddressAttribute().IsValid(email))
            {
                return Validation.CreateValidationProblem(IdentityResult.Failed(_userManager.ErrorDescriber.InvalidEmail(email)));
            }

            var user = new ApplicationUser();

            // Populate custom fields
            user.FirstName = registration.FirstName;
            user.LastName = registration.LastName;
            user.PhoneNumber = registration.PhoneNumber;

            await userStore.SetUserNameAsync(user, email, CancellationToken.None);
            await emailStore.SetEmailAsync(user, email, CancellationToken.None);
            var result = await _userManager.CreateAsync(user, registration.Password);

            if (!result.Succeeded)
            {
                return Validation.CreateValidationProblem(result);
            }

            //await SendConfirmationEmailAsync(user, userManager, context, email);
            return TypedResults.Ok();
        }


        [AllowAnonymous]
        [HttpGet]
        public Results<Ok, UnauthorizedHttpResult> IsAuthenticated()
        {
            if (HttpContext.User?.Identity?.IsAuthenticated ?? false)
            {
                return TypedResults.Ok();
            }
            else
            {
                return TypedResults.Unauthorized();
            }
        }

        [HttpPost]
        public async Task<Results<Ok, ProblemHttpResult>> Login([FromBody] LoginRequest login, [FromQuery] bool? useCookies, [FromQuery] bool? useSessionCookies, [FromServices] IServiceProvider serviceProvider)
        {
            var signInManager = serviceProvider.GetRequiredService<SignInManager<ApplicationUser>>();

            var useCookieScheme = useCookies == true || useSessionCookies == true;
            var isPersistent = useCookies == true && useSessionCookies != true;
            signInManager.AuthenticationScheme = useCookieScheme ? IdentityConstants.ApplicationScheme : IdentityConstants.BearerScheme;

            var result = await signInManager.PasswordSignInAsync(login.Email, login.Password, isPersistent, lockoutOnFailure: true);

            if (!result.Succeeded)
            {
                return TypedResults.Problem(result.ToString(), statusCode: StatusCodes.Status401Unauthorized);
            }

            return TypedResults.Ok();
        }

        [AllowAnonymous]
        [HttpGet]
        public Results<UnauthorizedHttpResult, Ok<ApplicationUser>> Info()
        {
            string? userName = HttpContext.User.Identity?.Name;

            ApplicationUser? user = null;

            if (string.IsNullOrEmpty(userName) == false)
            {
                user = _userContext.Users.Where(user => user.UserName.Equals(userName)).FirstOrDefault();
                if (user is not null)
                {
                    return TypedResults.Ok(user);
                }
            }

            return TypedResults.Unauthorized();
        }


        [HttpGet]
        public async Task<Results<Ok, ProblemHttpResult>> Logout()
        {
            if(HttpContext.User?.Identity?.IsAuthenticated ?? false)
            {
                await _signInManager.SignOutAsync();
                return TypedResults.Ok();
            }
            else
            {
                return TypedResults.Problem("There is no logged in user");
            }
        }
    }
}
