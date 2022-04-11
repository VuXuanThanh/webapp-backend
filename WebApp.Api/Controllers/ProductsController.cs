using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Core.Entities;
using WebApp.Core.Interface.Repository;
using WebApp.Core.Interface.Services;
using WebApp.Core.Services;

namespace WebApp.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductsController : BasesController<Product>
    {
        IProductRepository _productRepository;
        IProductService _productService;
        public ProductsController(IProductRepository productRepository, IProductService productService)
            : base(productRepository, productService)
        {
            _productRepository = productRepository;
            _productService = productService;
        }

        [HttpGet("filter")]
        [EnableCors("Policy")]
        public async Task<IActionResult> Filter(string categoryId, string productName,
            double priceMin, double priceMax, string brandId,
            int pageSize = 24, int pageIndex = 1)
        {
            var res = await _productService.Filter(categoryId, productName, priceMin, priceMax,
                brandId, pageSize, pageIndex);
            return Ok(res);
        }


        [HttpGet("filter1")]
        [EnableCors("Policy")]
        public async Task<IActionResult> Filter1(int type, string categoryId, string productName,
            double priceMin, double priceMax, string brandId,
            int pageSize = 24, int pageIndex = 1)
        {
            try
            {
                var res = await _productService.Filter1(type, categoryId, productName, priceMin, priceMax,
                    brandId, pageSize, pageIndex);
                return Ok(res);
            }
            catch (Exception ex)
            {
                _responseResult.devMsg = ex.Message;
                _responseResult.Success = false;
                return StatusCode(500, _responseResult);
            }
        }

    }
}
