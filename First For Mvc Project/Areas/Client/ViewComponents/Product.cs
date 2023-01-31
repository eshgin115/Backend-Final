using Pronia.Areas.Client.ViewModels.Product;
using Pronia.Contracts.File;
using Pronia.Contracts.Product;
using Pronia.Database;
using Pronia.Services.Concretes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Pronia.Areas.Client.ViewComponents
{
    public class Product : ViewComponent
    {
        private readonly DataContext _dataContext;
        private readonly IFileService _fileService;
        public Product(DataContext dataContext, IFileService fileService)
        {
            _dataContext = dataContext;
            _fileService = fileService;
        }
        public async Task<IViewComponentResult> InvokeAsync
            (string? slide = null,
            int? subcategoryId = null,
            int? colorId = null,
            int? tagId = null,
            string? searchQuery = null,
            int? minPrice = null,
            int? maxPrice = null)
        {
            var productsQuery = _dataContext.Plants.AsQueryable();
            

            if (slide == Manage.NEW_PRODUCT)
            {
                productsQuery = productsQuery
                    .OrderByDescending(p => p.CreatedAt)
                    .Take(4);
            }
            else if (minPrice != null && maxPrice != null)
            {
                productsQuery = productsQuery
                   .Where(p => p.Price! >= minPrice && p.Price <= maxPrice)
                   .Take(8);
            }
            else if (!String.IsNullOrEmpty(searchQuery))
            {
                productsQuery = productsQuery
                    .Where(p => p.Title!.StartsWith(searchQuery))
                    .Take(8);
            }
            else if (colorId != null)
            {
                productsQuery = productsQuery
                 .Where(p => p.PlantColors.Any(pc => pc.ColorId == colorId))
                 .Take(8);

            }
            else if (tagId != null)
            {
                productsQuery = productsQuery
                  .Where(p => p.PlantTags.Any(pt => pt.TagId == tagId))
                  .Take(8);

            }
            else if (subcategoryId != null)
            {
                productsQuery = productsQuery
                  .Where(p => p.SubcategoryId == subcategoryId)
                  .Take(4);
            }
            else if (slide == Manage.Latest_PRODUCT)
            {
                productsQuery = productsQuery
                    .OrderBy(p => p.CreatedAt)
                    .Take(8);
            }
            else if (slide == Manage.TOP_PRODUCT)
            {
                productsQuery = productsQuery
                   .OrderByDescending(
                        p => p.OrderProducts.Where(
                            op => op.Order.Status == Contracts.Order.Status.Completed).Count())
                   .Take(5);

            }
            else
            {
                productsQuery = productsQuery
                    .OrderBy(p => p.Price)
                    .Take(8);
            }

            var products = await productsQuery.Select(p => new ListViewModel(p.Id, p.Title, p.Price,
                  p.PlantImages!.Take(1)!.FirstOrDefault()! != null
                       ? _fileService.GetFileUrl(p.PlantImages!.Take(1)!.FirstOrDefault()!.ImageNameInFileSystem!, UploadDirectory.Plant)
                       : string.Empty,
                  p.PlantImages!.Skip(1).Take(1)!.FirstOrDefault()! != null
                       ? _fileService.GetFileUrl(p.PlantImages!.Skip(1)!.Take(1)!.FirstOrDefault()!.ImageNameInFileSystem!, UploadDirectory.Plant)
                       : string.Empty)

                       ).ToListAsync();

            return View(products);
        }
    }
}
