using Microsoft.EntityFrameworkCore;
using Pronia.DataAccess;
using Pronia.ExtentionServices.Interfaces;
using Pronia.Models;
using Pronia.Services.Interfaces;
using Pronia.ViewModels.SliderVMs;

namespace Pronia.Services.Implements
{
    public class SliderService : ISliderService
    {
        private readonly IFileService _fileService;
        private readonly ProniaDbContext _context;
        private readonly IWebHostEnvironment _env;
        public SliderService(ProniaDbContext context, IWebHostEnvironment env, IFileService fileService)
        {
            _context = context;
            _env = env;
            _fileService = fileService;
        }
        public async Task Create(CreateSliderVM sliderVM)
        {
            await _context.Sliders.AddAsync(new Slider
            {
                ImageUrl = await _fileService.UploadAsync(sliderVM.ImageFile,Path.Combine("assets","images"),"image",2),
                Title = sliderVM.Title,
                ButtonText = sliderVM.ButtonText,
                Offer = sliderVM.Offer,
                Description = sliderVM.Description
            });
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var entity = await GetById(id);
            _context.Sliders.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<ICollection<Slider>> GetAll()
        {
            return _context.Sliders.ToList();
        }

        public async Task<Slider> GetById(int id)
        {
            if (id < 1 || id == null) throw new ArgumentException();
            var entity = await _context.Sliders.FindAsync(id);
            if (entity == null) throw new ArgumentException();
            return entity;
        }

        public async Task Update(UpdateSliderVMs sliderVM)
        {
            var entity = await GetById(sliderVM.Id);
            entity.Title = sliderVM.Title;
            entity.Description = sliderVM.Description;
            entity.Offer = sliderVM.Offer;
            //entity.ImageUrl = sliderVM.ImageUrl;
            entity.ButtonText = sliderVM.ButtonText;
            await _context.SaveChangesAsync();
        }
    }
}
