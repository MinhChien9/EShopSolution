using EShopSolution.Application.Common;
using EShopSolution.Data.EF;
using EShopSolution.Data.Entities;
using EShopSolution.Utilities.Exceptions;
using EShopSolution.ViewModels.Catalog.ProductImages;
using EShopSolution.ViewModels.Catalog.Products;
using EShopSolution.ViewModels.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace EShopSolution.Application.Catalog.Products
{
    public class ProductService : IProductService
    {
        private readonly EShopDbContext _context;
        private readonly IStorageService _storageService;
        public ProductService(EShopDbContext context, IStorageService storageService)
        {
            _context = context;
            _storageService = storageService;
        }
        public async Task<int> Create(ProductCreateRequest request)
        {
            var product = new Product()
            {
                Price = request.Price,
                ViewCount = 0,
                DateCreated = DateTime.Now,
                OriginalPrice = request.OriginalPrice,
                Stock = request.Stock,
                ProductTranslations = new List<ProductTranslation>() {
                    new ProductTranslation()
                    {
                        Name=request.Name,
                        Description=request.Description,
                        Details=request.Details,
                        SeoAlias=request.SeoAlias,
                        SeoDescription=request.SeoDescription,
                        SeoTitle=request.SeoTitle,
                        LanguageId=request.LanguageId
                    }
                }
            };
            //Save image
            if (request.ThumnailImage != null)
            {
                product.ProductImages = new List<ProductImage>()
                {
                    new ProductImage(){
                        Caption="Thumnail Image",
                        DateCreated=DateTime.Now,
                        FileSize=request.ThumnailImage.Length,
                        ImagePath= await this.SaveFile(request.ThumnailImage),
                        IsDefault=true,
                        SortOrder=1
                    }
                };
            }

            _context.Products.Add(product);

            await _context.SaveChangesAsync();
            return product.Id;
        }
        public async Task<int> Update(ProductUpdateRequest request)
        {
            var product = await _context.Products.FindAsync(request.Id);

            var productTranslations = await _context.ProductTranslations.FirstOrDefaultAsync(pt => pt.ProductId == request.Id && pt.LanguageId == request.LanguageId);

            if (product == null || productTranslations == null)
                throw new EShopException($"Can not find product: {request.Id}");

            productTranslations.Name = request.Name;
            productTranslations.SeoAlias = request.SeoAlias;
            productTranslations.SeoDescription = request.SeoDescription;
            productTranslations.SeoTitle = request.SeoTitle;
            productTranslations.Description = request.Description;
            productTranslations.Details = request.Details;

            //Save image
            if (request.ThumnailImage != null)
            {
                var thumnailImage = await _context.ProductImages.FirstOrDefaultAsync(p => p.IsDefault == true && p.ProductId == request.Id);

                if (thumnailImage != null)
                {
                    thumnailImage.FileSize = request.ThumnailImage.Length;
                    thumnailImage.ImagePath = await this.SaveFile(request.ThumnailImage);

                    _context.ProductImages.Update(thumnailImage);
                }

            }

            return await _context.SaveChangesAsync();
        }
        public async Task<int> Delete(int productId)
        {
            var product = await _context.Products.FindAsync(productId);

            if (product == null)
                throw new EShopException($"Can not find product: {productId}");

            var images = _context.ProductImages.Where(p => p.ProductId == productId);

            foreach (var image in images)
            {
                await _storageService.DeleteFileAsync(image.ImagePath);
            }

            _context.Remove(product);

            return await _context.SaveChangesAsync();
        }
        public async Task<bool> IncreaseViewCount(int productId)
        {
            var product = await _context.Products.FindAsync(productId);

            product.ViewCount += 1;

            return await _context.SaveChangesAsync() > 0;

        }

        public async Task<bool> UpdatePrice(int productId, decimal newPrice)
        {
            var product = await _context.Products.FindAsync(productId);

            if (product == null)
                throw new EShopException($"Can not find product: {productId}");

            product.Price = newPrice;

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateStock(int productId, int quantity)
        {
            var product = await _context.Products.FindAsync(productId);

            if (product == null)
                throw new EShopException($"Can not find product: {productId}");

            product.Stock += quantity;

            return await _context.SaveChangesAsync() > 0;

        }

        public async Task<PagedResult<ProductViewModel>> GetAllPaging(GetManageProductPagingRequest request)
        {
            //1. Select join
            var query = from p in _context.Products
                        join pt in _context.ProductTranslations on p.Id equals pt.ProductId
                        join pic in _context.ProductInCategories on p.Id equals pic.ProductId
                        join c in _context.Categories on pic.CategoryId equals c.Id
                        where pt.LanguageId == request.LanguageId
                        select new { p, pt, pic };
            //2. fillter
            if (!string.IsNullOrEmpty(request.KeyWord))
                query = query.Where(x => x.pt.Name.ToLower().Contains(request.KeyWord.ToLower()));

            if (request.CategoryIds!=null&& request.CategoryIds.Count > 0)
                query = query.Where(x => request.CategoryIds.Contains(x.pic.CategoryId));

            //3. paging
            int totalRow = await query.CountAsync();

            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new ProductViewModel()
                {
                    Id = x.p.Id,
                    Stock = x.p.Stock,
                    ViewCount = x.p.ViewCount,
                    OriginalPrice = x.p.OriginalPrice,
                    Price = x.p.Price,
                    Name = x.pt.Name,
                    DateCreated = x.p.DateCreated,
                    Description = x.pt.Description,
                    Details = x.pt.Details,
                    LanguageId = x.pt.LanguageId,
                    SeoAlias = x.pt.SeoAlias,
                    SeoDescription = x.pt.SeoDescription,
                    SeoTitle = x.pt.SeoTitle
                }
                ).ToListAsync();

            //4. select and projection
            var pagedResult = new PagedResult<ProductViewModel>()
            {
                TotalRecords = totalRow,
                PageSize = request.PageSize,
                PageIndex = request.PageIndex,
                Items = data
            };

            return pagedResult;
        }

        private async Task<string> SaveFile(IFormFile file)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            await _storageService.SaveFileAsync(file.OpenReadStream(), fileName);
            return fileName;
        }


        public async Task<ProductViewModel> GetById(int productId, string languageId)
        {
            var product = await _context.Products.FindAsync(productId);

            var productTranslation = await _context.ProductTranslations.FirstOrDefaultAsync(x => x.ProductId == productId && x.LanguageId == languageId);

            if (product == null)
                throw new EShopException($"Can not find product {productId}");

            if (productTranslation == null)
                throw new EShopException($"Can not find language {languageId}");

            var productViewModel = new ProductViewModel()
            {
                Id = productId,
                Stock = product.Stock,
                ViewCount = product.ViewCount,
                OriginalPrice = product.OriginalPrice,
                Price = product.Price,
                DateCreated = product.DateCreated,
                Name = productTranslation.Name,
                Description = productTranslation.Description,
                Details = productTranslation.Details,
                LanguageId = productTranslation.LanguageId,
                SeoAlias = productTranslation.SeoAlias,
                SeoDescription = productTranslation.SeoDescription,
                SeoTitle = productTranslation.SeoTitle
            };

            return productViewModel;


        }
        public async Task<int> AddImage(int productId, ProductImageCreateRequest request)
        {

            var productImage = new ProductImage()
            {
                Caption = request.Caption,
                DateCreated = DateTime.Now,
                SortOrder = request.SortOrder,
                ProductId = productId
            };

            if (request.ImageFile != null)
            {
                productImage.ImagePath = await this.SaveFile(request.ImageFile);
                productImage.FileSize = request.ImageFile.Length;
            }

            _context.ProductImages.Add(productImage);

            return await _context.SaveChangesAsync();

        }

        public async Task<int> UpdateImage(int imageId, ProductImageUpdateRequest request)
        {
            var productImage = await _context.ProductImages.FindAsync(imageId);

            if (productImage == null)
                throw new EShopException($"Can not find image with id {imageId}");

            if (request.ImageFile != null)
            {
                productImage.FileSize = request.ImageFile.Length;
                productImage.ImagePath = await this.SaveFile(request.ImageFile);
            }

            return await _context.SaveChangesAsync();
        }

        public async Task<int> RemoveImage(int imageId)
        {
            var productImage = await _context.ProductImages.FindAsync(imageId);

            if (productImage == null)
                throw new EShopException($"Can not find image with id {imageId}");

            _context.ProductImages.Remove(productImage);

            return await _context.SaveChangesAsync();
        }

        public async Task<List<ProductImageViewModel>> GetListImages(int productId)
        {
            var images = await _context.ProductImages.Where(x => x.ProductId == productId)
                .Select(i => new ProductImageViewModel()
                {
                    Caption = i.Caption,
                    DateCreated = i.DateCreated,
                    FileSize = i.FileSize,
                    Id = i.Id,
                    ImagePath = i.ImagePath,
                    IsDefault = i.IsDefault,
                    ProductId = i.ProductId,
                    SortOrder = i.SortOrder
                }).ToListAsync();

            return images;
        }


        public async Task<ProductImageViewModel> GetImageById(int imageId)
        {
            var image = await _context.ProductImages.FindAsync(imageId);

            if (image == null)
                throw new EShopException($"Can not find image with id {imageId}");

            var imageViewModel = new ProductImageViewModel()
            {
                Caption = image.Caption,
                DateCreated = image.DateCreated,
                FileSize = image.FileSize,
                Id = image.Id,
                ImagePath = image.ImagePath,
                IsDefault = image.IsDefault,
                ProductId = image.ProductId,
                SortOrder = image.SortOrder
            };

            return imageViewModel;
        }
        public async Task<PagedResult<ProductViewModel>> GetAllByCategory(string languageId, GetPublicProductPagingRequest request)
        {
            //1. Select join
            var query = from p in _context.Products
                        join pt in _context.ProductTranslations on p.Id equals pt.ProductId
                        join pic in _context.ProductInCategories on p.Id equals pic.ProductId
                        join c in _context.Categories on pic.CategoryId equals c.Id
                        where pt.LanguageId == languageId
                        select new { p, pt, pic };

            //2. fillter
            if (request.CategoryId.HasValue && request.CategoryId > 0)
                query = query.Where(x => x.pic.CategoryId == request.CategoryId);

            //3. paging
            int totalRow = await query.CountAsync();

            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new ProductViewModel()
                {
                    Id = x.p.Id,
                    Stock = x.p.Stock,
                    ViewCount = x.p.ViewCount,
                    OriginalPrice = x.p.OriginalPrice,
                    Price = x.p.Price,
                    Name = x.pt.Name,
                    DateCreated = x.p.DateCreated,
                    Description = x.pt.Description,
                    Details = x.pt.Details,
                    LanguageId = x.pt.LanguageId,
                    SeoAlias = x.pt.SeoAlias,
                    SeoDescription = x.pt.SeoDescription,
                    SeoTitle = x.pt.SeoTitle
                }
                ).ToListAsync();

            //4. select and projection
            var pagedResult = new PagedResult<ProductViewModel>()
            {
                TotalRecords = totalRow,
                PageSize = request.PageSize,
                PageIndex = request.PageIndex,
                Items = data
            };

            return pagedResult;
        }
    }
}