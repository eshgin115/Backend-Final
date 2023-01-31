using Pronia.Areas.Client.ViewModels.Account;
using Pronia.Contracts.Order;
using Pronia.Database;
using Pronia.Database.Models;
using Pronia.Services.Concretes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Pronia.Areas.Client.Controllers
{
    [Area("client")]
    [Route("account")]
    [Authorize]
    public class AccountController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IUserService _userService;
        private User _CurrentUser;

        public AccountController
            (DataContext dataContext,
            IUserService userService)
        {
            _dataContext = dataContext;
            _userService = userService;
            _CurrentUser = userService.CurrentUser;
        }
        [HttpGet("updatePassword", Name = "client-account-updatePassword")]
        public IActionResult UpdatePassword()
        {

            return View();
        }
        [HttpPost("updatePassword", Name = "client-account-updatePassword")]
        public async Task<IActionResult> UpdatePassword(UpdatePasswordViewModel model)
        {
            if (!ModelState.IsValid) return View(model);


            if (!BCrypt.Net.BCrypt.Verify(model.CurrentPassword, _CurrentUser.Password))
            {
                ModelState.AddModelError("CurrentPassword", "CurrentPassword is not correct");
                return View(model);
            }
            string passwordHash = HashPassword(); 

            _CurrentUser.Password = passwordHash;

            await _dataContext.SaveChangesAsync();



            return View();


            string HashPassword()
            {
                var hash = BCrypt.Net.BCrypt.HashPassword(model.Password);
                return hash;
            }


        }


        #region UpdateUserData
        [HttpGet("UpdateUserData", Name = "client-account-UpdateUserData")]
        public IActionResult UpdateUserData()
        {

            return View();
        }
        [HttpPost("UpdateUserData", Name = "client-account-UpdateUserData")]
        public async Task<IActionResult> UpdateUserData(UpdateUserDataViewModel model)
        {
            if (!ModelState.IsValid) return View(model);


            if (!BCrypt.Net.BCrypt.Verify(model.CurrentPassword, _CurrentUser.Password))
            {
                ModelState.AddModelError("CurrentPassword", "CurrentPassword is not correct");
                return View(model);
            }


            _CurrentUser.FirstName = model.Name;
            _CurrentUser.LastName = model.LastName;

            await _dataContext.SaveChangesAsync();

           

            return View();
        }
        #endregion



        [HttpGet("orders", Name = "client-account-orders")]
        public async Task<IActionResult> OrdersAsync()
        {
            var model = await _dataContext.Orders
                       .Where(o => o.UserId == _userService.CurrentUser.Id)

                       .Select(o =>
                           new OrderViewModel(
                               o.Id,
                               o.CreatedAt,
                               StatusCodeExtensions.GetShortNameWithStatus(o.Status!),
                               o.SumTotalPrice,
                               o.OrderProducts.Count
                              ))
                       .ToListAsync();


            return View(model);
        }
        [HttpGet("contact", Name = "client-account-contact")]
        public IActionResult Contact()
        {
            return View(new ContactViewModel());
        }
        [HttpPost("contact-add", Name = "client-account-contact-add")]
        public async Task<IActionResult> ContactAddAsync(ContactViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            var contact = CreateContact();

            await _dataContext.Contacts.AddAsync(contact);
            await _dataContext.SaveChangesAsync();

            Contact CreateContact()
            {
                var contact = new Contact
                {
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Message = model.Message,
                    Phone = model.Phone,
                    UserId = _CurrentUser.Id,

                };
                return contact;
            }

            return RedirectToRoute("client-account-contact");
        }
    }
}
