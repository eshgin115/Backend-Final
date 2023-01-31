using First_For_Mvc_Project.Areas.Admin.ViewModels.AboutComponent;
using First_For_Mvc_Project.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace First_For_Mvc_Project.Areas.Admin.Controllers
{

    [Area("admin")]
    [Route("AboutComponent")]
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
