using EShopSolution.Data.Entities;
using EShopSolution.ViewModels.Common;
using EShopSolution.ViewModels.System.Roles;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShopSolution.Application.System.Roles
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<AppRole> _roleManager;
        public RoleService(RoleManager<AppRole> roleManager)
        {

            _roleManager = roleManager;
        }

        public async Task<ApiResult<List<RoleViewModel>>> GetAll()
        {
            var roles = await _roleManager.Roles.Select(r => new RoleViewModel()
            {
                Description = r.Description,
                Id = r.Id,
                Name = r.Name
            }).ToListAsync();

            return new ApiSuccessResult<List<RoleViewModel>>(roles);
        }
    }
}
