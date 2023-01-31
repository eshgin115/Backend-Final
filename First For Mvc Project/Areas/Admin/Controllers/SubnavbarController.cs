using Pronia.Areas.Admin.ViewModels.Subnavbar;
using Pronia.Database;
using Pronia.Database.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace Pronia.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("subnavbar")]
    [Authorize]
    public class SubnavbarController : Controller
    {

        private readonly DataContext _dataContext;
        private readonly ILogger<SubnavbarController> _logger;
        public SubnavbarController(DataContext dataContext, ILogger<SubnavbarController> logger)
        {
            _dataContext = dataContext;
            _logger = logger;
        }

        [HttpGet("list", Name = "admin-subnavbar-list")]
        public async Task<IActionResult> List()
        {
            var model = await _dataContext.Subnavbars.Select(s => new ListViewModel(s.Id, s.Name, s.Navbar.Name)).ToListAsync();
            return View(model);
        }

        #region Add
        [HttpGet("add", Name = "admin-subnavbar-add")]
        public async Task<IActionResult> AddAsync()
        {
            var model = new AddViewModel
            {

                Navbars = await _dataContext.Navbars.Select(n => new ViewModels.Navbar.ListViewModel(n.Id, n.Name)).ToListAsync()

            };
            return View(model);
        }
        [HttpPost("add", Name = "admin-subnavbar-add")]
        public async Task<IActionResult> Add(AddViewModel model)
        {
            if (!ModelState.IsValid) return await GetView(model);

            if (!_dataContext.Navbars.Any(n => n.Id == model.NavbarId))
            {
                ModelState.AddModelError(String.Empty, "Navbar is not found");
                return await GetView(model);
            }




            await AddBook();

            return RedirectToRoute("admin-subnavbar-list");




            async Task<IActionResult> GetView(AddViewModel model)
            {
                model.Navbars = await _dataContext.Navbars
                    .Select(s => new ViewModels.Navbar.ListViewModel(s.Id, s.Name))
                    .ToListAsync();


                return View(model);
            }

            async Task AddBook()
            {
                var subnavbar = new Subnavbar
                {
                    Name = model.Name,
                    ToURL = model.ToURL,
                    NavbarId = model.NavbarId,
                    Order = model.Order,


                };

                await _dataContext.Subnavbars.AddAsync(subnavbar);



                await _dataContext.SaveChangesAsync();
            }
        }

        #endregion
        #region Update
        [HttpGet("update/{id}", Name = "admin-subnavbar-update")]
        public async Task<IActionResult> UpdateAsync([FromRoute] int id)
        {
            var subnavbar = await _dataContext.Subnavbars.FirstOrDefaultAsync(n => n.Id == id);
            if (subnavbar is null) return NotFound();

            var model = await CreateModelAsync();

            return View(model);


            async Task<UpdateViewModel> CreateModelAsync()
            {
                var model = new UpdateViewModel
                {
                    Id = subnavbar.Id,
                    Order = subnavbar.Order,
                    ToURL = subnavbar.ToURL,
                    Name = subnavbar.Name,
                    Navbars = await _dataContext.Navbars.Select(s => new ViewModels.Navbar.ListViewModel(s.Id, s.Name)).ToListAsync()


                };
                return model;
            }
        }


        [HttpPost("update/{id}", Name = "admin-subnavbar-update")]
        public async Task<IActionResult> UpdateAsync(UpdateViewModel model)
        {
            var subnavbar = await _dataContext.Subnavbars.FirstOrDefaultAsync(b => b.Id == model.Id);
            if (subnavbar is null) return NotFound();


            if (!ModelState.IsValid) return await GetView(model);


            if (!_dataContext.Navbars.Any(n => n.Id == model.NavbarId))
            {
                ModelState.AddModelError(String.Empty, "navbar is not found");
                return await GetView(model);
            }




            await UpdateBookAsync();

            return RedirectToRoute("admin-subnavbar-list");




            async Task<IActionResult> GetView(UpdateViewModel model)
            {
                model.Navbars = await _dataContext.Navbars.Select(s => new ViewModels.Navbar.ListViewModel(s.Id, s.Name)).ToListAsync();



                return View(model);
            }

            async Task UpdateBookAsync()
            {
                subnavbar.ToURL = model.ToURL;
                subnavbar.NavbarId = model.NavbarId;
                subnavbar.Order = model.Order;
                subnavbar.Name = model.Name;
                await _dataContext.SaveChangesAsync();
            }
        }

        #endregion

        #region Delete

        [HttpPost("delete/{id}", Name = "admin-subnavbar-delete")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        {
            var subnavbar = await _dataContext.Subnavbars.FirstOrDefaultAsync(b => b.Id == id);
            if (subnavbar is null) return NotFound();


            _dataContext.Subnavbars.Remove(subnavbar);
            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-subnavbar-list");
        }

        #endregion
    }
}
