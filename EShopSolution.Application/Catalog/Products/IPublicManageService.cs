using EShopSolution.ViewModels.Catalog.Products;
using EShopSolution.ViewModels.Catalog.Products.Public;
using EShopSolution.ViewModels.Common;
using System.Threading.Tasks;

namespace EShopSolution.Application.Catalog.Products
{
    interface IPublicProductService
    {
        Task<PagedResult<ProductViewModel>> GetAllByCategory(GetProductPagingRequest request);
    }
}