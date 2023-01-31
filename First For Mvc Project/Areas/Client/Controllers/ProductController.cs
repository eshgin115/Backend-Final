using Pronia.Areas.Client.ViewModels.Product;
using Pronia.Contracts.File;
using Pronia.Database;
using Pronia.Services.Concretes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Pronia.Areas.Client.Controllers
{
    [Area("client")]
    [Route("product")]
    public class ProductController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IFileService _fileService;
        public ProductController(DataContext dataContext, IFileService fileService)
        {
            _dataContext = dataContext;
            _fileService = fileService;
        }
        [HttpGet("show/{id}", Name = "client-product-show")]
        public async Task<IActionResult> ShowAsync(int Id)
        {
            var plant = await _dataContext.Plants
                                .Include(p => p.PlantImages)
                                .FirstOrDefaultAsync(p => p.Id == Id);

            if (plant is null) return NotFound();


            var model = await CreateModel();


            return View(model);

            async Task<ProdcutItemViewModel> CreateModel()
            {
                    var model = new ProdcutItemViewModel(plant.Id, plant.Title, plant.Content, plant.Price,
                    plant.PlantImages.Select(pi => new ProdcutItemViewModel.ImageItemViewModel(pi.ImageName, _fileService.GetFileUrl(pi.ImageNameInFileSystem, UploadDirectory.Plant))).ToList(),
                    _dataContext.Subcategories!.Where(s => s.Id == plant.SubcategoryId)!.Include(s => s.Category)!.First().Category!.Name, plant.Subcategory.Name,
                  await  _dataContext.PlantTags.Where(pt => pt.PlantId == plant.Id).Select(pt => new ProdcutItemViewModel.TagItemViewModel(pt.Tag.Name, pt.TagId)).ToListAsync(),
                    plant.SubcategoryId
                     );
                return model;

            }

        }
    }
}
