using Pronia.Areas.Admin.ViewModels.BlogVideo;
using Pronia.Contracts.File;
using Pronia.Database;
using Pronia.Database.Models;
using Pronia.Services.Concretes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Pronia.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/blogs")]
    public class BlogVideoController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IFileService _fileService;

        public BlogVideoController(
            DataContext dataContext,
            IFileService fileService)
        {
            _dataContext = dataContext;
            _fileService = fileService;
        }
        #region List

        [HttpGet("{blogId}/video/list", Name = "admin-blog-video-list")]
        public async Task<IActionResult> ListAsync([FromRoute] int blogId)
        {
            var blog = await _dataContext.Blogs
                .Include(p => p.BlogVideos)
                .FirstOrDefaultAsync(p => p.Id == blogId);



            if (blog is null) return NotFound();

            var model = new BlogVideosViewModel { BlogId = blog.Id };

            model.videos = blog.BlogVideos!.Select(pi => new BlogVideosViewModel.ListItem
            {
                Id = pi.Id,
                VideoUrL = _fileService.GetFileUrl(pi.VideoNameInFileSystem, UploadDirectory.BlogVideo),

            }).ToList();

            return View(model);
        }
        #endregion


        #region Add

        [HttpGet("{blogId}/video/add", Name = "admin-blog-video-add")]
        public IActionResult Add()
        {
            return View(new AddViewModel());
        }

        [HttpPost("{blogId}/video/add", Name = "admin-blog-video-add")]
        public async Task<IActionResult> AddAsync([FromRoute] int blogId, [FromForm] AddViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var blog = await _dataContext.Blogs.FirstOrDefaultAsync(b => b.Id == blogId);


            if (blog is null) return NotFound();

            var videoNameInSystem = await _fileService.UploadAsync(model.Video, UploadDirectory.BlogVideo);

            var blogVideo = CreateBlogVideo();

            await _dataContext.BlogVideos.AddAsync(blogVideo);

            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-blog-video-list", new { blogId = blogId });


            BlogVideo CreateBlogVideo()
            {
                var blogVideo = new BlogVideo
                {
                    Blog = blog,
                    VideoName = model.Video.FileName,
                    VideoNameInFileSystem = videoNameInSystem,
                };
                return blogVideo;
            };
        }

        #endregion


        #region Update
        [HttpGet("{blogId}/video/{blogVideoId}/update", Name = "admin-blog-video-update")]
        public async Task<IActionResult> UpdateAsync(int blogId, int blogVideoId)
        {
            var blogVideo = await _dataContext.BlogVideos
               .FirstOrDefaultAsync(bi => bi.Id == blogVideoId && bi.BlogId == blogId);


            if (blogVideo is null) return NotFound();

            var model = new UpdateViewModel
            {
                VideoUrL = _fileService.GetFileUrl(blogVideo.VideoNameInFileSystem, UploadDirectory.BlogVideo),
            };

            return View(model);
        }
        [HttpPost("{blogId}/video/{blogVideoId}/update", Name = "admin-blog-video-update")]
        public async Task<IActionResult> UpdateAsync(int blogId, int blogVideoId, [FromForm] UpdateViewModel model)
        {
            var blogVideo = await _dataContext.BlogVideos
                .FirstOrDefaultAsync(bi => bi.Id == blogVideoId && bi.BlogId == blogId);


            if (blogVideo is null) return NotFound();

            if (!(model.Video == null)) await UpdateVideoAsync();


            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-blog-video-update", new { blogId = blogId });

            async Task UpdateVideoAsync()
            {
                await _fileService.DeleteAsync(blogVideo.VideoNameInFileSystem, UploadDirectory.BlogVideo);

                var videoNameInSystem = await _fileService.UploadAsync(model.Video, UploadDirectory.BlogImage);
                blogVideo.VideoNameInFileSystem = videoNameInSystem;
                blogVideo.VideoName = model.Video.FileName;
            }
        }

        #endregion


        #region Delete

        [HttpPost("{blogId}/video/{blogVideoId}/delete", Name = "admin-blog-video-delete")]
        public async Task<IActionResult> DeleteAsync(int blogId, int blogVideoId)
        {
            var blogVideo = await _dataContext.BlogVideos
                .FirstOrDefaultAsync(bi => bi.Id == blogVideoId && bi.BlogId == blogId);


            if (blogVideo is null) return NotFound();

            await _fileService.DeleteAsync(blogVideo.VideoNameInFileSystem, UploadDirectory.BlogVideo);

            _dataContext.BlogVideos.Remove(blogVideo);
            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-blog-video-list", new { blogId = blogId });
        }

        #endregion
    }
}
