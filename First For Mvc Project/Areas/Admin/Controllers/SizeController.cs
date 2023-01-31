using Pronia.Areas.Admin.ViewModels.Size;
using Pronia.Database;
using Pronia.Database.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace Pronia.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("size")]
    [Authorize]
    public class SizeController : Controller
    {
        private readonly DataContext _dataContext;

        public SizeController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        [HttpGet("list", Name = "admin-size-list")]
        public async Task<IActionResult> ListAsync()
        {
            var model = await _dataContext.Sizes
                .Select(n => new ListViewModel(n.Id, n.Name))
                .ToListAsync();

            return View(model);
        }
        [HttpGet("add", Name = "admin-size-add")]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost("add", Name = "admin-size-add")]
        public async Task<IActionResult> AddAsync(AddViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var size = new Size
            {
                Name = model.Name,
            };
            await _dataContext.Sizes.AddAsync(size);
            await _dataContext.SaveChangesAsync();
            return RedirectToRoute("admin-size-list");
        }
        [HttpGet("update/{id}", Name = "admin-size-update")]
        public async Task<IActionResult> UpdateAsync([FromRoute] int id)
        {
            var size = await _dataContext.Sizes.FirstOrDefaultAsync(c => c.Id == id);


            if (size is null) return NotFound();

            var model = new UpdateViewModel
            {
                Name = size.Name,
                Id = id
            };

            return View(model);
        }
        [HttpPost("update/{id}", Name = "admin-size-update")]
        public async Task<IActionResult> UpdateAsync(UpdateViewModel model)
        {
            var size = await _dataContext.Colors.FirstOrDefaultAsync(c => c.Id == model.Id);

            if (size is null) return NotFound();





            if (!ModelState.IsValid) return View(model);




            if (!_dataContext.Sizes.Any(n => n.Id == model.Id)) return View(model);



            model.Name = size.Name;


            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-size-list");

        }
        #region delete
        [HttpPost("delete/{id}", Name = "admin-size-delete")]
        public async Task<IActionResult> DeleteAsync(UpdateViewModel model)
        {
            var size = await _dataContext.Sizes.FirstOrDefaultAsync(c => c.Id == model.Id);
            if (size is null) return NotFound();

            _dataContext.Sizes.Remove(size);
            await _dataContext.SaveChangesAsync();
            return RedirectToRoute("admin-size-list");

        }
        #endregion
    }
}
