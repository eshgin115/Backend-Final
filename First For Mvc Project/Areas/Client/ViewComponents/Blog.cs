using First_For_Mvc_Project.Areas.Client.ViewModels.Blog;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pronia.Contracts.File;
using Pronia.Database;
using Pronia.Services.Concretes;

namespace First_For_Mvc_Project.Areas.Client.ViewComponents
{
    public class Blog : ViewComponent
    {
        private readonly DataContext _dataContext;
        private readonly IFileService _fileService;
        public Blog(DataContext dataContext, IFileService fileService)
        {
            _dataContext = dataContext;
            _fileService = fileService;
        }
        public async Task<IViewComponentResult> InvokeAsync(int? tagId = null, string? searchQuery = null)
        {
            var productsQuery = _dataContext.Blogs.AsQueryable();

            if (!String.IsNullOrEmpty(searchQuery))
            {
                productsQuery = productsQuery
                    .Where(p => p.Title!.StartsWith(searchQuery))
                    .Take(8);
            }
            else if (tagId != null)
            {
                productsQuery = productsQuery
                  .Where(p => p.BlogTags.Any(pt => pt.TagId == tagId))
                  .Take(8);

            }
            else
            {
                productsQuery = productsQuery
                    .Take(8);
            }

            var products = await productsQuery.Select(p => new ListItemViewModel(p.Id, p.Title, 
                  p.BlogImages!.Take(1)!.FirstOrDefault()! != null
                       ? _fileService.GetFileUrl(p.BlogImages!.Take(1)!.FirstOrDefault()!.ImageNameInFileSystem!, UploadDirectory.BlogImage)
                       : string.Empty,
                  p.BlogImages!.Skip(1).Take(1)!.FirstOrDefault()! != null
                       ? _fileService.GetFileUrl(p.BlogImages!.Skip(1)!.Take(1)!.FirstOrDefault()!.ImageNameInFileSystem!, UploadDirectory.BlogImage)
                       : string.Empty,
                   p.BlogVideos!.Take(1)!.FirstOrDefault()! != null
                       ? p.BlogVideos!.Take(1)!.FirstOrDefault()!.VideoURLFromBrauser!
                       : string.Empty,
                   p.Admin!.FirstName!,
                   p.CreatedAt,
                   p.Content
                   


                       )
                       ).ToListAsync();

            return View(products);
        }
    }
}
