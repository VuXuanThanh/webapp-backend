using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Core.Interface.Repository;
using WebApp.Core.Interface.Services;

namespace WebApp.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BasesController<T> : ControllerBase
    {
        protected IBaseRepository<T> _baseRepository;
        protected IBaseService<T> _baseService;

        public BasesController(IBaseRepository<T> baseRepository, IBaseService<T> baseService)
        {
            _baseRepository = baseRepository;
            _baseService = baseService;
        }

        [HttpGet]
        public IActionResult Get()
        {
          
            int x = _baseService.Insert();
            return Ok(x);

        }
    }
}
