using First_For_Mvc_Project.Areas.Client.ViewModels.Basket;
using First_For_Mvc_Project.Areas.Client.ViewModels.Product;
using First_For_Mvc_Project.Contracts.File;
using First_For_Mvc_Project.Database;
using First_For_Mvc_Project.Database.Models;
using First_For_Mvc_Project.Services.Concretes;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace First_For_Mvc_Project.Services.Services
{
    public class BasketService : IBasketService
    {
        private readonly DataContext _dataContext;
        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IFileService _fileService;



        public BasketService(DataContext dataContext, IUserService userService, IHttpContextAccessor httpContextAccessor, IFileService fileService)
        {
            _dataContext = dataContext;
            _userService = userService;
            _httpContextAccessor = httpContextAccessor;
            _fileService = fileService;
        }

        public async Task<List<ProductCookieViewModel>> AddBasketProductAsync(Plant plant, ModalViewModel model)
        {
            if (_userService.IsAuthenticated)
            {
                await AddToDatabaseAsync();

                return new List<ProductCookieViewModel>();
            }

            return AddToCookie();





            //Add product to database if user is authenticated
            async Task AddToDatabaseAsync()
            {
                var basketProduct = await _dataContext.BasketProducts
                    .FirstOrDefaultAsync(bp => bp.Basket.UserId == _userService.CurrentUser.Id && bp.PlantId == plant.Id);

                if (basketProduct is not null)
                {
                    basketProduct.Quantity++;
                }
                else
                {
                    var basket = await _dataContext.Baskets.FirstAsync(b => b.UserId == _userService.CurrentUser.Id);

                    basketProduct = new BasketProduct
                    {
                        Quantity = model.Quantity != null ? model.Quantity : 1,
                        BasketId = basket.Id,
                        PlantId = plant.Id,
                        SizeId = model.SizeId != null ? model.SizeId : null,
                        ColorId = model.ColorId != null ? model.ColorId : null,
                    };

                    await _dataContext.BasketProducts.AddAsync(basketProduct);
                }

                await _dataContext.SaveChangesAsync();
            }


            List<ProductCookieViewModel> AddToCookie()
            {
                var productCookieValue = _httpContextAccessor.HttpContext.Request.Cookies["products"];
                var productsCookieViewModel = productCookieValue is not null
                    ? JsonSerializer.Deserialize<List<ProductCookieViewModel>>(productCookieValue)
                    : new List<ProductCookieViewModel> { };

                var productCookieViewModel = productsCookieViewModel!.FirstOrDefault(pcvm => pcvm.Id == plant.Id);
                if (productCookieViewModel is null)
                {

                            productsCookieViewModel
                                    !.Add(new ProductCookieViewModel(plant.Id, plant.Title, plant.PlantImages.Take(1)!.FirstOrDefault()! != null
                                    ? _fileService.GetFileUrl(plant.PlantImages!.Take(1)!.FirstOrDefault()!.ImageNameInFileSystem!, UploadDirectory.Plant) : string.Empty,
                                    model.Quantity != null ? model.Quantity : 1, plant.Price,
                                    model.Quantity != null ? model.Quantity * plant.Price : plant.Price,
                                    model.SizeId != null ? model.SizeId : null,
                                    model.ColorId != null ? model.ColorId : null,
                                       _dataContext.PlantSizes.Include(ps => ps.Size).Where(ps => ps.PlantId == plant.Id)
                                       .Select(ps => new Areas.Admin.ViewModels.Size.ListViewModel(ps.SizeId, ps.Size.Name)).ToList(),
                                       _dataContext.PlantColors.Include(ps => ps.Color).Where(ps => ps.PlantId == plant.Id)
                                       .Select(ps => new Areas.Admin.ViewModels.Color.ListViewModel(ps.ColorId, ps.Color.Name)).ToList()

                                    ));
                }
                else
                {

                    //if (model.Quantity != null) productCookieViewModel.Quantity += model.Quantity;


                    //else productCookieViewModel.Quantity += 1;

                    productCookieViewModel.Quantity = model.Quantity != null ? productCookieViewModel.Quantity += model.Quantity : productCookieViewModel.Quantity += 1;

                    productCookieViewModel.ColorId = model.ColorId != null ? model.ColorId : productCookieViewModel.ColorId;

                    productCookieViewModel.SizeId = model.SizeId != null ? model.SizeId : productCookieViewModel.SizeId;



                    productCookieViewModel.Total = productCookieViewModel.Quantity * productCookieViewModel.Price;

                }

                _httpContextAccessor.HttpContext.Response.Cookies.Append("products", JsonSerializer.Serialize(productsCookieViewModel));

                return productsCookieViewModel;
            }
        }


    }
}
