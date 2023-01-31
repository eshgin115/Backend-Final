using Pronia.Areas.Client.ViewModels.Basket;
using Pronia.Areas.Client.ViewModels.Product;
using Pronia.Database.Models;

namespace Pronia.Services.Concretes
{
    public interface IBasketService
    {
        Task<List<ProductCookieViewModel>> AddBasketProductAsync(Plant plant,ModalViewModel? model= null);
    }
}
