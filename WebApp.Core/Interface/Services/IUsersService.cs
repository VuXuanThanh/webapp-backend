using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.Core.Entities;

namespace WebApp.Core.Interface.Services
{
    public interface IUsersService : IBaseService<Users>
    {
        AuthenticateResponse Login(AuthenticateRequest user, string ipAddress);
        List<Users> GetAlls();

        /// <summary>
        /// Generate access token: jwt
        /// </summary>
        /// <param name="acc"></param>
        /// <returns></returns>
        Tokens GenerateJSONWebToken(Users user);
        /// <summary>
        /// Get refresh token 
        /// </summary>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        UserToken GenerateRefreshToken(string ipAddress);
        Tokens RefreshToken(string token, string accountId);

        int Logout(string token, string userId);
    }
}
