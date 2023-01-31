using Pronia.Areas.Client.ViewModels.Shop;
using Pronia.Database;
using Microsoft.AspNetCore.Mvc;

namespace Pronia.Areas.Client.ViewComponents
{
    public class SearchModal : ViewComponent
    {
      
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(new ListViewModel());
        }
    }
}
