using EShopSolution.Utilities.Constants;
using EShopSolution.ViewModels.Catalog.Products;
using EShopSolution.ViewModels.Common;
using EShopSolution.ViewModels.System.Languages;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace EShopSolution.AdminApp.Services
{
    public class ProductApiClient : BaseApiClient, IProductApiClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ProductApiClient(IHttpClientFactory httpClientFactory, IConfiguration configuration, IHttpContextAccessor httpContextAccessor) :
            base(httpClientFactory, configuration, httpContextAccessor)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;

        }

        public Task<ApiResult<bool>> CreateProduct(ProductCreateRequest request)
        {
            var sessions = _httpContextAccessor.HttpContext.Session.GetString(SystemConstant.AppSettings.Token);

            using var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration[SystemConstant.AppSettings.BaseAddress]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);

            using var requestContent = new MultipartFormDataContent();

            requestContent.Add

        }

        public async Task<PagedResult<ProductViewModel>> GetProductsPaging(GetManageProductPagingRequest request)
        {
            var products = await GetAsync<PagedResult<ProductViewModel>>($"/api/products/paging?pageIndex={request.PageIndex}" +
                $"&pageSize={request.PageSize}" +
                $"&keyword={request.KeyWord}" +
                $"&languageId={request.LanguageId}");
            return products;

        }
    }
}
