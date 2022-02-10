using EShopSolution.ViewModels.Common;
using EShopSolution.ViewModels.System.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShopSolution.AdminApp.Services
{
    public interface IUserApiClient
    {
        Task<ApiResult<string>> Authenticate(LoginRequest request);

        Task<ApiResult<PagedResult<UserViewModel>>> GetUsersPaging(GetUserPagingRequest request);

        Task<ApiResult<bool>> RegisterUser(RegisterRequest request);
        Task<ApiResult<bool>> UpdateUser(Guid id,UserUpdateRequest request);
        Task<ApiResult<bool>> DeleteUser(UserDeleteRequest request);
        Task<ApiResult<UserViewModel>> GetById(Guid id);
    }
}
