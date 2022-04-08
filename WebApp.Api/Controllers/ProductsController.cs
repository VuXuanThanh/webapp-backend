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
            :base(productRepository, productService)
        {
            _productRepository = productRepository;
            _productService = productService;
        }
        
    }
}
