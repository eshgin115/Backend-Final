using Pronia.Areas.Admin.ViewModels.PaymentBenefits;
using Pronia.Contracts.File;
using Pronia.Database;
using Pronia.Database.Models;
using Pronia.Services.Concretes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Pronia.Areas.Admin.Controllers
{

    [Area("admin")]
    [Route("Paymentbenefits")]
    public class PaymentbenefitsController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IFileService _fileService;
        public PaymentbenefitsController(DataContext dataContext, IFileService fileService)
        {
            _dataContext = dataContext;
            _fileService = fileService;
        }

        [HttpGet("list", Name = "admin-Paymentbenefits-list")]
        public async Task<IActionResult> ListAsync()
        {
            var model = await _dataContext.PaymentBenefits.Select(p =>
                          new ListViewModel(p.Id, p.Title!, p.Order, p.Content!,
                          _fileService.GetFileUrl(p.ImageNameInFileSystem, UploadDirectory.Paymentbenefits)))
                          .ToListAsync();

            return View(model);
        }
        #region Add
        [HttpGet("add", Name = "admin-Paymentbenefits-add")]
        public IActionResult Add()
        {
            return View(new AddViewModel());
        }
        [HttpPost("add", Name = "admin-Paymentbenefits-add")]
        public async Task<IActionResult> Add(AddViewModel model)
        {
            if (!ModelState.IsValid) return View(model);


            if (_dataContext.PaymentBenefits.Any(p => p.Order == model.Order))
            {
                ModelState.AddModelError(String.Empty, "this order using");
                return View(model);

            }
            var imageNameInSystem = await _fileService.UploadAsync(model!.Image!, UploadDirectory.Paymentbenefits);

            await AddPaymentBenefits(model.Image!.FileName, imageNameInSystem);


            return RedirectToRoute("admin-Paymentbenefits-list");


            async Task AddPaymentBenefits(string imageName, string imageNameInSystem)
            {
                var paymentbenefits = CreatePaymentBenefits();

                await _dataContext.PaymentBenefits.AddAsync(paymentbenefits);

                await _dataContext.SaveChangesAsync();


                PaymentBenefits CreatePaymentBenefits()
                {
                    var paymentbenefits = new PaymentBenefits
                    {
                        Content = model.Content,
                        Title = model.Title,
                        Order = model.Order,
                        ImageName = imageName,
                        ImageNameInFileSystem = imageNameInSystem
                    };
                    return paymentbenefits;
                };
            }
        }

        #endregion

        #region Update
        [HttpGet("update/{id}", Name = "admin-Paymentbenefits-update")]
        public async Task<IActionResult> UpdateAsync([FromRoute] int id)
        {
            var paymentBenefits = await _dataContext.PaymentBenefits.FirstOrDefaultAsync(b => b.Id == id);


            if (paymentBenefits is null) return NotFound();


            var model = CreateModel();

            return View(model);

            UpdateViewModel CreateModel()
            {
                var model = new UpdateViewModel
                {
                    Id = paymentBenefits.Id,
                    Content = paymentBenefits.Content,
                    Title = paymentBenefits.Title,
                    Order = paymentBenefits.Order,
                    ImageURL = _fileService.GetFileUrl(paymentBenefits.ImageNameInFileSystem, UploadDirectory.Paymentbenefits)
                };
                return model;
            }
        }

        [HttpPost("update/{id}", Name = "admin-Paymentbenefits-update")]
        public async Task<IActionResult> UpdateAsync(UpdateViewModel model)
        {
            var paymentBenefits = await _dataContext.PaymentBenefits.FirstOrDefaultAsync(b => b.Id == model.Id);


            if (paymentBenefits is null) return NotFound();



            if (!ModelState.IsValid) return View(model);


            if (_dataContext.PaymentBenefits.Any(p => p.Order == model.Order) && !(model.Order == paymentBenefits.Order))
            {
                ModelState.AddModelError(String.Empty, "this order using");
                return View(model);

            }



            await _fileService.DeleteAsync(paymentBenefits.ImageNameInFileSystem, UploadDirectory.Paymentbenefits);

            var imageFileNameInSystem = await _fileService.UploadAsync(model.Image!, UploadDirectory.Paymentbenefits);

            await UpdatePaymentBenefitsAsync(model.Image!.FileName, imageFileNameInSystem);

            return RedirectToRoute("admin-Paymentbenefits-list");





            async Task UpdatePaymentBenefitsAsync(string imageName, string imageNameInFileSystem)
            {
                paymentBenefits.Title = model.Title;
                paymentBenefits.Order = model.Order;
                paymentBenefits.Content = model.Content;
                paymentBenefits.ImageName = imageName;
                paymentBenefits.ImageNameInFileSystem = imageNameInFileSystem;




                await _dataContext.SaveChangesAsync();
            }
        }
        #endregion


        [HttpPost("delete/{id}", Name = "admin-Paymentbenefits-delete")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        {
            var paymentBenefits = await _dataContext.PaymentBenefits.FirstOrDefaultAsync(b => b.Id == id);


            if (paymentBenefits is null) return NotFound();

            await _fileService.DeleteAsync(paymentBenefits.ImageNameInFileSystem, UploadDirectory.Slider);

            _dataContext.PaymentBenefits.Remove(paymentBenefits);
            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-paymentBenefits-list");
        }
    }
}
