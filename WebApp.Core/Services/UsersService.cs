﻿using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.Core.Entities;
using WebApp.Core.Interface.Repository;
using WebApp.Core.Interface.Services;

namespace WebApp.Core.Services
{
    public class UsersService : BaseService<Users>, IUsersService
    {
        IUsersRepository _usersRepository;

        public UsersService(IUsersRepository usersRepository) : base(usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public string GenerateJSONWebToken(Users user)
        {
            throw new NotImplementedException();
        }

        public UserToken GenerateRefreshToken(string ipAddress)
        {
            throw new NotImplementedException();
        }

        public List<Users> GetAlls()
        {
            var res = _usersRepository.GetAlls();
            return res;
        }

        public AuthenticateResponse Login(Users user, string ipAddress)
        {
            var result = _usersRepository.Login(user);
            if (result == null)
                return null;

            var accessToken = _usersRepository.GenerateJSONWebToken(result);
            var refreshToken = _usersRepository.GenerateRefreshToken(result, ipAddress);

            //var handler = new JwtSecurityTokenHandler();
            //var decodedValue = handler.ReadJwtToken(accessToken);
            AuthenticateResponse authenticateResponse = new AuthenticateResponse(result.UserId, result.Email,
                accessToken, refreshToken.Token);

            return authenticateResponse;
        }

        public Tokens RefreshToken(string token, string accountId)
        {
            var res = _usersRepository.RefreshToken(token, accountId);
            var isExpried = DateTime.Compare(res.Expires, DateTime.Now);
            if (isExpried > 0)
            {
                var acc = new Users();
                acc.UserId = accountId;
                var accessToken = _usersRepository.GenerateJSONWebToken(acc);
                var newToken = new Tokens();
                newToken.Token = accessToken;
                newToken.RefreshToken = token;
                return newToken;
            }
            else
            {
                return null;
            }
        }
    }
}
