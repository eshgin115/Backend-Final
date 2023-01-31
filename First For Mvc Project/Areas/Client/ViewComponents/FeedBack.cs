using First_For_Mvc_Project.Areas.Admin.ViewModels.FeedBack;
using First_For_Mvc_Project.Contracts.File;
using First_For_Mvc_Project.Database;
using First_For_Mvc_Project.Services.Concretes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace First_For_Mvc_Project.Areas.Client.ViewComponents
{
    public class FeedBack : ViewComponent
    {
        private readonly DataContext _dataContext;
        private readonly IFileService _fileService;
        public FeedBack(DataContext dataContext, IFileService fileService)
        {
            _dataContext = dataContext;
            _fileService = fileService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = await _dataContext.Feedbacks
                            .Select(f => new ListViewModel(f.Id,
                             _fileService.GetFileUrl(f.ImageNameInFileSystem, UploadDirectory.FeedBack),
                          f.Name, f.Role.Name,f.Content))
                            .ToListAsync();

            return View(model);

        }
    }
}
