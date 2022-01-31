
using EShopSolution.Application.Catalog.Products;
using EShopSolution.Application.Common;
using EShopSolution.ViewModels.Catalog.ProductImages;
using EShopSolution.ViewModels.Catalog.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class ProductsController : ControllerBase
    {
        private readonly IPublicProductService _publicProductService;
        private readonly IManageProductService _manageProductService;
        private readonly IStorageService _storageService;

        public ProductsController(IPublicProductService publicProductService, IManageProductService manageProductService, IStorageService storageService)
        {
            _publicProductService = publicProductService;
            _manageProductService = manageProductService;
            _storageService = storageService;
        }


        [HttpGet("{languageId}")]
        public async Task<IActionResult> GetAllPaging(string languageId, [FromQuery] GetPublicProductPagingRequest request)
        {
            var products = await _publicProductService.GetAllByCategory(languageId, request);

            return Ok(products);
        }

        [HttpGet("{productId}/{languageId}")]
        public async Task<IActionResult> GetById(int productId, string languageId)
        {
            var product = await _manageProductService.GetById(productId, languageId);

            if (product == null)
                return BadRequest($"Can not find product {productId}");

            return Ok(product);
        }

        [HttpPost()]
        public async Task<IActionResult> Create([FromForm] ProductCreateRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var productId = await _manageProductService.Create(request);

            if (productId == 0)
                return BadRequest();

            var product = await _manageProductService.GetById(productId, request.LanguageId);

            return CreatedAtAction("GetById", new { productId = product.Id, languageId = product.LanguageId }, product);
        }

        [HttpPut()]
        public async Task<IActionResult> Update([FromForm] ProductUpdateRequest request)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var effectedResult = await _manageProductService.Update(request);

            if (effectedResult == 0)
                return BadRequest();

            return Ok();
        }

        [HttpDelete("{productId}")]
        public async Task<IActionResult> Delete(int productId)
        {
            var effectedResult = await _manageProductService.Delete(productId);

            if (effectedResult == 0)
                return BadRequest();

            return Ok();
        }

        [HttpPatch("price/{productId}/{price}")]
        public async Task<IActionResult> UpdatePrice(int productId, decimal price)
        {
            var isSuccessfull = await _manageProductService.UpdatePrice(productId, price);

            if (!isSuccessfull)
                return BadRequest();

            return Ok();

        }

        [HttpPut("stock/{productId}/{quantity}")]
        public async Task<IActionResult> UpdateStock([FromQuery] int productId, int quantity)
        {
            var isSuccessfull = await _manageProductService.UpdateStock(productId, quantity);

            if (!isSuccessfull)
                return BadRequest();

            return Ok();
        }

        [HttpPut("increaseViewCount/{productId}")]
        public async Task<IActionResult> IncreaseViewCount(int productId)
        {
            var isSuccessfull = await _manageProductService.IncreaseViewCount(productId);

            if (isSuccessfull)
                return BadRequest();

            return Ok();
        }

        [HttpPost("{productId}/images")]
        public async Task<IActionResult> AddImages(int productId, [FromForm] ProductImageCreateRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var imageId = await _manageProductService.AddImage(productId, request);

            if (imageId == 0)
                return BadRequest();

            var image = await _manageProductService.GetImageById(imageId);

            return CreatedAtAction(nameof(GetImageById), new { productId = productId, imageId = image }, image);
        }

        [HttpPut("{productId}/images/{imageId}")]
        public async Task<IActionResult> UpdateImage(int imageId, [FromForm] ProductImageUpdateRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _manageProductService.UpdateImage(imageId, request);

            if (result == 0)
                return BadRequest();

            return Ok();
        }

        [HttpDelete("{productId}/images/{imageId}")]
        public async Task<IActionResult> DeleteImage(int imageId)
        {
            var result = await _manageProductService.RemoveImage(imageId);

            if (result == 0)
                return BadRequest();

            return Ok();
        }

        [HttpGet("{productId}/images/{imageId}")]
        public async Task<IActionResult> GetImageById(int productId, int imageId)
        {
            var image = await _manageProductService.GetImageById(imageId);

            if (image == null)
                return BadRequest($"Can not find image with id {imageId}");

            return Ok(image);
        }
    }
}
