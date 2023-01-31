using First_For_Mvc_Project.Areas.Client.ViewModels.Authentication;
using First_For_Mvc_Project.Areas.Client.ViewModels.Basket;
using First_For_Mvc_Project.Contracts.Identity;
using First_For_Mvc_Project.Database;
using First_For_Mvc_Project.Database.Models;
using First_For_Mvc_Project.Exceptions;
using First_For_Mvc_Project.Services.Concretes;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Text.Json;

namespace First_For_Mvc_Project.Services.Services
{
    public class UserService : IUserService
    {

        private readonly DataContext _dataContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserActivationService _userActivationService;
        private User _currentUser;

        public UserService(DataContext dataContext,
            IHttpContextAccessor httpContextAccessor,
            IUserActivationService userActivationService)
        {
            _dataContext = dataContext;
            _httpContextAccessor = httpContextAccessor;
            _userActivationService = userActivationService;
        }

        public bool IsAuthenticated
        {
            get => _httpContextAccessor.HttpContext!.User.Identity!.IsAuthenticated;
        }

        public User CurrentUser
        {
            get
            {
                if (_currentUser is not null)
                {
                    return _currentUser;
                }

                var idClaim = _httpContextAccessor.HttpContext!.User.Claims.FirstOrDefault(C => C.Type == CustomClaimNames.ID);
                if (idClaim is null)
                    throw new IdentityCookieException("Identity cookie not found");

                _currentUser = _dataContext.Users.First(u => u.Id == int.Parse(idClaim.Value));

                return _currentUser;
            }
        }

        public async Task<bool> CheckEmailConfirmedAsync(string? email)
        {
            ArgumentNullException.ThrowIfNull(email);
            return await _dataContext.Users.AnyAsync(u => u.Email == email && u.IsEmailConfirmed);
        }


        public string GetCurrentUserFullName()
        {
            return $"{CurrentUser.FirstName} {CurrentUser.LastName}";
        }

        public async Task<bool> CheckPasswordAsync(string? email, string? password)
        {
            ArgumentNullException.ThrowIfNull(email);
            ArgumentNullException.ThrowIfNull(password);
            var user = await _dataContext.Users.SingleOrDefaultAsync(u => u.Email == email);




            if (user is null) return false;



            return BCrypt.Net.BCrypt.Verify(password, user!.Password);

        }
        public async Task SignInAsync(int id, string? role = null)
        {
            var claims = new List<Claim>
            {
                new Claim(CustomClaimNames.ID, id.ToString())
            };

            if (role is not null)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var userPrincipal = new ClaimsPrincipal(identity);

            await _httpContextAccessor.HttpContext!.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, userPrincipal);
        }

        public async Task SignInAsync(string? email, string? password, string? role = null)
        {
            ArgumentNullException.ThrowIfNull(email);
            ArgumentNullException.ThrowIfNull(password);
            var user = await _dataContext.Users.Include(u => u.Role).FirstAsync(u => u.Email == email);


            if (BCrypt.Net.BCrypt.Verify(password, user!.Password))
            {
                if (user.Role != null && user.Role.Name == RoleNames.ADMIN)
                {
                    await SignInAsync(user.Id, user.Role.Name);
                }
                else
                {

                    await SignInAsync(user.Id, role);
                }
            }
        }

        public async Task SignOutAsync()
        {
            await _httpContextAccessor.HttpContext!.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        public async Task CreateAsync(RegisterViewModel model)
        {
            ArgumentNullException.ThrowIfNull(model);
            var user = await CreateUserAsync();
            var basket = await CreateBasketAsync();
            await CreteBasketProductsAsync();

            await _userActivationService.SendActivationUrlAsync(user);

            await _dataContext.SaveChangesAsync();


            async Task<User> CreateUserAsync()
            {
                var user = new User
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    Password = model.Password,
                };
                await _dataContext.Users.AddAsync(user);

                return user;
            }

            async Task<Basket> CreateBasketAsync()
            {
                //Create basket process
                var basket = new Basket
                {
                    User = user,
                };
                await _dataContext.Baskets.AddAsync(basket);

                return basket;
            }

            async Task CreteBasketProductsAsync()
            {
                //Add products to basket if cookie exists
                var productCookieValue = _httpContextAccessor.HttpContext!.Request.Cookies["products"];
                if (productCookieValue is not null)
                {
                    var productsCookieViewModel = JsonSerializer.Deserialize<List<ProductCookieViewModel>>(productCookieValue);
                    foreach (var productCookieViewModel in productsCookieViewModel)
                    {
                        var plant = await _dataContext.Plants.FirstOrDefaultAsync(p => p.Id == productCookieViewModel.Id);
                        var basketProduct = new BasketProduct
                        {
                            Basket = basket,
                            PlantId = plant!.Id,
                            Quantity = productCookieViewModel.Quantity,
                        };

                        await _dataContext.BasketProducts.AddAsync(basketProduct);
                    }
                }
            }

        }
    }
}
