using First_For_Mvc_Project.Areas.Admin.ViewModels.Brand;
using First_For_Mvc_Project.Contracts.File;
using First_For_Mvc_Project.Database;
using First_For_Mvc_Project.Database.Models;
using First_For_Mvc_Project.Services.Concretes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace First_For_Mvc_Project.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("brand")]
    public class BrandController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IFileService _fileService;

        public BrandController(DataContext dataContext, IFileService fileService = null)
        {
            _dataContext = dataContext;
            _fileService = fileService;
        }
        #region List
        [HttpGet("list", Name = "admin-brand-list")]
        public async Task<IActionResult> ListAsync()
        {
            var model = await _dataContext.Brands
                .Select(b => new ListViewModel(b.Id,
                 _fileService.GetFileUrl(b.ImageNameInFileSystem, UploadDirectory.Brand),b.CreatedAt,b.UpdatedAt
              ))
                .ToListAsync();

            return View(model);
        }
        #endregion

        #region Add
        [HttpGet("add", Name = "admin-brand-add")]
        public IActionResult Add()
        {
          
            return View(new AddViewModel());
        }
        [HttpPost("add", Name = "admin-brand-add")]
        public async Task<IActionResult> AddAsync(AddViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
          
            var ImageNameInFileSystem = await _fileService.UploadAsync(model.Photo, UploadDirectory.Brand);

            var brand = CreateBrand();

            await _dataContext.Brands.AddAsync(brand);
            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-brand-list");

            Brand CreateBrand()
            {
                var brand = new Brand
                {
                    ImageNameInFileSystem = ImageNameInFileSystem,
                    ImageName = model.Photo.FileName

                };
                return brand;
            };
        }

        #endregion


        #region Update
        [HttpGet("update/{id}", Name = "admin-brand-update")]
        public async Task<IActionResult> UpdateAsync([FromRoute] int id)
        {
            var brand = await _dataContext.Brands.FirstOrDefaultAsync(b => b.Id == id);

            if (brand is null) return NotFound();
            var model = new UpdateViewModel
            {
                PhotoUrl = _fileService.GetFileUrl(brand.ImageNameInFileSystem, UploadDirectory.Brand),
            };

            return View(model);
        }
        [HttpPost("update/{id}", Name = "admin-brand-update")]
        public async Task<IActionResult> UpdateAsync([FromRoute] int id, UpdateViewModel model)
        {

            var brand = await _dataContext.Brands.FirstOrDefaultAsync(b => b.Id == id);

            if (!ModelState.IsValid) return  GetView();

            if (brand is null) return NotFound();


            if (model.Photo.Name is not null) await UpdateImageAsync();
           


            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-brand-list");


            IActionResult GetView()
            {
                model.PhotoUrl = _fileService.GetFileUrl(brand.ImageNameInFileSystem, UploadDirectory.Brand);
         
                return View(model);
            }

       
            async Task UpdateImageAsync()
            {
                await _fileService.DeleteAsync(brand.ImageNameInFileSystem, UploadDirectory.Brand);
                var ImageNameInFileSystem = await _fileService.UploadAsync(model.Photo, UploadDirectory.Brand);
                brand.ImageName = model.Photo.FileName;
                brand.ImageNameInFileSystem = ImageNameInFileSystem;
            };
           
        }

        #endregion




        #region Delete
        [HttpPost("delete/{id}", Name = "admin-brand-delete")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        {
            var brand = await _dataContext.Brands.FirstOrDefaultAsync(b => b.Id == id);

            if (brand is null) return NotFound();

            await _fileService.DeleteAsync(brand.ImageNameInFileSystem, UploadDirectory.Brand);

            _dataContext.Brands.Remove(brand);

            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-brand-list");

        }
        #endregion

    }
}
