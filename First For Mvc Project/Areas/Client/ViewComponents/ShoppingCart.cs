﻿using Pronia.Areas.Client.ViewModels.Basket;
using Pronia.Contracts.File;
using Pronia.Database;
using Pronia.Services.Concretes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text.Json;

namespace Pronia.Areas.Client.ViewComponents
{
    public class ShoppingCart : ViewComponent
    {
        private readonly DataContext _dataContext;
        private readonly IUserService _userService;
        private readonly IFileService _fileService;


        public ShoppingCart
            (DataContext dataContext,
            IUserService userService,
            IFileService fileService)
        {
            _dataContext = dataContext;
            _userService = userService;
            _fileService = fileService;
        }

        public async Task<IViewComponentResult> InvokeAsync(List<ProductCookieViewModel>? viewModels = null)
        {
            // Case 1 : Qeydiyyat kecilib, o zaman bazadan gotur
            if (_userService.IsAuthenticated)
            {
                var model = await CreateModel();
                return View(model);
            }
            //Case 2: Argument olaraq actiondan gonderilib
            if (viewModels is not null)
            {
                return View(viewModels);
            }


            //Case 3: Argument gonderilmeyib bu zaman cookiden oxu
            var productsCookieValue = HttpContext.Request.Cookies["products"];
            var productsCookieViewModel = new List<ProductCookieViewModel>();
            if (productsCookieValue is not null)
            {
                productsCookieViewModel = JsonSerializer.Deserialize<List<ProductCookieViewModel>>(productsCookieValue);
            }

            return View(productsCookieViewModel);

            async Task<List<ProductCookieViewModel>> CreateModel()
            {
                var model = await _dataContext.BasketProducts
                   .Where(bp => bp.Basket.UserId == _userService.CurrentUser.Id)
                   .Select(bp =>
                       new ProductCookieViewModel(
                           bp.PlantId,
                           bp.Plant!.Title,
                           bp.Plant.PlantImages.Take(1)!.FirstOrDefault()! != null
                       ? _fileService.GetFileUrl(bp.Plant.PlantImages!.Take(1)!.FirstOrDefault()!.ImageNameInFileSystem!, UploadDirectory.Plant)
                       : string.Empty,
                           bp.Quantity,
                           bp.Plant.Price,
                           bp.Plant.Price * bp.Quantity,
                            _dataContext.PlantSizes.Include(ps => ps.Size).Where(ps => ps.PlantId == bp.PlantId)
                           .Select(ps => new Admin.ViewModels.Size.ListViewModel(ps.SizeId, ps.Size.Name)).ToList(),
                            _dataContext.PlantColors.Include(ps => ps.Color).Where(ps => ps.PlantId == bp.PlantId)
                           .Select(ps => new Admin.ViewModels.Color.ListViewModel(ps.ColorId, ps.Color.Name)).ToList()
                           ))
                   .ToListAsync();
                return model;
            }
        }
    }
}
