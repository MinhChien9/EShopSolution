
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
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IStorageService _storageService;

        public ProductsController(IProductService productService, IStorageService storageService)
        {
            _productService = productService;
            _storageService = storageService;
        }

        [HttpGet("{languageId}")]
        public async Task<IActionResult> GetAllPaging(string languageId, [FromQuery] GetPublicProductPagingRequest request)
        {
            var products = await _productService.GetAllByCategory(languageId, request);

            return Ok(products);
        }
        [HttpGet("paging")]
        public async Task<IActionResult> GetAllPaging( [FromQuery] GetManageProductPagingRequest request)
        {
            var products = await _productService.GetAllPaging( request);

            return Ok(products);
        }
        [HttpGet("{productId}/{languageId}")]
        public async Task<IActionResult> GetById(int productId, string languageId)
        {
            var product = await _productService.GetById(productId, languageId);

            if (product == null)
                return BadRequest($"Can not find product {productId}");

            return Ok(product);
        }

        [HttpPost()]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Create([FromForm] ProductCreateRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var productId = await _productService.Create(request);

            if (productId == 0)
                return BadRequest();

            var product = await _productService.GetById(productId, request.LanguageId);

            return CreatedAtAction("GetById", new { productId = product.Id, languageId = product.LanguageId }, product);
        }

        [HttpPut()]
        public async Task<IActionResult> Update([FromForm] ProductUpdateRequest request)
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var effectedResult = await _productService.Update(request);

            if (effectedResult == 0)
                return BadRequest();

            return Ok();
        }

        [HttpDelete("{productId}")]
        public async Task<IActionResult> Delete(int productId)
        {
            var effectedResult = await _productService.Delete(productId);

            if (effectedResult == 0)
                return BadRequest();

            return Ok();
        }

        [HttpPatch("price/{productId}/{price}")]
        public async Task<IActionResult> UpdatePrice(int productId, decimal price)
        {
            var isSuccessfull = await _productService.UpdatePrice(productId, price);

            if (!isSuccessfull)
                return BadRequest();

            return Ok();

        }

        [HttpPut("stock/{productId}/{quantity}")]
        public async Task<IActionResult> UpdateStock([FromQuery] int productId, int quantity)
        {
            var isSuccessfull = await _productService.UpdateStock(productId, quantity);

            if (!isSuccessfull)
                return BadRequest();

            return Ok();
        }

        [HttpPut("increaseViewCount/{productId}")]
        public async Task<IActionResult> IncreaseViewCount(int productId)
        {
            var isSuccessfull = await _productService.IncreaseViewCount(productId);

            if (isSuccessfull)
                return BadRequest();

            return Ok();
        }

        [HttpPost("{productId}/images")]
        public async Task<IActionResult> AddImages(int productId, [FromForm] ProductImageCreateRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var imageId = await _productService.AddImage(productId, request);

            if (imageId == 0)
                return BadRequest();

            var image = await _productService.GetImageById(imageId);

            return CreatedAtAction(nameof(GetImageById), new { productId = productId, imageId = image }, image);
        }

        [HttpPut("{productId}/images/{imageId}")]
        public async Task<IActionResult> UpdateImage(int imageId, [FromForm] ProductImageUpdateRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _productService.UpdateImage(imageId, request);

            if (result == 0)
                return BadRequest();

            return Ok();
        }

        [HttpDelete("{productId}/images/{imageId}")]
        public async Task<IActionResult> DeleteImage(int imageId)
        {
            var result = await _productService.RemoveImage(imageId);

            if (result == 0)
                return BadRequest();

            return Ok();
        }

        [HttpGet("{productId}/images/{imageId}")]
        public async Task<IActionResult> GetImageById(int productId, int imageId)
        {
            var image = await _productService.GetImageById(imageId);

            if (image == null)
                return BadRequest($"Can not find image with id {imageId}");

            return Ok(image);
        }

    }
}
