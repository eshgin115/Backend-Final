using First_For_Mvc_Project.Areas.Client.ViewModels.Shop;
using First_For_Mvc_Project.Database;
using Microsoft.AspNetCore.Mvc;

namespace First_For_Mvc_Project.Areas.Client.ViewComponents
{
    public class SearchModal : ViewComponent
    {
      
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(new ListViewModel());
        }
    }
}
