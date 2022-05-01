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
    public class UsersController : BasesController<Users>
    {
        IUsersRepository _usersRepository;
        IUsersService _usersService;

        public UsersController(IUsersRepository usersRepository, IUsersService usersService) : base(usersRepository, usersService)
        {
            _usersRepository = usersRepository;
            _usersService = usersService;
        }


        [HttpGet("users")]
        [EnableCors("Policy")]
        [Authorize]
        public IActionResult Get123()
        {
            var res = _usersService.GetAlls();
            return Ok(res);
        }

        [HttpPost("login")]
        [EnableCors("Policy")]
        public IActionResult Login(AuthenticateRequest acc)
        {
            try
            {
                var ipAddress = "10.0.0.1";
                var res = _usersService.Login(acc, ipAddress);
                if (res == null)
                {
                    _responseResult.userMsg = "Thông tin email hoặc mật khẩu không chính xác";
                    return BadRequest(_responseResult);

                }
                else
                {
                    setTokenCookie(res.JwtToken, res.RefreshToken);
                    setUserCookie(res.UserId, res.UserName, res.Expires,res.RoleName);
                    var responseAuthen = new
                    {
                        UserId = res.UserId,
                        UserName = res.UserName

                    };
                    _responseResult.Success = true;
                    _responseResult.data = responseAuthen;
                    return Ok(_responseResult);
                }
            }
            catch (Exception ex)
            {
                _responseResult.devMsg = ex.Message;
                _responseResult.userMsg = "Đã có lỗi xảy ra vui lòng thử lại sau";
                return StatusCode(500, _responseResult);
            }


        }


        [HttpGet("logout")]
        [EnableCors("Policy")]
        public IActionResult Logout()
        {
            try
            {
                var refreshToken = Request.Cookies["refreshToken"];
                var accountId = Request.Cookies["_userId"];
                var res = _usersService.Logout(refreshToken, accountId);
                if (res == 0)
                    return BadRequest(_responseResult);
                else
                {
                    Response.Cookies.Delete("token");
                    Response.Cookies.Delete("refreshToken");
                    Response.Cookies.Delete("_userId");
                    return Ok(res);

                }
            }
            catch (Exception ex)
            {
                _responseResult.devMsg = ex.Message;
                _responseResult.userMsg = "Đã có lỗi xảy ra vui lòng thử lại sau";
                return StatusCode(500, _responseResult);
            }
        }


        [HttpGet("refresh-token")]
        [EnableCors("Policy")]
        public IActionResult RefreshToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];
            var accountId = Request.Cookies["_userId"];
            var res = _usersService.RefreshToken(refreshToken, accountId);
            if (res == null)
            {
                return StatusCode(401, "required login again");
            }
            else
            {
                setTokenCookie(res.Token, res.RefreshToken);
                return Ok(res);
            }
        }

        [HttpGet("roles")]
        [EnableCors("Policy")]
        [Authorize]
        public IActionResult GetRoles()
        {
            try
            {
                var res = _usersRepository.GetUserRoles();
                return Ok(res);
            }
            catch (Exception ex)
            {
                _responseResult.devMsg = ex.Message;
                _responseResult.userMsg = "Đã có lỗi xảy ra vui lòng thử lại sau";
                return StatusCode(500, _responseResult);
            }
        }

        [HttpGet("userId/roles/{value}")]
        [EnableCors("Policy")]
        [Authorize]
        public IActionResult CheckRoleUserId(int value)
        {
            try
            {
                var userId = Request.Cookies["_userId"];
                var res = _usersRepository.CheckUserPolicyRole(userId);
                if (res.Permission >= value)
                    return Ok();
                else
                {
                    Response.Cookies.Delete("_userId");
                    return Unauthorized();
                }

            }
            catch (Exception ex)
            {
                _responseResult.devMsg = ex.Message;
                _responseResult.userMsg = "Đã có lỗi xảy ra vui lòng thử lại sau";
                return StatusCode(500, _responseResult);
            }
        }


        private void setTokenCookie(string accessToken, string refreshToken)
        {
            var cookieOptions = new CookieOptions
            {
                Secure = false,
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddHours(3),
                IsEssential = false

            };
            //Response.Cookies.Append("refreshToken", refreshToken, cookieOptions);
            Response.Cookies.Append("token", accessToken, cookieOptions);
            Response.Cookies.Append("refreshToken", refreshToken, new CookieOptions
            {
                Secure = false,
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(3),
                IsEssential = false

            });

        }

        private void setUserCookie(string userId, string userName, DateTime tokenExpries, string role)
        {
            var cookieOptions = new CookieOptions
            {
                Secure = false,
                HttpOnly = false,
                Expires = DateTime.Now.AddDays(7),
                IsEssential = true
            };

            Response.Cookies.Append("_userId", userId, cookieOptions);
            Response.Cookies.Append("_user", userName, cookieOptions);
            Response.Cookies.Append("_tokenExpries", tokenExpries.ToString(), cookieOptions);
            Response.Cookies.Append("_role", role, cookieOptions);
        }


    }
}
