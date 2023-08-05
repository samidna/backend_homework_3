using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pronia.DataAccess;
using Pronia.Models;
using Pronia.Services.Interfaces;
using Pronia.ViewModels.HomeVMs;

namespace Pronia.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISliderService _sliderService;

        private readonly IProductService _productService;
        public HomeController(ISliderService sliderService, IProductService productService)
        {
            _sliderService = sliderService;
            _productService = productService;
        }

        public async Task<IActionResult> Index()
        {
            HomeVM vm = new()
            {
                Sliders = await _sliderService.GetAll(),
                Products = await _productService.GetAll(false)
            };
            return View(vm);
        }
    }
}