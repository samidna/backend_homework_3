using Microsoft.EntityFrameworkCore;
using Pronia.DataAccess;
using Pronia.ExtentionServices.Interfaces;
using Pronia.Models;
using Pronia.Services.Interfaces;
using Pronia.ViewModels.ProductVMs;

namespace Pronia.Services.Implements;

public class ProductService : IProductService
{
    readonly ProniaDbContext _context;
    readonly IFileService _fileService;

    public ProductService(ProniaDbContext context, IFileService fileService)
    {
        _context = context;
        _fileService = fileService;
    }

    public IQueryable<Product> GetTable { get => _context.Set<Product>(); }

    public async Task Create(CreateProductVM productVM)
    {
        Product entity = new()
        {
            Name = productVM.Name,
            Description = productVM.Description,
            Price = productVM.Price,
            Rating = productVM.Rating,
            Discount = productVM.Discount,
            StockCount = productVM.StockCount,
            MainImage = await _fileService.UploadAsync(productVM.MainImageFile, Path.Combine("assets", "imgs", "products"))
        };
        if(productVM.ImageFiles != null)
        {
            List<ProductImage> imgs = new();
            foreach (var item in productVM.ImageFiles)
            {
                string fileName = await _fileService.UploadAsync(item, Path.Combine("assets", "imgs", "products"));
                imgs.Add(new ProductImage
                {
                    Name = fileName
                });
            }
            entity.ProductImages = imgs;
        }
        if (productVM.HoverImageFile != null)
            entity.HoverImage = await _fileService.UploadAsync(productVM.HoverImageFile, Path.Combine("assets", "imgs", "products"));
        await _context.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(int? id)
    {
        var entity = await GetById(id);
        _context.Remove(entity);
        _fileService.Delete(entity.MainImage);
        if(entity.HoverImage != null)
        {
            _fileService.Delete(entity.HoverImage);
        }
        await _context.SaveChangesAsync();
    }

    public async Task<List<Product>> GetAll(bool takeAll)
    {
        if (takeAll)
        {
            return await _context.Products.ToListAsync();
        }
        return await _context.Products.Where(p => p.IsDeleted == false).ToListAsync();
    }

    public async Task<Product> GetById(int? id)
    {
        if (id == null || id < 1) throw new ArgumentException();
        var entity = await _context.Products.FindAsync(id);
        if (entity == null) throw new NullReferenceException();
        return entity;
    }

    public async Task SoftDelete(int? id)
    {
        var entity = await GetById(id);
        entity.IsDeleted = !entity.IsDeleted;
        await _context.SaveChangesAsync();
    }

    public Task Update(UpdateProductVM productVM)
    {
        throw new NotImplementedException();
    }
}
