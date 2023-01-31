using Pronia.Areas.Admin.ViewModels.Navbar;
using Pronia.Database;
using Pronia.Database.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Pronia.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("navbar")]
    public class NavbarController :Controller
    {
        private readonly DataContext _dataContext;
        private readonly ILogger<NavbarController> _logger;

        public NavbarController(DataContext dataContext, ILogger<NavbarController> logger)
        {
            _dataContext = dataContext;
            _logger = logger;
        }
        #region List
        [HttpGet("list", Name = "admin-navbar-list")]
        public async Task<IActionResult> ListAsync()
        {
            var model = await _dataContext.Navbars
                .Select(n => new ListViewModel(n.Id, n.Name,n.Order))
                .ToListAsync();

            return View(model);
        }
        #endregion


        #region add
        [HttpGet("add", Name = "admin-navbar-add")]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost("add", Name = "admin-navbar-add")]
        public async Task<IActionResult> AddAsync(AddViewModel model)
        {
            if (!ModelState.IsValid) return View(model);





            if (_dataContext.Navbars.Any(n => n.Order == model.Order))
            {
                ModelState.AddModelError(String.Empty, "this order using");
                return View(model);

            }
            var navbar = CreateNavbar();

            await _dataContext.Navbars.AddAsync(navbar);
            await _dataContext.SaveChangesAsync();
            return RedirectToRoute("admin-navbar-list");

            Navbar CreateNavbar()
            {
                var navbar = new Navbar
                {
                    Name = model.Name,
                    ToURL = model.ToURL,
                    Order = model.Order,
                    IsViewOnFooter = model.IsViewOnFooter,
                    IsViewOnHeader = model.IsViewOnHeader,

                };
                return navbar;
            };
        }
        #endregion


        #region update
        [HttpGet("update/{id}", Name = "admin-navbar-update")]
        public async Task<IActionResult> UpdateAsync([FromRoute] int id)
        {
            var navbar = await _dataContext.Navbars.FirstOrDefaultAsync(n => n.Id == id);


            if (navbar is null) return NotFound();




            var model = CreateModel();

            return View( model);

            UpdateViewModel CreateModel()
            {
                var model = new UpdateViewModel
                {
                    Order = navbar.Order,
                    IsViewOnHeader = navbar.IsViewOnHeader,
                    IsViewOnFooter = navbar.IsViewOnFooter,
                    Id = id
                };
                return model;
            }


        }
        [HttpPost("update/{id}", Name = "admin-navbar-update")]
        public async Task<IActionResult> UpdateAsync(UpdateViewModel model)
        {
            var navbar = await _dataContext.Navbars.FirstOrDefaultAsync(n => n.Id == model.Id);
            if (navbar is null) return NotFound();





            if (!ModelState.IsValid) return View( model);




            if (!_dataContext.Navbars.Any(n => n.Id == model.Id) && !(model.Order == navbar.Order)) return View( model);




            Update();


            await _dataContext.SaveChangesAsync();

            return RedirectToRoute("admin-navbar-list");

            void Update()
            {
                navbar.Order = model.Order;
                navbar.ToURL = model.ToURL;
                navbar.Name = model.Name;
                navbar.IsViewOnFooter = model.IsViewOnFooter;
                navbar.IsViewOnHeader = model.IsViewOnHeader;
            }

        }
        #endregion
        #region delete
        [HttpPost("delete/{id}", Name = "admin-navbar-delete")]
        public async Task<IActionResult> DeleteAsync(UpdateViewModel model)
        {
            var navbar = await _dataContext.Navbars.FirstOrDefaultAsync(n => n.Id == model.Id);
            if (navbar is null) return NotFound();


            _dataContext.Navbars.Remove(navbar);
            await _dataContext.SaveChangesAsync();



            return RedirectToRoute("admin-navbar-list");

        }
        #endregion
    }
}
