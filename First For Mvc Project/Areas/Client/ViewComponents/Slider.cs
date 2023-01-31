using First_For_Mvc_Project.Areas.Client.ViewModels.Slider;
using First_For_Mvc_Project.Contracts.File;
using First_For_Mvc_Project.Database;
using First_For_Mvc_Project.Services.Concretes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace First_For_Mvc_Project.Areas.Client.ViewComponents
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
