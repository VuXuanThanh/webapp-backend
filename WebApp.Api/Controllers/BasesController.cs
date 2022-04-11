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

    public class BasesController<T> : ControllerBase
    {
        protected IBaseRepository<T> _baseRepository;
        protected IBaseService<T> _baseService;
        protected ResponseResult _responseResult;

        public BasesController(IBaseRepository<T> baseRepository, IBaseService<T> baseService)
        {
            _baseRepository = baseRepository;
            _baseService = baseService;
            _responseResult = new ResponseResult();
        }

        /// <summary>
        /// api get all items in db
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [EnableCors("Policy")]
        public async Task<IActionResult> Get()
        {
            var res = await _baseService.GetAll();
            return Ok(res);

        }

        [HttpGet("{entityId}")]
        [EnableCors("Policy")]
        public async Task<IActionResult> GetById(string entityId)
        {
            try
            {
                var res = await _baseService.GetById(entityId);
                return Ok(res);

            }
            catch (Exception ex)
            {
                _responseResult.devMsg = ex.Message;
                _responseResult.Success = false;
                return StatusCode(500, _responseResult);
            }

        }


        [HttpGet("search")]
        public virtual async Task<IActionResult> Filter(string searchString, int pageSize, int pageIndex)
        {
            var res = await _baseService.Filter(searchString, pageSize, pageIndex);
            return Ok(res);
        }
    }
}
