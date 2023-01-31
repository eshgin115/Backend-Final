using First_For_Mvc_Project.Areas.Client.ViewComponents;
using First_For_Mvc_Project.Areas.Client.ViewModels.Shop;
using First_For_Mvc_Project.Database;
using First_For_Mvc_Project.Services.Concretes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace First_For_Mvc_Project.Areas.Client.Controllers
{
    [Area("client")]
    [Route("shop")]
    public class ShopController : Controller
    {
        private readonly DataContext _dataContext;
        public ShopController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        [HttpGet("index", Name = "client-shop-index")]
        public async Task<IActionResult> IndexAsync(string? searchQuery = null)
        {
            var model = await CreateModel();


          
            return View(model);



            async Task<ListViewModel> CreateModel()
            {
                var model = new ListViewModel
                {
                    SearchQuery = searchQuery,
                    //_dataContext.PlantSizes.Include(ps => ps.Size).Where(ps => ps.PlantId == plant.Id)
                    //.Select(ps => new Admin.ViewModels.Size.ListViewModel(ps.SizeId, ps.Size.Name)).ToList(),
                    Colors = await _dataContext.Colors.Select(c => new Admin.ViewModels.Color.ListViewModel(c.Id, c.Name)).ToListAsync(),
                    Subcategories = await _dataContext.Subcategories.Select(s => new Admin.ViewModels.Subcategory.ListViewModel(s.Id, s.Name)).ToListAsync(),
                    Tags = await _dataContext.Tags.Select(t => new Admin.ViewModels.Tag.ListViewModel(t.Id, t.Name)).ToListAsync()

                };
                return model;
            }
        }
        [HttpGet("search", Name = "client-shop-search")]
        public IActionResult Search(ListViewModel model)
        {
            return ViewComponent(nameof(Product),
                 new
                 {
                     slide = string.Empty,
                     subcategoryId = model.SubcategoryId,
                     colorId = model.ColorId,
                     tagId = model.TagId,
                     searchQuery = model.SearchQuery,
                     minPrice = model.MinPrice,
                     maxPrice = model.MaxPrice

                 });
        }
    }
}
