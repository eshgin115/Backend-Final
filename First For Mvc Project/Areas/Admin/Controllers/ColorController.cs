using Pronia.Areas.Admin.ViewModels.Color;
using Pronia.Database;
using Pronia.Database.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Pronia.Contracts.Identity;

namespace Pronia.Areas.Admin.Controllers
{
    [Area("admin")]
    [Authorize(Roles = RoleNames.ADMIN)]
    public class ColorController : Controller
    {
        private readonly DataContext _dataContext;

        public ColorController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        #region List
        [HttpGet("list", Name = "admin-color-list")]
        public async Task<IActionResult> ListAsync()
        {
            var model = await _dataContext.Colors
                .Select(n => new ListViewModel(n.Id, n.Name))
                .ToListAsync();

            return View(model);
        }
        #endregion


        #region ADD
        [HttpGet("add", Name = "admin-color-add")]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost("add", Name = "admin-color-add")]
        public async Task<IActionResult> AddAsync(AddViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var color = new Color
            {
                Name = model.Name,
            };
            await _dataContext.Colors.AddAsync(color);
            await _dataContext.SaveChangesAsync();
            return RedirectToRoute("admin-color-list");
        }

        #endregion

        #region Update
        [HttpGet("update/{id}", Name = "admin-color-update")]
        public async Task<IActionResult> UpdateAsync([FromRoute] int id)
        {
            var color = await _dataContext.Colors.FirstOrDefaultAsync(c => c.Id == id);


            if (color is null) return NotFound();

            var model = new UpdateViewModel
            {
                Name=color.Name,
                Id = id
            };

            return View(model);
        }
        [HttpPost("update/{id}", Name = "admin-color-update")]
        public async Task<IActionResult> UpdateAsync(UpdateViewModel model)
        {
            var color = await _dataContext.Colors.FirstOrDefaultAsync(c => c.Id == model.Id);
            if (color is null) return NotFound();





            if (!ModelState.IsValid) return View(model);




            if (!_dataContext.Colors.Any(n => n.Id == model.Id)) return View(model);



            model.Name = color.Name;

          
            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-color-list");

        }

        #endregion


        #region delete
        [HttpPost("delete/{id}", Name = "admin-color-delete")]
        public async Task<IActionResult> DeleteAsync(UpdateViewModel model)
        {
            var color = await _dataContext.Colors.FirstOrDefaultAsync(c => c.Id == model.Id);
            if (color is null) return NotFound();

            _dataContext.Colors.Remove(color);
            await _dataContext.SaveChangesAsync();
            return RedirectToRoute("admin-navbar-list");

        }
        #endregion
    }
}
