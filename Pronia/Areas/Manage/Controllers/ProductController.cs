using Microsoft.AspNetCore.Mvc;
using Pronia.Extentions;
using Pronia.Services.Interfaces;
using Pronia.ViewModels.ProductVMs;

namespace Pronia.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class ProductController : Controller
    {
        readonly IProductService _service;

        public ProductController(IProductService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _service.GetAll(true));
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateProductVM productVM)
        {
            if (!productVM.MainImageFile.IsTypeValid("image"))
            {
                ModelState.AddModelError("MainImageFile", "Wrong file type");
            }
            if (!productVM.MainImageFile.IsSizeValid(2))
            {
                ModelState.AddModelError("MainImageFile", "File max size is 2 mb");
            }
            if (productVM.HoverImageFile != null)
            {
                if (!productVM.HoverImageFile.IsTypeValid("image"))
                {
                    ModelState.AddModelError("HoverImageFile", "Wrong file type");
                }
                if (!productVM.HoverImageFile.IsSizeValid(2))
                {
                    ModelState.AddModelError("HoverImageFile", "File max size is 2 mb");
                }
            }
            if(productVM.ImageFiles != null)
            {
                foreach (var imgs in productVM.ImageFiles)
                {
                    if (!imgs.IsTypeValid("image"))
                    {
                        ModelState.AddModelError("ImageFiles", "Wrong file type");
                    }
                    if (!imgs.IsSizeValid(2))
                    {
                        ModelState.AddModelError("ImageFiles", "File max size is 2 mb");
                    }
                }
            }
            if (!ModelState.IsValid) return View();
            await _service.Create(productVM);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int? id)
        {
            await _service.Delete(id);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> ChangeStatus(int? id)
        {
            await _service.SoftDelete(id);
            TempData["IsDeleted"] = true;
            return RedirectToAction(nameof(Index));
        }
    }
}
