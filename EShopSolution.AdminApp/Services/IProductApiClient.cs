using EShopSolution.ViewModels.Catalog.Products;
using EShopSolution.ViewModels.Common;
using EShopSolution.ViewModels.System.Users;
using System.Threading.Tasks;

namespace EShopSolution.AdminApp.Services
{
    public interface IProductApiClient
    {
        Task<PagedResult<ProductViewModel>> GetProductsPaging(GetManageProductPagingRequest request);
        Task<ApiResult<bool>> CreateProduct(ProductCreateRequest request);
    }
}
