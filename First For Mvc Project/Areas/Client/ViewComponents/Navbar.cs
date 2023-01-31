using First_For_Mvc_Project.Areas.Client.ViewModels.Navbar;
using First_For_Mvc_Project.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace First_For_Mvc_Project.Areas.Client.ViewComponents
{
    public class Navbar : ViewComponent
    {
        private readonly DataContext _dataContext;
        public Navbar(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            
            var model = await _dataContext.Navbars.Include(n => n.Subnavbars.OrderBy(s => s.Order)).
                Where(n => n.IsViewOnHeader).OrderBy(n => n.Order).
                Select(n => new ListViewModel(n.Name, n.ToURL, n.Subnavbars.Select(s => new ListViewModel.SubnavbarItem(s.Name, s.ToURL)).ToList()
                )).ToListAsync();

            return View(model);
        }

    }
}
