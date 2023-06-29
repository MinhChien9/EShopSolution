using EShopSolution.AdminApp.Services;
using EShopSolution.Utilities.Constants;
using EShopSolution.ViewModels.Catalog.Products;
using EShopSolution.ViewModels.System.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Reflection.Metadata;
using System.Threading.Tasks;

namespace EShopSolution.AdminApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductApiClient _productApiClient;
        private readonly IConfiguration _configuration;

        public ProductController(IProductApiClient productApiClient, IConfiguration configuration)
        {
            _productApiClient = productApiClient;
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string keyword, int pageIndex = 1, int pageSize = 10)
        {
            var languageId = HttpContext.Session.GetString(SystemConstant.AppSettings.DefaultLanguageId);
            var request = new GetManageProductPagingRequest()
            {
                KeyWord = keyword,
                PageIndex = pageIndex,
                PageSize = pageSize,
                LanguageId = languageId
            };

            if (TempData["result"] != null)
            {
                ViewBag.SuccessMsg = TempData["result"];
            }
            ViewBag.Keyword = keyword;

            var data = await _productApiClient.GetProductsPaging(request);

            return View(data);
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Create([FromForm] ProductCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var result = await _productApiClient.CreateProduct(request);

            if (result.IsSuccessed)
            {
                TempData["result"] = "Thêm mới sản phẩm thành công.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", result.Message);

            return View();

        }
        [HttpGet]
        public ActionResult Create()
        {

            return View();

        }


    }
}
