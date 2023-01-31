﻿using DemoApplication.Areas.Admin.ViewModels.Order;
using Pronia.Areas.Admin.ViewModels.Order;
using Pronia.Contracts.Email;
using Pronia.Contracts.Order;
using Pronia.Database;
using Pronia.Services.Concretes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace Pronia.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/order")]
    [Authorize]
    public class OrderController : Controller
    {
        private readonly DataContext _dbContext;
        private readonly IUserService _userService;
        public IEmailService _emailService { get; set; }

        public OrderController(DataContext dbContext, IUserService userService, IEmailService emailService)
        {
            _dbContext = dbContext;
            _userService = userService;
            _emailService = emailService;
        }
        [HttpGet("list", Name = "admin-order-list")]
        public async Task<IActionResult> ListAsync()
        {
            var model = await _dbContext.Orders

                   .Select(o =>
                       new ListViewModel(
                           o.Id,
                           o.CreatedAt,
                           StatusCodeExtensions.GetShortNameWithStatus(o.Status),
                           o.SumTotalPrice
                          ))
                   .ToListAsync();

            return View(model);
        }
        [HttpGet("update/{id}", Name = "admin-order-update")]
        public async Task<IActionResult> UpdateAsync(string id)
        {
            var order = await _dbContext.Orders.Include(o => o.OrderProducts)
              .FirstOrDefaultAsync(o => o.Id == id);


            if (order is null) return NotFound();

            var model = new UpdateViewModel { Id = id };



            return View(model);
        }
        [HttpPost("update/{id}", Name = "admin-order-update")]
        public async Task<IActionResult> UpdateAsync(string id, UpdateViewModel model)
        {
            var order = await _dbContext.Orders.Include(o => o.User)
              .FirstOrDefaultAsync(o => o.Id == id);


            if (order is null) NotFound();
            order!.Status = model.Statuses;





            var stausMessageDto = PrepareStausMessage(order.User.Email);
            _emailService.Send(stausMessageDto);
            await _dbContext.SaveChangesAsync();

            return RedirectToRoute("admin-order-list");


            MessageDto PrepareStausMessage(string email)
            {
                string body = StatusCodeExtensions.GetNotification(order.Status, order.User.FirstName, order.User.LastName, order.Id);

                string subject = EmailMessages.Subject.ORDER_MESSAGE;

                return new MessageDto(email, subject, body);
            }
        }
        [HttpPost("delete/{orderId}", Name = "admin-order-delete")]
        public async Task<IActionResult> Delete(string orderId)
        {
            var order = await _dbContext.Orders.Include(o => o.OrderProducts).FirstOrDefaultAsync(o => o.Id == orderId);
            if (order == null) return NotFound();

            _dbContext.Orders.Remove(order);
            _dbContext.OrderProducts.RemoveRange(order.OrderProducts!);
            await _dbContext.SaveChangesAsync();


            return RedirectToRoute("admin-order-list");
        }
    }
}
