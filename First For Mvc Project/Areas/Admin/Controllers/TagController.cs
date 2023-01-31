using First_For_Mvc_Project.Areas.Admin.ViewModels.Tag;
using First_For_Mvc_Project.Database;
using First_For_Mvc_Project.Database.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace First_For_Mvc_Project.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("tag")]
    public class TagController : Controller
    {
        private readonly DataContext _dataContext;

        public TagController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        [HttpGet("list", Name = "admin-tag-list")]
        public async Task<IActionResult> ListAsync()
        {
            var model = await _dataContext.Tags
                .Select(n => new ListViewModel(n.Id, n.Name))
                .ToListAsync();

            return View(model);
        }
        [HttpGet("add", Name = "admin-tag-add")]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost("add", Name = "admin-tag-add")]
        public async Task<IActionResult> AddAsync(AddViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var tag = new Tag
            {
                Name = model.Name,
            };
            await _dataContext.Tags.AddAsync(tag);
            await _dataContext.SaveChangesAsync();
            return RedirectToRoute("admin-tag-list");
        }
        [HttpGet("update/{id}", Name = "admin-tag-update")]
        public async Task<IActionResult> UpdateAsync([FromRoute] int id)
        {
            var tag = await _dataContext.Tags.FirstOrDefaultAsync(c => c.Id == id);


            if (tag is null) return NotFound();

            var model = new UpdateViewModel
            {
                Name = tag.Name,
                Id = id
            };

            return View(model);
        }
        [HttpPost("update/{id}", Name = "admin-tag-update")]
        public async Task<IActionResult> UpdateAsync(UpdateViewModel model)
        {
            var tag = await _dataContext.Tags.FirstOrDefaultAsync(c => c.Id == model.Id);
            if (tag is null) return NotFound();





            if (!ModelState.IsValid) return View(model);




            if (!_dataContext.Tags.Any(n => n.Id == model.Id)) return View(model);



            model.Name = tag.Name;


            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-tag-list");

        }
        #region delete
        [HttpPost("delete/{id}", Name = "admin-tag-delete")]
        public async Task<IActionResult> DeleteAsync(UpdateViewModel model)
        {
            var tag = await _dataContext.Tags.FirstOrDefaultAsync(c => c.Id == model.Id);
            if (tag is null) return NotFound();

            _dataContext.Tags.Remove(tag);
            await _dataContext.SaveChangesAsync();
            return RedirectToRoute("admin-tag-list");

        }
        #endregion
    }
}
