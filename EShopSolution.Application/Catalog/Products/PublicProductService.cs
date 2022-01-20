﻿using EShopSolution.Data.EF;
using EShopSolution.ViewModels.Catalog.Products;
using EShopSolution.ViewModels.Catalog.Products.Public;
using EShopSolution.ViewModels.Common;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShopSolution.Application.Catalog.Products
{
    public class PublicProductService : IPublicProductService
    {
        private readonly EShopDbContext _context;
        public PublicProductService(EShopDbContext context)
        {
            _context = context;
        }
        public async Task<PagedResult<ProductViewModel>> GetAllByCategory(GetProductPagingRequest request)
        {
            //1. Select join
            var query = from p in _context.Products
                        join pt in _context.ProductTranslations on p.Id equals pt.ProductId
                        join pic in _context.ProductInCategories on p.Id equals pic.ProductId
                        join c in _context.Categories on pic.CategoryId equals c.Id
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
                TotalRecord = totalRow,
                Items = data
            };

            return pagedResult;
        }
    }
}