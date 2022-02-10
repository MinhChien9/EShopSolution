using EShopSolution.Data.Entities;
using EShopSolution.ViewModels.Common;
using EShopSolution.ViewModels.System.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EShopSolution.Application.System.Users
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IConfiguration _config;
        public UserService(UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            RoleManager<AppRole> roleManager,
            IConfiguration config)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _config = config;
        }
        public async Task<ApiResult<string>> Authenticate(LoginRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);

            if (user == null)
                return new ApiErrorResult<string>("Tài khoản hoặc mật khẩu không hợp lệ");

            var result = await _signInManager.PasswordSignInAsync(user, request.Password, request.RememberMe, true);

            if (!result.Succeeded)
                return new ApiErrorResult<string>("Tài khoản hoặc mật khẩu không hợp lệ");

            var roles = await _userManager.GetRolesAsync(user);

            var claims = new[] {
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.GivenName,user.FirstName),
                new Claim(ClaimTypes.Role,string.Join(";",roles)),
                new Claim(ClaimTypes.Name,request.UserName)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Tokens:Issuer"],
                _config["Tokens:Issuer"],
                claims,
                expires: DateTime.Now.AddHours(3),
                signingCredentials: creds
                );

            return new ApiSuccessResult<string>(new JwtSecurityTokenHandler().WriteToken(token));


        }

        public async Task<ApiResult<bool>> Delete(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());

            if (user == null)
                return new ApiErrorResult<bool>("Người dùng không tồn tại");

            var result = await _userManager.DeleteAsync(user);

            if (result.Succeeded)
                return new ApiSuccessResult<bool>();

            return new ApiErrorResult<bool>("Xóa không thành công");
        }

        public async Task<ApiResult<UserViewModel>> GetById(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());

            if (user != null)
                return new ApiSuccessResult<UserViewModel>(
                    new UserViewModel()
                    {
                        Email = user.Email,
                        FirstName = user.FirstName,
                        Id = id,
                        LastName = user.LastName,
                        PhoneNumber = user.PhoneNumber,
                        Dob = user.Dob,
                        UserName = user.UserName
                    }
                );

            return new ApiErrorResult<UserViewModel>("Người dùng không tồn tại");
        }

        public async Task<ApiResult<PagedResult<UserViewModel>>> GetUserPaging(GetUserPagingRequest request)
        {
            var query = _userManager.Users;

            if (!string.IsNullOrEmpty(request.Keyword))
            {
                query = query.Where(u => u.NormalizedUserName.Contains(request.Keyword.Normalize()) ||
                  u.FirstName.Contains(request.Keyword) ||
                  u.LastName.Contains(request.Keyword) ||
                  u.NormalizedEmail.Contains(request.Keyword.Normalize()) ||
                  u.PhoneNumber.Contains(request.Keyword) ||
                  u.UserName.Contains(request.Keyword));
            }

            int totalRow = await query.CountAsync();

            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(u => new UserViewModel()
                {
                    FirstName = u.FirstName,
                    Email = u.Email,
                    Id = u.Id,
                    LastName = u.LastName,
                    PhoneNumber = u.PhoneNumber,
                    UserName = u.UserName
                }
                ).ToListAsync();

            var pagedResult = new PagedResult<UserViewModel>()
            {
                TotalRecords = totalRow,
                PageSize = request.PageSize,
                PageIndex = request.PageIndex,
                Items = data
            };

            return new ApiSuccessResult<PagedResult<UserViewModel>>(pagedResult);
        }

        public async Task<ApiResult<bool>> Register(RegisterRequest request)
        {
            if (await _userManager.FindByNameAsync(request.UserName) != null)
                return new ApiErrorResult<bool>("Tài khoản đã tồn tại");

            if (await _userManager.FindByEmailAsync(request.Email) != null)
                return new ApiErrorResult<bool>("Email đã tồn tại");

            var user = new AppUser()
            {
                Dob = request.DoB,
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.UserName,
                PhoneNumber = request.PhoneNumber
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            if (result.Succeeded)
                return new ApiSuccessResult<bool>();

            return new ApiErrorResult<bool>("Đăng ký không thành công");
        }

        public async Task<ApiResult<bool>> Update(Guid id, UserUpdateRequest request)
        {

            if (await _userManager.Users.AnyAsync(u => u.Id != id && u.Email == request.Email))
                return new ApiErrorResult<bool>("Email đã tồn tại");

            var user = await _userManager.FindByIdAsync(id.ToString());

            user.Dob = request.DoB;
            user.Email = request.Email;
            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.PhoneNumber = request.PhoneNumber;

            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
                return new ApiSuccessResult<bool>();

            return new ApiErrorResult<bool>("Cập nhật không thành công");
        }
    }
}
