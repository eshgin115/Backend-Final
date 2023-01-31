using Pronia.Areas.Admin.ViewModels.FeedBack;
using Pronia.Contracts.File;
using Pronia.Database;
using Pronia.Services.Concretes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Pronia.Areas.Client.ViewComponents
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
