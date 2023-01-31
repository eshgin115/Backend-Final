using Pronia.Areas.Client.ViewModels.Slider;
using Pronia.Contracts.File;
using Pronia.Database;
using Pronia.Services.Concretes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Pronia.Areas.Client.ViewComponents
{
    public class Slider : ViewComponent
    {
        private readonly DataContext _dataContext;
        private readonly IFileService _fileService;
        public Slider(DataContext dataContext, IFileService fileService)
        {
            _dataContext = dataContext;
            _fileService = fileService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = await _dataContext.Sliders
                         .OrderBy(s => s.Order)
                         .Select(s =>
                          new ListViewModel( s.Offer!, s.Tittle, s.Content!,
                          _fileService.GetFileUrl(s.ImageNameInFileSystem, UploadDirectory.Slider), s.ButtonName!, s.ButtonURL!))
                          .ToListAsync();

           
            return View(model);
        }
    }
}
