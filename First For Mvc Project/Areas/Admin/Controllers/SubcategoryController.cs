using Pronia.Areas.Admin.ViewModels.Subcategory;
using Pronia.Database;
using Pronia.Database.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Pronia.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("subcategory")]
    public class SubcategoryController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly ILogger<SubcategoryController> _logger;
        public SubcategoryController(DataContext dataContext, ILogger<SubcategoryController> logger)
        {
            _dataContext = dataContext;
            _logger = logger;
        }

        [HttpGet("list", Name = "admin-subcategory-list")]
        public async Task<IActionResult> List()
        {
            var model = await _dataContext.Subcategories.Select(s => new ListViewModel(s.Id, s.Name, s.Category.Name)).ToListAsync();
            return View(model);
        }

        #region Add
        [HttpGet("add", Name = "admin-subcategory-add")]
        public async Task<IActionResult> AddAsync()
        {
            var model = new AddViewModel
            {

                Categories = await _dataContext.Categories.Select(n => new ViewModels.Category.ListViewModel(n.Id, n.Name)).ToListAsync()

            };
            return View(model);
        }
        [HttpPost("add", Name = "admin-subcategory-add")]
        public async Task<IActionResult> Add(AddViewModel model)
        {
            if (!ModelState.IsValid) return await GetView(model);

            if (!_dataContext.Categories.Any(n => n.Id == model.CategoryId))
            {
                ModelState.AddModelError(String.Empty, "Category is not found");
                return await GetView(model);
            }




            await AddBook();

            return RedirectToRoute("admin-subcategory-list");




            async Task<IActionResult> GetView(AddViewModel model)
            {
                model.Categories = await _dataContext.Categories
                    .Select(s => new ViewModels.Category.ListViewModel(s.Id, s.Name))
                    .ToListAsync();


                return View(model);
            }

            async Task AddBook()
            {
                var subcategory = new Subcategory
                {
                    Name = model.Name,
                    Categoryİd = model.CategoryId,
                    

                };

                await _dataContext.Subcategories.AddAsync(subcategory);



                await _dataContext.SaveChangesAsync();
            }
        }

        #endregion
        #region Update
        [HttpGet("update/{id}", Name = "admin-subcategory-update")]
        public async Task<IActionResult> UpdateAsync([FromRoute] int id)
        {
            var subcategory = await _dataContext.Subcategories.FirstOrDefaultAsync(n => n.Id == id);
            if (subcategory is null) return NotFound();

            var model = new UpdateViewModel
            {

                Id = subcategory.Id,
                Name = subcategory.Name,
                Categories = await _dataContext.Categories.Select(s => new ViewModels.Category.ListViewModel(s.Id, s.Name)).ToListAsync()
            };

            return View(model);
        }


        [HttpPost("update/{id}", Name = "admin-subcategory-update")]
        public async Task<IActionResult> UpdateAsync(UpdateViewModel model)
        {
            var subcategory = await _dataContext.Subcategories.FirstOrDefaultAsync(b => b.Id == model.Id);
            if (subcategory is null) return NotFound();


            if (!ModelState.IsValid) return await GetView(model);


            if (!_dataContext.Categories.Any(n => n.Id == model.Categoryİd))
            {
                ModelState.AddModelError(String.Empty, "Category is not found");
                return await GetView(model);
            }




            await UpdateBookAsync();

            return RedirectToRoute("admin-subcategory-list");




            async Task<IActionResult> GetView(UpdateViewModel model)
            {
                model.Categories = await _dataContext.Categories.Select(s => new ViewModels.Category.ListViewModel(s.Id, s.Name)).ToListAsync();



                return View(model);
            }

            async Task UpdateBookAsync()
            {

                subcategory.Categoryİd = model.Categoryİd;
                subcategory.Name = model.Name; ;

                await _dataContext.SaveChangesAsync();
            }
        }

        #endregion

        #region Delete

        [HttpPost("delete/{id}", Name = "admin-subcategory-delete")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        {
            var subcategory = await _dataContext.Subcategories.FirstOrDefaultAsync(b => b.Id == id);
            if (subcategory is null) return NotFound();


            _dataContext.Subcategories.Remove(subcategory);
            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-subcategory-list");
        }

        #endregion
    }
}
