using First_For_Mvc_Project.Areas.Client.ViewModels.Authentication;
using First_For_Mvc_Project.Attributs;
using First_For_Mvc_Project.Database;
using First_For_Mvc_Project.Services.Concretes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace First_For_Mvc_Project.Areas.Client.Controllers
{
    [Area("client")]
    [Route("auth")]
    public class AuthenticationController : Controller
    {
        private readonly DataContext _dbContext;
        private readonly IUserService _userService;

        public AuthenticationController(DataContext dbContext, IUserService userService)
        {
            _dbContext = dbContext;
            _userService = userService;
        }

        #region Login and Logout
        [HttpGet("login", Name = "client-auth-login")]
        [ServiceFilter(typeof(IsAuthenticated))]
        public async Task<IActionResult> LoginAsync()
        {
           

            return View(new LoginViewModel());
        }

        [HttpPost("login", Name = "client-auth-login")]
        public async Task<IActionResult> LoginAsync(LoginViewModel? model)
        {
            if (!ModelState.IsValid) return View(model);




            if (!await _userService.CheckPasswordAsync(model!.Email, model!.Password))
            {
                ModelState.AddModelError(String.Empty, "Email or password is not correct");
                return View(model);
            }

            if (!await _userService.CheckEmailConfirmedAsync(model!.Email))
            {
                ModelState.AddModelError(String.Empty, "Email is not confirmed");
                return View(model);
            }

            await _userService.SignInAsync(model!.Email, model!.Password);

            return RedirectToRoute("client-home-index");
        }
        [Authorize]
        [HttpGet("logout", Name = "client-auth-logout")]
        public async Task<IActionResult> LogoutAsync()
        {
            await _userService.SignOutAsync();

            return RedirectToRoute("client-home-index");
        }

        #endregion

        #region Register

        [HttpGet("register", Name = "client-auth-register")]
        [ServiceFilter(typeof(IsAuthenticated))]
        public ViewResult Register()
        {
            return View(new RegisterViewModel());
        }
        [HttpPost("register", Name = "client-auth-register")]
        public async Task<IActionResult> RegisterAsync(RegisterViewModel model)
        {
            if (!ModelState.IsValid) return View(model);


            var user = await _dbContext.Users.SingleOrDefaultAsync(u => u.Email == model.Email);
            if (user is not null)
            {
                ModelState.AddModelError("Email", "email already exists");
                return View(model);
            }
            string passwordHash = HashPassword();
            model.Password = passwordHash;
            await _userService.CreateAsync(model);

          
            return RedirectToRoute("client-auth-login");

            string HashPassword()
            {
                var hash = BCrypt.Net.BCrypt.HashPassword(model.Password);
                return hash;
            }
        }

        [HttpGet("activate/{token}", Name = "client-auth-activate")]
        public async Task<IActionResult> ActivateAsync([FromRoute] string token)
        {
            var userActivation = await _dbContext.UserActivations
                .Include(ua => ua.User)
                .FirstOrDefaultAsync(ua =>
                    !ua!.User!.IsEmailConfirmed &&
                    ua.ActivationToken == token);

            if (userActivation is null) return NotFound();



            if (DateTime.Now > userActivation!.ExpireDate) return Ok("Token expired olub teessufler");



            userActivation!.User!.IsEmailConfirmed = true;

            await _dbContext.SaveChangesAsync();

            return RedirectToRoute("client-auth-login");
        }

        #endregion
    }
}
