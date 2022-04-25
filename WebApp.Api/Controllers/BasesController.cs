using Microsoft.AspNetCore.Authorization;
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
        protected Role role;

        public BasesController(IBaseRepository<T> baseRepository, IBaseService<T> baseService)
        {
            _baseRepository = baseRepository;
            _baseService = baseService;
            _responseResult = new ResponseResult();
            role = new Role();
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

        [HttpPost]
        [EnableCors("Policy")]
        [Authorize]

        public async Task<IActionResult> Post(T entity)
        {
            try
            {
                if(!CheckPolicyAuthorization(Role.Administrator))
                {
                    return Unauthorized("You don’t have permission to access");
                }
                var res = await _baseService.Insert(entity);
                if (res > 0)
                {
                    _responseResult.Success = true;
                    _responseResult.data = res;

                }
                return Ok(_responseResult);

            }
            catch (Exception ex)
            {
                _responseResult.devMsg = ex.Message;
                _responseResult.Success = false;
                return StatusCode(500, _responseResult);
            }
        }

        [HttpDelete("{entityId}")]
        [EnableCors("Policy")]
        public async Task<IActionResult> Delete(string entityId)
        {
            try
            {
                if (!CheckPolicyAuthorization(Role.Administrator))
                {
                    return Unauthorized("You don’t have permission to access");
                }
                var res = await _baseService.Delete(entityId);
                if (res > 0)
                    return Ok();
                else
                    return NoContent();
            }
            catch (Exception ex)
            {
                _responseResult.userMsg = "Đã có lỗi xảy ra vui lòng thử lại sau";
                _responseResult.devMsg = ex.Message;
                _responseResult.Success = false;
                return StatusCode(500, _responseResult);
            }

        }


        [HttpPut("")]
        [EnableCors("Policy")]
        [Authorize]
        public async Task<IActionResult> Update(T entity)
        {
            try
            {
                if (!CheckPolicyAuthorization(Role.Administrator))
                {
                    return Unauthorized("You don’t have permission to access");
                }
                var res = await _baseService.Update(entity);
                if (res > 0)
                    return Ok(entity);
                else
                    return NoContent();
            }
            catch (Exception ex)
            {
                _responseResult.userMsg = "Đã có lỗi xảy ra vui lòng thử lại sau";
                _responseResult.devMsg = ex.Message;
                _responseResult.Success = false;
                return StatusCode(500, _responseResult);
            }
        }

        private bool CheckPolicyAuthorization(string key)
        {
            var role = (key!=null) ? Request.Cookies["_role"].ToString(): null;
            return (role == key) ? true : false;

        }


        //[HttpGet("search")]
        //public virtual async Task<IActionResult> Filter(string searchString, int pageSize, int pageIndex)
        //{
        //    var res = await _baseService.Filter(searchString, pageSize, pageIndex);
        //    return Ok(res);
        //}
    }
}
