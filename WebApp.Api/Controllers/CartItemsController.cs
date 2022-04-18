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
    public class CartItemsController : ControllerBase
    {
        ICartItemRepository _cartItemRepository;
        ICartItemService _cartItemService;
        ResponseResult _responseResult;

        public CartItemsController(ICartItemRepository cartItemRepository, ICartItemService cartItemService)
        {
            _cartItemRepository = cartItemRepository;
            _cartItemService = cartItemService;
            _responseResult = new ResponseResult();
        }

        [HttpPost]
        [EnableCors("Policy")]
        public async Task<IActionResult> Post(CartItem cartItem)
        {
            try
            {

                var res = await _cartItemService.Insert(cartItem);
                if (res > 0)
                {
                    _responseResult.Success = true;
                    _responseResult.data = "Sản phẩm này đã được thêm vào giỏ hàng của bạn";
                    return Ok(_responseResult);
                }
                else if (res == -1)
                {
                    _responseResult.userMsg = "Sản phẩm này đã tồn tại trong giỏ hàng của bạn";
                }
                else
                {
                    _responseResult.userMsg = "Không thể thêm được sản phẩm vào giỏ";
                }
                return BadRequest(_responseResult);
            }
            catch (Exception ex)
            {
                _responseResult.userMsg = "Đã có lỗi xảy ra vui lòng thử lại sau";
                _responseResult.devMsg = ex.Message;
                _responseResult.Success = false;
                return StatusCode(500, _responseResult);
            }

        }

        [HttpDelete]
        [EnableCors("Policy")]
        public async Task<IActionResult> Delete(string userId, string productId)
        {
            try
            {
                var res = await _cartItemService.Delete(userId, productId);
                if (res > 0)
                {
                    return Ok();
                }
                else
                {
                    return NoContent();
                }
            }
            catch (Exception ex)
            {
                _responseResult.devMsg = ex.Message;
                _responseResult.Success = false;
                return StatusCode(500, _responseResult);
            }
        }

        [HttpGet("{userId}/records")]
        [EnableCors("Policy")]
        public async Task<IActionResult> GetSumRecords(string userId)
        {
            try
            {
                var res = await _cartItemService.GetAllRecord(userId);
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

        [HttpGet("{userId}")]
        [EnableCors("Policy")]
        public async Task<IActionResult> GetByUserId(string userId)
        {
            try
            {
                var res = await _cartItemService.GetCartItemsByUserId(userId);
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

    }
}
