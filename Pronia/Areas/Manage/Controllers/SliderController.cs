using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pronia.DataAccess;
using Pronia.Extentions;
using Pronia.Models;
using Pronia.Services.Interfaces;
using Pronia.ViewModels.SliderVMs;

namespace Pronia.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class SliderController : Controller
    {
        private readonly ISliderService _service;
        public SliderController(ISliderService service)
        {
            _service = service;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
            return View(await _service.GetAll());
            }
            catch (Exception)
            {
                return NotFound();
            }
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateSliderVM sliderVM)
        {
            try
            {
                if (sliderVM.ImageFile != null)
                {
                    if (!sliderVM.ImageFile.IsTypeValid("image"))
                        ModelState.AddModelError("ImageFile", "Wrong file type");
                    if (sliderVM.ImageFile.IsSizeValid(2))
                        ModelState.AddModelError("ImageFile", "File max size is 2mb");
                }
                if (!ModelState.IsValid) return View();
                await _service.Create(sliderVM);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                return NotFound();
            }
        }
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
            await _service.Delete(id);
            TempData["IsDeleted"] = true;
            return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                return NotFound();
            }
        }
        public async Task<IActionResult> Update(int id)
        {
            try
            {
            return View(await _service.GetById(id));
            }
            catch (Exception)
            {
                return NotFound();
            }
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id, UpdateSliderVMs sliderVM)
        {
            try
            {
            await _service.Update(sliderVM);
            return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                return NotFound();
            }
        }
    }
}
