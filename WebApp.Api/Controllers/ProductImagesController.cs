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

namespace WebApp.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductImagesController : BasesController<ProductImage>
    {
        IProductImageRepository _productImageRepository;
        IProductImageService _productImageService;

        public ProductImagesController(IProductImageRepository productImageRepository, IProductImageService productImageService):base(productImageRepository, productImageService)
        {
            _productImageRepository = productImageRepository;
            _productImageService = productImageService;
        }

        [HttpGet("productId/{productId}")]
        [EnableCors("Policy")]
        public IActionResult GetImageByProduct(string productId)
        {
            try
            {
                var res = _productImageService.GetImageByProductId(productId);
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
