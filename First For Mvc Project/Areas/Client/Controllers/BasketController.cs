using Pronia.Areas.Client.ViewComponents;
using Pronia.Areas.Client.ViewModels.Basket;
using Pronia.Areas.Client.ViewModels.Product;
using Pronia.Database;
using Pronia.Database.Models;
using Pronia.Services.Concretes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace Pronia.Areas.Client.Controllers
{
    [Area("client")]
    [Route("basket")]
    public class BasketController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IBasketService _basketService;
        private readonly IUserService _userService;

        public BasketController
            (DataContext dataContext,
            IBasketService basketService,
            IUserService userService)
        {
            _dataContext = dataContext;
            _basketService = basketService;
            _userService = userService;
        }


        [HttpPost("add/{id}", Name = "client-basket-add")]
        public async Task<IActionResult> AddProductAsync([FromRoute] int id,  ModalViewModel model)
        {
            var product = await _dataContext.Plants.Include(p => p.PlantImages).FirstOrDefaultAsync(p => p.Id == id);
            if (product is null)
            {
                return NotFound();
            }

            var productsCookieViewModel = await _basketService.AddBasketProductAsync(product,model);
            if (productsCookieViewModel.Any())
            {
                return ViewComponent(nameof(ShopCart), productsCookieViewModel);
            }

            return ViewComponent(nameof(ShopCart));
        }
        [HttpGet("delete/{id}", Name = "client-basket-delete")]
        public async Task<IActionResult> DeleteProductAsync([FromRoute] int id)
        {
            var product = await _dataContext.Plants.FirstOrDefaultAsync(p => p.Id == id);
            if (product is null)
            {
                return NotFound();
            }


            if (_userService.IsAuthenticated)
            {
                var basketProduct = await _dataContext.BasketProducts.FirstOrDefaultAsync(p => p.PlantId == id);

                _dataContext.BasketProducts.Remove(basketProduct);
                await _dataContext.SaveChangesAsync();
                return ViewComponent(nameof(ShopCart));
            }
            var productCookieValue = HttpContext.Request.Cookies["products"];
            if (productCookieValue is null)
            {
                return NotFound();
            }

            var productsCookieViewModel = JsonSerializer.Deserialize<List<ProductCookieViewModel>>(productCookieValue);
            productsCookieViewModel.RemoveAll(pcvm => pcvm.Id == id);

            HttpContext.Response.Cookies.Append("products", JsonSerializer.Serialize(productsCookieViewModel));

            return ViewComponent(nameof(ShopCart), productsCookieViewModel);
        }

            #region Add_Update
        [HttpPost("addbymodal/{id}", Name = "client-basket-addbymodal")]
        public async Task<IActionResult> AddProductByModalAsync([FromRoute] int id, ModalViewModel model)
        {
            var product = await _dataContext.Plants.Include(p => p.PlantImages).FirstOrDefaultAsync(p => p.Id == id);


            if (product is null) return NotFound();


            var productsCookieViewModel = await _basketService.AddBasketProductAsync(product,model);


            if (productsCookieViewModel.Any()) return ViewComponent(nameof(ShoppingCart), productsCookieViewModel);

            return ViewComponent(nameof(ShoppingCart));
        }
        [HttpGet("update", Name = "client-shopping-basket-update")]
        public IActionResult UpdateProduct()
        {
            return ViewComponent(nameof(ShopCart));
        }
        #endregion

    }
}
