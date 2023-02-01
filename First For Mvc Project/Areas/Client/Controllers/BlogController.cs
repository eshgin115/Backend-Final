using First_For_Mvc_Project.Areas.Client.ViewComponents;
using First_For_Mvc_Project.Areas.Client.ViewModels.Blog;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pronia.Database;

namespace First_For_Mvc_Project.Areas.Client.Controllers
{

    [Area("client")]
    [Route("blog")]
    public class BlogController : Controller
    {
        private readonly DataContext _dataContext;
        public BlogController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        [HttpGet("index", Name = "client-blog-index")]
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
                    Tags = await _dataContext.Tags.Select(t => new Pronia.Areas.Admin.ViewModels.Tag.ListViewModel(t.Id, t.Name)).ToListAsync()

                };
                return model;
            }
        }
        [HttpGet("search", Name = "client-blog-search")]
        public IActionResult Search(ListViewModel model)
        {
            return ViewComponent(nameof(Blog),
                 new
                 {
                     slide = string.Empty,
                     tagId = model.TagId,
                     searchQuery = model.SearchQuery

                 });
        }
    }
}
