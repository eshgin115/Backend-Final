using Pronia.Areas.Admin.ViewModels.PaymentBenefits;
using Pronia.Contracts.File;
using Pronia.Database;
using Pronia.Services.Concretes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Pronia.Areas.Client.ViewComponents
{
    public class PaymentBenefits : ViewComponent
    {
        private readonly DataContext _dataContext;
        private readonly IFileService _fileService;
        public PaymentBenefits(DataContext dataContext, IFileService fileService)
        {
            _dataContext = dataContext;
            _fileService = fileService;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = await _dataContext.PaymentBenefits
                        .OrderBy(p => p.Order)
                        .Take(3)
                        .Select(p =>
                        new ListViewModel(p.Id, p.Title!, p.Order, p.Content!,
                        _fileService.GetFileUrl(p.ImageNameInFileSystem, UploadDirectory.Paymentbenefits)))
                        .ToListAsync();

            return View(model);
           
        }
    }
}
