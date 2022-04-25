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
    public class BrandsController : BasesController<Brand>
    {
        IBrandRepository _brandRepository;
        IBrandService _brandService;

        public BrandsController(IBrandRepository brandRepository, IBrandService brandService)
            :base(brandRepository, brandService)
        {
            _brandRepository = brandRepository;
            _brandService = brandService;
        }
    }
}
