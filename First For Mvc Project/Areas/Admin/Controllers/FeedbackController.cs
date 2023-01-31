using Pronia.Areas.Admin.ViewModels.FeedBack;
using Pronia.Contracts.File;
using Pronia.Database;
using Pronia.Database.Models;
using Pronia.Services.Concretes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Pronia.Areas.Admin.Controllers
{

    [Area("admin")]
    [Route("feedback")]
    public class FeedbackController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IFileService _fileService;

        public FeedbackController(DataContext dataContext, IFileService fileService = null)
        {
            _dataContext = dataContext;
            _fileService = fileService;
        }
        #region List
        [HttpGet("list", Name = "admin-feedback-list")]
        public async Task<IActionResult> ListAsync()
        {
            var model = await _dataContext.Feedbacks
                .Select(f => new ListViewModel(f.Id,
                 _fileService.GetFileUrl(f.ImageNameInFileSystem, UploadDirectory.FeedBack),
              f.Name, f.Role.Name, f.CreatedAt, f.UpdatedAt))
                .ToListAsync();

            return View(model);
        }

        #endregion

        #region Add
        [HttpGet("add", Name = "admin-feedback-add")]
        public async Task<IActionResult> AddAsync()
        {
            var model = new AddViewModel
            {
                Roles = await _dataContext.Roles.Select(r => new AddViewModel.ItemViewModel(r.Id, r.Name)).ToListAsync(),
            };
            return View(model);
        }
        [HttpPost("add", Name = "admin-feedback-add")]
        public async Task<IActionResult> AddAsync(AddViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            if (!_dataContext.Roles.Any(r => r.Id == model.RoleId))
            {
                model.Roles =
                await _dataContext.Roles.Select(r => new AddViewModel.ItemViewModel(r.Id, r.Name)).ToListAsync();
                return View(model);

            }

            var feedBackPpImageNameInFileSystem = await _fileService.UploadAsync(model.ProfilePhoto, UploadDirectory.FeedBack);

            var feedBack = new Feedback
            {
                Name = model.Name,
                ImageName = model.ProfilePhoto.FileName,
                ImageNameInFileSystem = feedBackPpImageNameInFileSystem,
                RoleId = model.RoleId,

                Content = model.Content
            };

            await _dataContext.Feedbacks.AddAsync(feedBack);
            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-feedback-list");
        }
        #endregion


        #region Update
        [HttpGet("update/{id}", Name = "admin-feedback-update")]
        public async Task<IActionResult> UpdateAsync([FromRoute] int id)
        {
            var feedBack = await _dataContext.Feedbacks.FirstOrDefaultAsync(fb => fb.Id == id);

            if (feedBack is null) return NotFound();


            var model = await CreateModelAsync();

            return View(model);
            async Task<UpdateViewModel> CreateModelAsync()
            {
                var model = new UpdateViewModel
                {
                    Name = feedBack.Name,
                    Content = feedBack.Content,
                    RoleId = feedBack.RoleId,
                    ProfilePhotoUrl = _fileService.GetFileUrl(feedBack.ImageNameInFileSystem, UploadDirectory.FeedBack),
                    Roles = await _dataContext.Roles.Select(r => new UpdateViewModel.ItemViewModel(r.Id, r.Name)).ToListAsync(),

                };
                return model
            }
        }
        [HttpPost("update/{id}", Name = "admin-feedback-update")]
        public async Task<IActionResult> UpdateAsync([FromRoute] int id, UpdateViewModel model)
        {

            var feedBack = await _dataContext.Feedbacks.FirstOrDefaultAsync(fb => fb.Id == id);

            if (!ModelState.IsValid) return await GetViewAsync();

            if (feedBack is null) return NotFound();
            if (!_dataContext.Roles.Any(r => r.Id == model.RoleId)) return await GetViewAsync();


            if (model.ProfilePhoto is not null) await UpdateImageAsync();
         

            UpdateFeedBack();

            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-feedback-list");


            async Task<IActionResult> GetViewAsync()
            {
                model.ProfilePhotoUrl = _fileService.GetFileUrl(feedBack.ImageNameInFileSystem, UploadDirectory.FeedBack);
                model.Roles =
                    await _dataContext.Roles.Select(r => new UpdateViewModel.ItemViewModel(r.Id, r.Name)).ToListAsync();
                return View(model);
            }

            void UpdateFeedBack()
            {
                feedBack.Name = model.Name;
                feedBack.RoleId = model.RoleId;
                feedBack.Content = model.Content;
            }

            async Task UpdateImageAsync()
            {
                await _fileService.DeleteAsync(feedBack.ImageNameInFileSystem, UploadDirectory.FeedBack);
                var feedBackPpImageNameInFileSystem = await _fileService.UploadAsync(model.ProfilePhoto, UploadDirectory.FeedBack);
                feedBack.ImageName = model.ProfilePhoto.FileName;
                feedBack.ImageNameInFileSystem = feedBackPpImageNameInFileSystem;
            }
        }
        #endregion


        #region Delete
        [HttpPost("delete/{id}", Name = "admin-feedback-delete")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        {
            var feedBack = await _dataContext.Feedbacks.FirstOrDefaultAsync(fb => fb.Id == id);

            if (feedBack is null) return NotFound();

            await _fileService.DeleteAsync(feedBack.ImageNameInFileSystem, UploadDirectory.FeedBack);

            _dataContext.Feedbacks.Remove(feedBack);

            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-feedback-list");

        }

        #endregion
    }
}
