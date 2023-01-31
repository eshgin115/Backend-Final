using First_For_Mvc_Project.Areas.Admin.ViewModels.AboutComponent;
using First_For_Mvc_Project.Areas.Client.ViewModels.Product;
using First_For_Mvc_Project.Contracts.File;
using First_For_Mvc_Project.Database;
using First_For_Mvc_Project.Services.Concretes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace First_For_Mvc_Project.Areas.Client.Controllers
{
    [Area("client")]
    [Route("home")]
    public class HomeController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IFileService _fileService;
        public HomeController(DataContext dataContext, IFileService fileService)
        {
            _dataContext = dataContext;
            _fileService = fileService;
        }
        [HttpGet("~/", Name = "client-home-index")]
        [HttpGet("index")]
        public async Task<IActionResult> IndexAsync()
        {
            return View();
        }
        [HttpGet("modal/{id}", Name = "client-home-modal")]
        public async Task<IActionResult> ModalAsync(int Id)
        {

            var plant = await _dataContext.Plants
                                        .Include(p=>p.PlantImages)
                                        .Include(p=>p.PlantSizes)
                                        .Include(p=>p.PlantColors)
                                        .FirstOrDefaultAsync(p => p.Id == Id);

            if (plant is null) return NotFound();


            var model = await CreateModel();

            return PartialView("~/Areas/Client/Views/Shared/Partials/GetModal/_ProductModalPartial.cshtml", model);




            async Task<ModalViewModel> CreateModel()
            {
              model=  new ModalViewModel(plant.Id, plant.Title, plant.Content, plant.Price,
                 plant.PlantImages!.Take(1).FirstOrDefault() != null
                 ? _fileService.GetFileUrl(plant.PlantImages.Take(1).FirstOrDefault()!.ImageNameInFileSystem, UploadDirectory.Plant)
                 : String.Empty,
              await  _dataContext.PlantSizes.Include(ps => ps.Size).Where(ps => ps.PlantId == plant.Id)
                 .Select(ps => new Admin.ViewModels.Size.ListViewModel(ps.SizeId, ps.Size.Name)).ToListAsync(),
              await _dataContext.PlantColors.Include(pc => pc.Color).Where(pc => pc.PlantId == plant.Id)
                 .Select(pc => new Admin.ViewModels.Color.ListViewModel(pc.ColorId, pc.Color.Name)).ToListAsync()
                 );
                return model;
            }

        }
        [HttpGet("about", Name = "client-home-about")]
        public async Task<IActionResult> AboutAsync()
        {
            var model = new UpdateViewModel
            {
                Content = (await _dataContext.AboutComponents.SingleOrDefaultAsync())!.Content,
            };
            return View(model);
        }
        
    }
}
