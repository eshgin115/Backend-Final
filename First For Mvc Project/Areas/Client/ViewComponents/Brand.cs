using First_For_Mvc_Project.Areas.Admin.ViewModels.Brand;
using First_For_Mvc_Project.Contracts.File;
using First_For_Mvc_Project.Database;
using First_For_Mvc_Project.Services.Concretes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace First_For_Mvc_Project.Areas.Client.ViewComponents
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
