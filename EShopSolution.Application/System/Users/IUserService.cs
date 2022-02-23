using EShopSolution.ViewModels.Common;
using EShopSolution.ViewModels.System.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShopSolution.Application.System.Users
{
    public interface IUserService
    {
        Task<ApiResult<string>> Authenticate(LoginRequest request);
        Task<ApiResult<bool>> Register(RegisterRequest request);
        Task<ApiResult<bool>> Update(Guid id, UserUpdateRequest request);
        Task<ApiResult<bool>> Delete(Guid id);
        Task<ApiResult<PagedResult<UserViewModel>>> GetUserPaging(GetUserPagingRequest request);
        Task<ApiResult<UserViewModel>> GetById(Guid id);
        Task<ApiResult<bool>> RoleAssign(Guid id, RoleAssignRequest request);

        Task<bool> IsUserInRole(Guid userId, string roleName);

    }
}
