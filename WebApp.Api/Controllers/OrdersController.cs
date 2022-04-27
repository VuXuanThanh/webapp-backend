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
    public class OrdersController : ControllerBase
    {
        IOrdersRepository _ordersRepository;
        IOrdersService _ordersService;
        IOrderItemService _orderItemService;
        ResponseResult _responseResult;
       

        public OrdersController(IOrdersRepository ordersRepository, IOrdersService ordersService, IOrderItemService orderItemService)
        {
            _ordersRepository = ordersRepository;
            _ordersService = ordersService;
            _orderItemService = orderItemService;
            _responseResult = new ResponseResult();
            

        }

        [Authorize]
        [HttpPost]
        [EnableCors("Policy")]
        public async Task<IActionResult> Post(Orders order)
        {
            try
            {
                order.CreateDate = DateTime.Now;
                var res = await _ordersRepository.Insert(order);


                if (res.SuccessResult > 0)
                {
                    var res2 = await _orderItemService.Insert(res.SuccessOrderID);
                    if (res2 > 0)
                        return Ok();
                    else
                        return BadRequest();
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                _responseResult.userMsg = "Đã có lỗi xảy ra vui lòng thử lại sau";
                _responseResult.devMsg = ex.Message;
                _responseResult.Success = false;
                return StatusCode(500, _responseResult);
            }
        }

        [HttpGet]
        [EnableCors("Policy")]
        public async Task<IActionResult> Get()
        {
            try
            {
                var res = await _ordersRepository.Filter();
                return Ok(res);
            }
            catch (Exception ex)
            {

                _responseResult.userMsg = "Đã có lỗi xảy ra vui lòng thử lại sau";
                _responseResult.devMsg = ex.Message;
                _responseResult.Success = false;
                return StatusCode(500, _responseResult);
            }
        }

        [HttpPut]
        [EnableCors("Policy")]
        public async Task<IActionResult> Put(Orders order)
        {
            try
            {
                var res = await _ordersRepository.Update(order);
                if (res > 0)
                    return Ok(res);
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

        //[HttpDelete("{orderId}")]
        //[EnableCors("Policy")]
        //public async Task<IActionResult> Delete(string orderId)
        //{
        //    try
        //    {
        //        var res = await _ordersRepository.Delete(orderId);
        //        if (res > 0)
        //            return Ok(res);
        //        else
        //            return NoContent();

        //    }
        //    catch (Exception ex)
        //    {
        //        _responseResult.userMsg = "Đã có lỗi xảy ra vui lòng thử lại sau";
        //        _responseResult.devMsg = ex.Message;
        //        _responseResult.Success = false;
        //        return StatusCode(500, _responseResult);
        //    }
        //}

    }
}
