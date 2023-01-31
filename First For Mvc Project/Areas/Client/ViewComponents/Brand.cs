using Pronia.Areas.Admin.ViewModels.Brand;
using Pronia.Contracts.File;
using Pronia.Database;
using Pronia.Services.Concretes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Pronia.Areas.Client.ViewComponents
{
    public class Brand :ViewComponent
    {
        private readonly DataContext _dataContext;
        private readonly IFileService _fileService;
        public Brand(DataContext dataContext, IFileService fileService)
        {
            _dataContext = dataContext;
            _fileService = fileService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = await _dataContext.Brands
                            .Select(b => new ListViewModel(b.Id,
                             _fileService.GetFileUrl(b.ImageNameInFileSystem, UploadDirectory.Brand)
                          ))
                            .ToListAsync();

            return View(model);

        }
    }
}
