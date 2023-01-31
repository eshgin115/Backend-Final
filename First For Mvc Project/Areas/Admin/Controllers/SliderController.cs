using Pronia.Areas.Admin.ViewModels.Slider;
using Pronia.Contracts.File;
using Pronia.Database;
using Pronia.Database.Models;
using Pronia.Services.Concretes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace Pronia.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("slider")]
    [Authorize]
    public class SliderController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IFileService _fileService;

        public SliderController(
            DataContext dataContext,
            IFileService fileService)
        {
            _dataContext = dataContext;
            _fileService = fileService;
        }
        [HttpGet("list", Name = "admin-slider-list")]
        public async Task<IActionResult> ListAsync()
        {
            var model = await _dataContext.Sliders.Select(s =>
                          new ListViewModel(s.Id, s.Offer!, s.Tittle, s.Content!,
                          _fileService.GetFileUrl(s.ImageNameInFileSystem, UploadDirectory.Slider),
                          s.ButtonName!, s.ButtonURL!, s.Order))
                          .ToListAsync();

            return View(model);
        }



        #region Add
        [HttpGet("add", Name = "admin-slider-add")]
        public IActionResult Add()
        {
            return View(new AddViewModel());
        }
        [HttpPost("add", Name = "admin-slider-add")]
        public async Task<IActionResult> Add(AddViewModel model)
        {
            if (!ModelState.IsValid) return View(model);


            if (_dataContext.Sliders.Any(n => n.Order == model.Order))
            {
                ModelState.AddModelError(String.Empty, "this order using");
                return View(model);

            }
            var imageNameInSystem = await _fileService.UploadAsync(model!.Image!, UploadDirectory.Slider);

            await AddSlider(model.Image!.FileName, imageNameInSystem);


            return RedirectToRoute("admin-slider-list");


            async Task AddSlider(string imageName, string imageNameInSystem)
            {
                var mainslider = CreateSlider();

                await _dataContext.Sliders.AddAsync(mainslider);

                await _dataContext.SaveChangesAsync();

                Slider CreateSlider()
                {
                    var mainslider = new Slider
                    {
                        ButtonName = model.ButtonName,
                        Offer = model.Offer,
                        Tittle = model.Tittle,
                        Content = model.Content,
                        ButtonURL = model.ButtonURL,
                        Order = model.Order,
                        ImageName = imageName,
                        ImageNameInFileSystem = imageNameInSystem
                    };
                    return mainslider;
                };
            }
        }
        #endregion




        [HttpGet("update/{id}", Name = "admin-slider-update")]
        public async Task<IActionResult> UpdateAsync([FromRoute] int id)
        {
            var slider = await _dataContext.Sliders.FirstOrDefaultAsync(b => b.Id == id);


            if (slider is null) return NotFound();


            var model = CreateModel();

            return View(model);


            UpdateViewModel CreateModel()
            {
                var model = new UpdateViewModel
                {
                    Id = slider.Id,
                    ButtonName = slider.ButtonName,
                    ButtonURL = slider.ButtonURL,
                    Content = slider.Content,
                    Offer = slider.Offer,
                    Tittle = slider.Tittle,
                    Order = slider.Order,
                    ImageURL = _fileService.GetFileUrl(slider.ImageNameInFileSystem, UploadDirectory.Slider)
                };
                return model;
            }
        }

        [HttpPost("update/{id}", Name = "admin-slider-update")]
        public async Task<IActionResult> UpdateAsync(UpdateViewModel model)
        {
            var slider = await _dataContext.Sliders.FirstOrDefaultAsync(b => b.Id == model.Id);


            if (slider is null) return NotFound();



            if (!ModelState.IsValid) return View(model);
            if (_dataContext.Sliders.Any(s => s.Order == model.Order) && !(model.Order == slider.Order))
            {
                ModelState.AddModelError(String.Empty, "this order using");
                return View(model);

            }


            await _fileService.DeleteAsync(slider.ImageNameInFileSystem, UploadDirectory.Slider);

            var imageFileNameInSystem = await _fileService.UploadAsync(model.Image!, UploadDirectory.Slider);

            await UpdateSliderAsync(model.Image.FileName, imageFileNameInSystem);

            return RedirectToRoute("admin-slider-list");



            async Task UpdateSliderAsync(string imageName, string imageNameInFileSystem)
            {
                slider.Tittle = model.Tittle;
                slider.Offer = model.Offer;
                slider.ButtonURL = model.ButtonURL;
                slider.ButtonName = model.ButtonName;
                slider.Order = model.Order;
                slider.Content = model.Content;
                slider.ImageName = imageName;
                slider.ImageNameInFileSystem = imageNameInFileSystem;




                await _dataContext.SaveChangesAsync();
            }
        }
        [HttpPost("delete/{id}", Name = "admin-slider-delete")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        {
            var slider = await _dataContext.Sliders.FirstOrDefaultAsync(b => b.Id == id);


            if (slider is null) return NotFound();

            await _fileService.DeleteAsync(slider.ImageNameInFileSystem, UploadDirectory.Slider);

            _dataContext.Sliders.Remove(slider);
            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-slider-list");
        }
    }
}
