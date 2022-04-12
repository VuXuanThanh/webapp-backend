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
        public IActionResult Login(Users acc, string ipAddress = "10.0.0.1")
        {
            var res = _usersService.Login(acc, ipAddress);
            if (res == null)
                return BadRequest();
            else
            {
                var x = res;
                setTokenCookie(x.JwtToken, x.RefreshToken);
                setUserCookie(x.UserId);
                return Ok(res);
            }

        }
        [HttpGet("user/refresh-token")]
        [EnableCors("Policy")]
        public IActionResult RefreshToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];
            var accountId = Request.Cookies["_user"];
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

        private void setTokenCookie(string accessToken, string refreshToken)
        {
            var cookieOptions = new CookieOptions
            {
                Secure = false,
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddMinutes(40),
                IsEssential = true

            };
            //Response.Cookies.Append("refreshToken", refreshToken, cookieOptions);
            Response.Cookies.Append("Authorization", accessToken, cookieOptions);
            Response.Cookies.Append("refreshToken", refreshToken, cookieOptions);

        }

        private void setUserCookie(string userId)
        {
            var cookieOptions = new CookieOptions
            {
                Secure = false,
                HttpOnly = false,
                Expires = DateTime.UtcNow.AddMinutes(40),
                IsEssential = true
            };

            Response.Cookies.Append("_user", userId, cookieOptions);

        }


    }
}
