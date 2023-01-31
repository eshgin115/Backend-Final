using First_For_Mvc_Project.Areas.Client.ViewModels.Basket;
using First_For_Mvc_Project.Areas.Client.ViewModels.Product;
using First_For_Mvc_Project.Database.Models;

namespace First_For_Mvc_Project.Services.Concretes
{
    public interface IBasketService
    {
        Task<List<ProductCookieViewModel>> AddBasketProductAsync(Plant plant,ModalViewModel? model= null);
    }
}
