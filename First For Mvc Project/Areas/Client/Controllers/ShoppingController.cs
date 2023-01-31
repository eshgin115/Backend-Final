using First_For_Mvc_Project.Areas.Client.ViewComponents;
using First_For_Mvc_Project.Areas.Client.ViewModels.Basket;
using First_For_Mvc_Project.Areas.Client.ViewModels.Product;
using First_For_Mvc_Project.Areas.Client.ViewModels.Shopping;
using First_For_Mvc_Project.Database;
using First_For_Mvc_Project.Services.Concretes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace First_For_Mvc_Project.Areas.Client.Controllers
{
    [Area("client")]
    [Route("shopping")]
    public class ShoppingController : Controller
    {

        private readonly DataContext _dbContext;
        private readonly IUserService _userService;
        private readonly IOrderService _orderService;
        private readonly INotificationService _notificationService;
        private readonly IBasketService _basketService;


        public ShoppingController
            (DataContext dbContext,
            IUserService userService,
            IOrderService orderService,
            INotificationService notificationService,
            IBasketService basketService = null)
        {
            _dbContext = dbContext;
            _userService = userService;
            _orderService = orderService;
            _notificationService = notificationService;
            _basketService = basketService;
        }

      

        [HttpGet("cart", Name = "client-shopping-cart")]
        public IActionResult Cart()
        {
            return View();
        }
        
        [HttpPost("placerorder", Name = "client-shopping-placerorder")]
        public async Task<IActionResult> PlaceOrder()
        {
            var model = await CreateModel();
           

      

            var order = await _orderService.AddOrderAsync(model.SumTotal);



            await _orderService.AddOrderProductAsync(model, order.Id);

            var pasketProducts = await _dbContext.BasketProducts
                        .Where(bp => bp.Basket!.UserId == _userService.CurrentUser.Id)
                       .ToListAsync();

            _dbContext.BasketProducts.RemoveRange(pasketProducts);


            await _dbContext.SaveChangesAsync();
            await _notificationService.SenOrderCreatedToAdmin(order.Id);

            return RedirectToRoute("client-account-orders");

            async Task<OrderViewModel> CreateModel()
            {
                var model = new OrderViewModel
                {
                    SumTotal = (int)_dbContext.BasketProducts!
                 .Where(bp => bp.Basket!.UserId == _userService.CurrentUser.Id).Sum(bp => bp.Plant.Price * bp.Quantity)!,
                    Models = await _dbContext.BasketProducts
                 .Where(bp => bp.Basket!.UserId == _userService.CurrentUser.Id)
                 .Select(bp =>
                     new OrderViewModel.ItemViewModel(
                         bp.PlantId,
                         bp.Plant.Title,
                         bp.Quantity,
                         bp.Plant.Price,
                         bp.Plant.Price * bp.Quantity,
                         bp.SizeId,
                         bp.ColorId
                         ))
                 .ToListAsync()
                };
                return model;
            };

        }


        //#region Add_Update
        //[HttpPost("add/{id}", Name = "client-shopping-cart-add")]
        //public async Task<IActionResult> AddProductAsync([FromRoute] int id, ModalViewModel model)
        //{
        //    var product = await _dbContext.Plants.Include(p => p.PlantImages).FirstOrDefaultAsync(p => p.Id == id);
        //    if (product is null)
        //    {
        //        return NotFound();
        //    }


        //    var productsCookieViewModel = await _basketService.AddBasketProductAsync(product,model);
        //    if (productsCookieViewModel.Any())
        //    {
        //        return ViewComponent(nameof(ShoppingCart), productsCookieViewModel);
        //    }

        //    return ViewComponent(nameof(ShoppingCart));
        //}
        //[HttpGet("update", Name = "client-shopping-basket-update")]
        //public async Task<IActionResult> UpdateProductAsync()
        //{
        //    return ViewComponent(nameof(ShopCart));
        //}
        //#endregion


        #region Delete

        [HttpGet("delete/{id}", Name = "client-shopping-cart-delete")]
        public async Task<IActionResult> DeleteProductAsync([FromRoute] int id)
        {
            var product = await _dbContext.Plants.FirstOrDefaultAsync(p => p.Id == id);


            if (product is null) return NotFound();


            if (_userService.IsAuthenticated)
            {
                var basketProduct = await _dbContext.BasketProducts.FirstOrDefaultAsync(p => p.PlantId == id);

                _dbContext.BasketProducts.Remove(basketProduct);
                await _dbContext.SaveChangesAsync();
                return ViewComponent(nameof(ShoppingCart));
            }
            var productCookieValue = HttpContext.Request.Cookies["products"];


            if (productCookieValue is null) return NotFound();

            var productsCookieViewModel = JsonSerializer.Deserialize<List<ViewModels.Basket.ProductCookieViewModel>>(productCookieValue);
            productsCookieViewModel.RemoveAll(pcvm => pcvm.Id == id);

            HttpContext.Response.Cookies.Append("products", JsonSerializer.Serialize(productsCookieViewModel));

            return ViewComponent(nameof(ShoppingCart), productsCookieViewModel);


           
        }

        [HttpGet("deleteindivudual/{id}", Name = "client-shopping-cart-delete-indivudual")]
        public async Task<IActionResult> DeleteIndivudualProductAsync([FromRoute] int id)
        {
            var product = await _dbContext.Plants.FirstOrDefaultAsync(p => p.Id == id);

            if (product is null) return NotFound();




            if (_userService.IsAuthenticated)
            {
                var basketProduct = await _dbContext.BasketProducts.FirstOrDefaultAsync(p => p.PlantId == id);

                if (basketProduct!.Quantity > 1)
                {
                    basketProduct.Quantity -= 1;

                }
                _dbContext.BasketProducts.Remove(basketProduct);
                await _dbContext.SaveChangesAsync();
                return ViewComponent(nameof(ShoppingCart));
            }
            var productCookieValue = HttpContext.Request.Cookies["products"];


            if (productCookieValue is null) return NotFound();

            var productsCookieViewModel = JsonSerializer.Deserialize<List<ViewModels.Basket.ProductCookieViewModel>>(productCookieValue);
            foreach (var item in productsCookieViewModel!)
            {
                if (item.Id == product.Id)
                {
                    if (item.Quantity > 1)
                    {
                        item.Quantity -= 1;
                        item.Total = item.Price * item.Quantity;
                        break;
                    }
                    productsCookieViewModel.RemoveAll(pcvm => pcvm.Id == id);
                    break;
                }
            }

            HttpContext.Response.Cookies.Append("products", JsonSerializer.Serialize(productsCookieViewModel));

            return ViewComponent(nameof(ShoppingCart), productsCookieViewModel);
        }
        #endregion

        [Authorize]

        [HttpGet("checkout", Name = "client-shopping-checkout")]
        public async Task<IActionResult> Checkout()
        {
            var model = await CreateModel();

            return View(model);

            async Task<OrderViewModel> CreateModel()
            {

                var model = new OrderViewModel
                {
                    SumTotal = (int)_dbContext.BasketProducts
                 .Where(bp => bp.Basket!.UserId == _userService.CurrentUser.Id).Sum(bp => bp.Plant!.Price * bp.Quantity)!,
                    Models = await _dbContext.BasketProducts
                 .Where(bp => bp.Basket.UserId == _userService.CurrentUser.Id)
                 .Select(bp =>
                     new OrderViewModel.ItemViewModel(
                         bp.Id,
                         bp.Plant.Title,
                         bp.Quantity,
                         bp.Plant.Price * bp.Quantity
                         ))
                 .ToListAsync()
                };
                return model;
            }

        }


    }
}


























        //public async Task<IActionResult> Checkout()
        //{
        //    var model = new ViewModels.Shopping.OrderViewModel
        //    {
        //        SumTotal = (int)_dbContext.BasketProducts
        //          .Where(bp => bp.Basket.UserId == _userService.CurrentUser.Id).Sum(bp => bp.Plant.Price * bp.Quantity),
        //        Models = await _dbContext.BasketProducts
        //          .Where(bp => bp.Basket.UserId == _userService.CurrentUser.Id)
        //          .Select(bp =>
        //              new ViewModels.Shopping.OrderViewModel.ItemViewModel(
        //                  bp.Id,
        //                  bp.Plant.Title,
        //                  bp.Quantity,
        //                  bp.Plant.Price,
        //                  bp.Plant.Price * bp.Quantity
        //                  ))
        //          .ToListAsync()
        //    };

        //    return View(model);

