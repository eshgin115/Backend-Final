using Pronia.Areas.Admin.ViewModels.AboutComponent;
using Pronia.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Pronia.Contracts.Identity;

namespace Pronia.Areas.Admin.Controllers
{

    [Area("admin")]
    [Route("AboutComponent")]
    [Authorize(Roles = RoleNames.ADMIN)]
    public class AboutComponentController : Controller
    {
        private readonly DataContext _dataContext;

        public AboutComponentController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        [HttpGet("update", Name = "admin-AboutComponent-update")]
        public async Task<IActionResult> UpdateAsync()
        {
            var aboutComponent = await _dataContext.AboutComponents.SingleOrDefaultAsync();
            var model = new UpdateViewModel
            {
                Content = aboutComponent.Content
            };
            return View(model);
        }
        [HttpPost("update", Name = "admin-AboutComponent-update")]
        public async Task<IActionResult> UpdateAsync(UpdateViewModel model)
        {
            var aboutComponent = await _dataContext.AboutComponents.SingleOrDefaultAsync();


            aboutComponent.Content = model.Content;

            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-AboutComponent-update");

        }
    }
}
