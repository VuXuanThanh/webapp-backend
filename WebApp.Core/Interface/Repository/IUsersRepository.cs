using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.Core.Entities;

namespace WebApp.Core.Interface.Repository
{
    public interface IUsersRepository : IBaseRepository<Users>
    {
        Users Login(AuthenticateRequest user);
        List<Users> GetAlls();
        Tokens GenerateJSONWebToken(Users user);

        UserToken GenerateRefreshToken(Users user, string ipAddress);
        UserToken RefreshToken(string token, string accountId);

        bool checkRefreshToken(string accountId);

        int Logout(string token, string userId);
    }
}