//        }

//        //[HttpPost("placerorder/{id}", Name = "client-shopping-placerorder")]
//        //public async Task<IActionResult> PlaceOrder([FromRoute] int id)
//        //{
//        //    var model = new ViewModels.Shopping.OrderViewModel
//        //    {
//        //        SumTotal = (int)_dbContext.BasketProducts
//        //          .Where(bp => bp.Basket.UserId == _userService.CurrentUser.Id).Sum(bp => bp.Plant.Price * bp.Quantity),
//        //        Models = await _dbContext.BasketProducts
//        //          .Where(bp => bp.Basket.UserId == _userService.CurrentUser.Id)
//        //          .Select(bp =>
//        //              new ViewModels.Shopping.OrderViewModel.ItemViewModel(
//        //                  bp.PlantId,
//        //                  bp.Plant.Title,
//        //                  bp.Quantity,
//        //                  bp.Plant.Price,
//        //                  bp.Plant.Price * bp.Quantity
//        //                  ))
//        //          .ToListAsync()
//        //    };

//        //    //await Task.WhenAll
//        //    //    (  _orderService.AddOrderAsync(model.SumTotal),
//        //    //       _orderService.AddOrderProductAsync(model, order.Id)
//        //    //    );

//        //    var order = await _orderService.AddOrderAsync(model.SumTotal);



//        //    await _orderService.AddOrderProductAsync(model, order.Id);

//        //    var pasketProducts = await _dbContext.BasketProducts
//        //                .Where(bp => bp.Basket.UserId == _userService.CurrentUser.Id)
//        //               .ToListAsync();

//        //    _dbContext.BasketProducts.RemoveRange(pasketProducts);


//        //    await _dbContext.SaveChangesAsync();
//        //    await _notificationService.SenOrderCreatedToAdmin(order.Id);

//        //    return RedirectToRoute("client-account-orders");

//        //}
//    }
//}
