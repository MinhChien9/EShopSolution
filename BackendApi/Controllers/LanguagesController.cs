using EShopSolution.Application.Catalog.Products;
using EShopSolution.Application.Common;
using EShopSolution.Application.System.Languages;
using EShopSolution.ViewModels.Catalog.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BackendApi.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class LanguagesController : Controller
    {

        private readonly ILanguageService _languageService;

        public LanguagesController(ILanguageService languageService)
        {
            _languageService = languageService;
        }

        [HttpGet()]
        public async Task<IActionResult> GetAllPaging(string languageId, [FromQuery] GetPublicProductPagingRequest request)
        {
            var languages = await _languageService.GetAll();

            return Ok(languages);
        }
    }
}
