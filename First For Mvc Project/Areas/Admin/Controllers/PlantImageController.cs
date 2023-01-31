using Pronia.Areas.Admin.ViewModels.PlantImage;
using Pronia.Contracts.File;
using Pronia.Database;
using Pronia.Database.Models;
using Pronia.Services.Concretes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Pronia.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/plants")]
    public class PlantImageController : Controller
    {

        private readonly DataContext _dataContext;
        private readonly IFileService _fileService;

        public PlantImageController(
            DataContext dataContext,
            IFileService fileService)
        {
            _dataContext = dataContext;
            _fileService = fileService;
        }

        #region List

        [HttpGet("{plantId}/image/list", Name = "admin-plant-image-list")]
        public async Task<IActionResult> ListAsync([FromRoute] int plantId)
        {
            var plant = await _dataContext.Plants
                .Include(p => p.PlantImages)
                .FirstOrDefaultAsync(p => p.Id == plantId );

            if (plant is null) return NotFound();

            var model = new BlogImagesViewModel { PlantId = plant.Id };

            model.Images = plant.PlantImages!.Select(pi => new BlogImagesViewModel.ListItem
            {
                Id = pi.Id,
                ImageUrL = _fileService.GetFileUrl(pi.ImageNameInFileSystem, UploadDirectory.Plant),
                CreatedAt = pi.CreatedAt,
                Order = pi.Order

            }).ToList();

            return View(model);
        }

        #endregion

        #region Add

        [HttpGet("{plantId}/image/add", Name = "admin-plant-image-add")]
        public async Task<IActionResult> AddAsync()
        {
            return View(new AddViewModel());
        }

        [HttpPost("{plantId}/image/add", Name = "admin-plant-image-add")]
        public async Task<IActionResult> AddAsync([FromRoute] int plantId, [FromForm] AddViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var plant = await _dataContext.Plants.FirstOrDefaultAsync(b => b.Id == plantId);


            if (plant is null) return NotFound();

            var imageNameInSystem = await _fileService.UploadAsync(model.Image, UploadDirectory.Plant);

            var plantImage = CreatePlantImage();

            await _dataContext.PlantImages.AddAsync(plantImage);

            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-plant-image-list", new { plantId = plantId });

            PlantImage CreatePlantImage()
            {
                var plantImage = new PlantImage
                {
                    Plant = plant,
                    ImageName = model.Image.FileName,
                    ImageNameInFileSystem = imageNameInSystem,
                    Order = (int)model.Order,
                };
                return plantImage;

            };
        }

        #endregion

        #region Update
        [HttpGet("{plantId}/image/{plantImageId}/update", Name = "admin-plant-image-update")]
        public async Task<IActionResult> UpdateAsync(int plantId, int plantImageId)
        {
            var plantImage = await _dataContext.PlantImages
               .FirstOrDefaultAsync(bi => bi.Id == plantImageId && bi.PlantId == plantId);
            if (plantImage is null) return NotFound();

            var model = new UpdateViewModel
            {
                ImageUrL = _fileService.GetFileUrl(plantImage.ImageNameInFileSystem, UploadDirectory.Plant),
                Order = plantImage.Order,
            };

            return View(model);
        }
        [HttpPost("{plantId}/image/{plantImageId}/update", Name = "admin-plant-image-update")]
        public async Task<IActionResult> UpdateAsync(int plantId, int plantImageId, [FromForm] UpdateViewModel model)
        {
            var plantImage = await _dataContext.PlantImages
                .FirstOrDefaultAsync(bi => bi.Id == plantImageId && bi.PlantId == plantId);




            if (plantImage is null) return NotFound();





            if (!(model.Image == null)) await UpdateImageAsync();
         

            plantImage.Order = (int)model.Order;






            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-plant-image-update", new { plantId = plantId });

             async Task UpdateImageAsync()
             {

                await _fileService.DeleteAsync(plantImage.ImageNameInFileSystem, UploadDirectory.Plant);

                var imageNameInSystem = await _fileService.UploadAsync(model.Image, UploadDirectory.Plant);
                plantImage.ImageNameInFileSystem = imageNameInSystem;
                plantImage.ImageName = model.Image.FileName;
             };
        }

        #endregion

        #region Delete

        [HttpPost("{plantId}/image/{plantImageId}/delete", Name = "admin-plant-image-delete")]
        public async Task<IActionResult> DeleteAsync(int plantId, int plantImageId)
        {
            var plantImage = await _dataContext.PlantImages
                .FirstOrDefaultAsync(bi => bi.Id == plantImageId && bi.PlantId == plantId);


            if (plantImage is null) return NotFound();

            await _fileService.DeleteAsync(plantImage.ImageNameInFileSystem, UploadDirectory.Plant);

            _dataContext.PlantImages.Remove(plantImage);
            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-plant-image-list", new { plantId = plantId });
        }

        #endregion
    }
}
