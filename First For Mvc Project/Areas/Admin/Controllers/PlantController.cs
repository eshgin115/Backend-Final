using Pronia.Areas.Admin.ViewModels.Plant;
using Pronia.Contracts.File;
using Pronia.Database;
using Pronia.Database.Models;
using Pronia.Services.Concretes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Pronia.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("plant")]
    public class PlantController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IFileService _fileService;
        private readonly ILogger<PlantController> _logger;
        public PlantController(DataContext dataContext, IFileService fileService, ILogger<PlantController> logger)
        {
            _dataContext = dataContext;
            _fileService = fileService;
            _logger = logger;
        }

        [HttpGet("list", Name = "admin-plant-list")]
        public async Task<IActionResult> ListAsync()
        {
            var model = await CreateModelAsync();

            return View(model);

            async Task<List<ListViewModel>> CreateModelAsync()
            {
                var model = await _dataContext.Plants
                     .Select(p =>
                         new ListViewModel(p.Id, p.Title!, p.Content!, p.Price, p.CreatedAt, p.UpdatedAt,
                          p.PlantImages!.Take(1)!.FirstOrDefault()! != null
                          ? _fileService.GetFileUrl(p.PlantImages!.Take(1)!.FirstOrDefault()!.ImageNameInFileSystem!, UploadDirectory.Plant)
                          : string.Empty,
                          p.PlantTags.Select(pt => new ViewModels.Tag.ListViewModel(pt.Tag.Id, pt.Tag.Name)).ToList(),
                          p.PlantSizes.Select(ps => new ViewModels.Size.ListViewModel(ps.Size.Id, ps.Size.Name)).ToList(),
                          p.PlantColors.Select(pc => new ViewModels.Color.ListViewModel(pc.Color.Id, pc.Color.Name)).ToList(),
                          p.Subcategory.Category.Name,
                          p.Subcategory.Name))



                         .ToListAsync();
                return model;
            };
        }
        [HttpGet("add", Name = "admin-plant-add")]
        public async Task<IActionResult> AddAsync()
        {
            var model = await CreateModelAsync();

            return View(model);


            async Task<AddViewModel> CreateModelAsync()
            {
                var model = new AddViewModel
                {
                    Tags = await _dataContext.Tags
                  .Select(t => new ViewModels.Tag.ListViewModel(t.Id, t.Name))
                  .ToListAsync(),
                    Sizes = await _dataContext.Sizes
                  .Select(s => new ViewModels.Size.ListViewModel(s.Id, s.Name))
                  .ToListAsync(),
                    Colors = await _dataContext.Colors
                  .Select(c => new ViewModels.Color.ListViewModel(c.Id, c.Name))
                  .ToListAsync(),
                    Subcategories = await _dataContext.Subcategories
                    .Select(s => new ViewModels.Subcategory.ListViewModel(s.Id, s.Name))
                    .ToListAsync(),
                };
                return model;
            }
        }

        [HttpPost("add", Name = "admin-plant-add")]
        public async Task<IActionResult> Add(AddViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return await GetViewAsync(model);
            }

            if (!_dataContext.Subcategories.Any(a => a.Id == model.SubcategoryId))
            {
                ModelState.AddModelError(string.Empty, "Subcategory is not found");
                return await GetViewAsync(model);
            }


            foreach (var colorId in model.ColorIds)
            {
                if (!_dataContext.Colors.Any(c => c.Id == colorId))
                {
                    ModelState.AddModelError(string.Empty, "Something went wrong");
                    _logger.LogWarning($"color with id({colorId}) not found in db ");
                    return await GetViewAsync(model);
                }

            }

            foreach (var tagId in model.TagIds)
            {
                if (!_dataContext.Tags.Any(c => c.Id == tagId))
                {
                    ModelState.AddModelError(string.Empty, "Something went wrong");
                    _logger.LogWarning($"tag with id({tagId}) not found in db ");
                    return await GetViewAsync(model);
                }

            }


            foreach (var sizeId in model.SizeIds)
            {
                if (!_dataContext.Sizes.Any(c => c.Id == sizeId))
                {
                    ModelState.AddModelError(string.Empty, "Something went wrong");
                    _logger.LogWarning($"tag with id({sizeId}) not found in db ");
                    return await GetViewAsync(model);
                }

            }


            var plant = await AddPlantAsync();

            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-plant-list");




            async Task<IActionResult> GetViewAsync(AddViewModel model)
            {
                model.Tags = await _dataContext.Tags
                    .Select(t => new ViewModels.Tag.ListViewModel(t.Id, t.Name))
                    .ToListAsync();

                model.Sizes = await _dataContext.Sizes
                     .Select(s => new ViewModels.Size.ListViewModel(s.Id, s.Name))
                     .ToListAsync();
                model.Colors = await _dataContext.Colors
                    .Select(c => new ViewModels.Color.ListViewModel(c.Id, c.Name))
                    .ToListAsync();
                model.Subcategories = await _dataContext.Subcategories
                      .Select(s => new ViewModels.Subcategory.ListViewModel(s.Id, s.Name))
                      .ToListAsync();

                return View(model);
            }

            async Task<Plant> AddPlantAsync()
            {
                var plant = new Plant
                {
                    Title = model.Title,
                    Content = model.Content,
                    Price = model.Price,
                    SubcategoryId = model.SubcategoryId,


                };

                await _dataContext.Plants.AddAsync(plant);

                foreach (var colorId in model.ColorIds)
                {
                    var plantColor = new PlantColor
                    {
                        ColorId = colorId,
                        Plant = plant,
                    };

                    await _dataContext.PlantColors.AddAsync(plantColor);
                }



                foreach (var sizeId in model.SizeIds)
                {
                    var platsize = new PlantSize
                    {
                        SizeId = sizeId,
                        Plant = plant,
                    };

                    await _dataContext.PlantSizes.AddAsync(platsize);
                }


                foreach (var tagId in model.TagIds)
                {
                    var plantTag = new PlantTag
                    {
                        TagId = tagId,
                        Plant = plant,
                    };

                    await _dataContext.PlantTags.AddAsync(plantTag);
                }

                return plant;
            }
        }


        [HttpGet("update/{id}", Name = "admin-plant-update")]
        public async Task<IActionResult> UpdateAsync([FromRoute] int id)
        {
            var plant = await _dataContext.Plants
                .Include(p => p.PlantTags)
                .Include(p => p.PlantSizes)
                .Include(p => p.PlantColors)
                .FirstOrDefaultAsync(b => b.Id == id);
            if (plant is null)
            {
                return NotFound();
            }
            var model = await CreateModel();

            async Task<UpdateViewModel> CreateModel()
            {
                var model = new UpdateViewModel
                {
                    Id = plant.Id,
                    Price = plant.Price,
                    Title = plant.Title,
                    Content = plant.Content,
                    Tags = await _dataContext.Tags
                .Select(t => new ViewModels.Tag.ListViewModel(t.Id, t.Name))
                .ToListAsync(),
                    Sizes = await _dataContext.Sizes
                .Select(s => new ViewModels.Size.ListViewModel(s.Id, s.Name))
                .ToListAsync(),
                    Colors = await _dataContext.Colors
                .Select(c => new ViewModels.Color.ListViewModel(c.Id, c.Name))
                .ToListAsync(),
                    Subcategories = await _dataContext.Subcategories
                  .Select(s => new ViewModels.Subcategory.ListViewModel(s.Id, s.Name))
                  .ToListAsync(),
                    TagIds = plant!.PlantTags!.Select(pt => pt.TagId).ToList(),
                    ColorIds = plant.PlantColors!.Select(plant => plant.ColorId).ToList(),
                    SizeIds = plant.PlantSizes!.Select(pt => pt.SizeId).ToList(),
                    SubcategoryId = plant.SubcategoryId
                };
                return model;
            }
            return View(model);
        }



        [HttpPost("update/{id}", Name = "admin-plant-update")]
        public async Task<IActionResult> UpdateAsync(UpdateViewModel model)
        {
            var plant = await _dataContext.Plants
                .Include(p => p.PlantTags)
                .Include(p => p.PlantSizes)
                .Include(p => p.PlantColors)
                .FirstOrDefaultAsync(p => p.Id == model.Id);
            if (plant is null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return await GetViewAsync(model);
            }

            if (!_dataContext.Subcategories.Any(s => s.Id == model.SubcategoryId))
            {
                ModelState.AddModelError(string.Empty, "subcategory is not found");
                return await GetViewAsync(model);
            }

            foreach (var colorId in model.ColorIds)
            {
                if (!_dataContext.Colors.Any(c => c.Id == colorId))
                {
                    ModelState.AddModelError(string.Empty, "Something went wrong");
                    _logger.LogWarning($"color with id({colorId}) not found in db ");
                    return await GetViewAsync(model);
                }

            }
            foreach (var tagId in model.TagIds)
            {
                if (!_dataContext.Tags.Any(t => t.Id == tagId))
                {
                    ModelState.AddModelError(string.Empty, "Something went wrong");
                    _logger.LogWarning($"tag with id({tagId}) not found in db ");
                    return await GetViewAsync(model);
                }

            }
            foreach (var sizeId in model.SizeIds)
            {
                if (!_dataContext.Sizes.Any(s => s.Id == sizeId))
                {
                    ModelState.AddModelError(string.Empty, "Something went wrong");
                    _logger.LogWarning($"size with id({sizeId}) not found in db ");
                    return await GetViewAsync(model);
                }

            }


            await UpdatePlantAsync();

            return RedirectToRoute("admin-plant-list");




            async Task<IActionResult> GetViewAsync(UpdateViewModel model)
            {

                model.Tags = await _dataContext.Tags
                 .Select(t => new ViewModels.Tag.ListViewModel(t.Id, t.Name))
                 .ToListAsync();
                model.Sizes = await _dataContext.Sizes
                  .Select(s => new ViewModels.Size.ListViewModel(s.Id, s.Name))
                  .ToListAsync();
                model.Colors = await _dataContext.Colors
                  .Select(c => new ViewModels.Color.ListViewModel(c.Id, c.Name))
                  .ToListAsync();
                model.Subcategories = await _dataContext.Subcategories
                    .Select(s => new ViewModels.Subcategory.ListViewModel(s.Id, s.Name))
                    .ToListAsync();
                model.TagIds = plant!.PlantTags!.Select(pt => pt.TagId).ToList();
                model.ColorIds = plant.PlantColors!.Select(plant => plant.ColorId).ToList();
                model.SizeIds = plant.PlantSizes!.Select(pt => pt.SizeId).ToList();

                return View(model);
            }

            async Task UpdatePlantAsync()
            {
                UpdatePLantParametr();

                await UpdatePlantTag();
                await UpdatePlantSize();
                await UpdatePlantColor();

                await _dataContext.SaveChangesAsync();
            }
            void UpdatePLantParametr()
            {

                plant.Title = model.Title;
                plant.SubcategoryId = model.SubcategoryId;
                plant.Price = model.Price;
            };

            async Task UpdatePlantTag()
            {
                var TagsInDb = plant.PlantTags.Select(pt => pt.TagId).ToList();
                var TagsInDbToRemove = TagsInDb.Except(model.TagIds).ToList();
                var TagsToAdd = model.TagIds.Except(TagsInDb).ToList();

                plant.PlantTags.RemoveAll(pt => TagsInDbToRemove.Contains(pt.TagId));

                foreach (var tagId in TagsToAdd)
                {
                    var plantTag = new PlantTag
                    {
                        TagId = tagId,
                        Plant = plant,
                    };

                    await _dataContext.PlantTags.AddAsync(plantTag);
                }
            }




            async Task UpdatePlantSize()
            {
                var SizesInDb = plant.PlantSizes.Select(ps => ps.SizeId).ToList();
                var SizesInDbToRemove = SizesInDb.Except(model.SizeIds).ToList();
                var SizesToAdd = model.SizeIds.Except(SizesInDb).ToList();

                plant.PlantSizes.RemoveAll(pt => SizesInDbToRemove.Contains(pt.SizeId));

                foreach (var sizeId in SizesToAdd)
                {
                    var plantSize = new PlantSize
                    {
                        SizeId = sizeId,
                        Plant = plant,
                    };

                    await _dataContext.PlantSizes.AddAsync(plantSize);
                }

            }



            async Task UpdatePlantColor()
            {
                var ColorsInDb = plant!.PlantColors!.Select(pc => pc.ColorId).ToList();
                var ColorsInDbToRemove = ColorsInDb.Except(model.ColorIds).ToList();
                var ColorsToAdd = model.ColorIds.Except(ColorsInDb).ToList();

                plant.PlantColors.RemoveAll(pc => ColorsInDbToRemove.Contains(pc.ColorId));

                foreach (var colorId in ColorsToAdd)
                {
                    var plantColor = new PlantColor
                    {
                        ColorId = colorId,
                        Plant = plant,
                    };

                    await _dataContext.PlantColors.AddAsync(plantColor);
                }
            }
        }




        #region Delete

        [HttpPost("delete/{id}", Name = "admin-plant-delete")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        {
            var plant = await _dataContext.Plants.FirstOrDefaultAsync(p => p.Id == id);
            if (plant is null)
            {
                return NotFound();
            }


            _dataContext.Plants.Remove(plant);
            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-plant-list");
        }

        #endregion






    }
}
