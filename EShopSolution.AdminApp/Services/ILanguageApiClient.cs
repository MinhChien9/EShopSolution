using EShopSolution.ViewModels.Common;
using EShopSolution.ViewModels.System.Languages;
using EShopSolution.ViewModels.System.Users;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EShopSolution.AdminApp.Services
{
    public interface ILanguageApiClient
    {
        Task<ApiResult<List<LanguageViewModel>>> GetAll();
    }
}
