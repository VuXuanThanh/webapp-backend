using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using WebApp.Core.Entities;
using WebApp.Core.Interface.Repository;

namespace WebApp.Infrastructure.Repository
{
    public class UsersRepository : BaseRepository<Users>, IUsersRepository
    {
        public UsersRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public bool checkRefreshToken(string accountId)
        {
            throw new NotImplementedException();
        }

        public string GenerateJSONWebToken(Users user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[] {
                new Claim("UserId", user.UserId),
               
                };
            var token = new JwtSecurityToken(_configuration["JWT:Issuer"],
                _configuration["JWT:Audience"],
                claims,
                null,
                expires: DateTime.Now.AddMinutes(5),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public UserToken GenerateRefreshToken(Users user, string ipAddress)
        {
            var rngCryptoServiceProvider = new RNGCryptoServiceProvider();
            var randomBytes = new byte[32];
            rngCryptoServiceProvider.GetBytes(randomBytes);

            var refToken = new UserToken
            {
                Token = Convert.ToBase64String(randomBytes),
                Expires = DateTime.Now.AddMinutes(10),
                CreateDate = DateTime.Now,
                CreateByIp = ipAddress
            };
            var sql = "Proc_InsertUserToken";
            var dynamicParam = new DynamicParameters();
            dynamicParam.Add("TokenId", Guid.NewGuid().ToString());
            dynamicParam.Add("Token", refToken.Token);
            dynamicParam.Add("Expires", refToken.Expires);
            dynamicParam.Add("IsExpired", false);
            dynamicParam.Add("CreateDate", refToken.CreateDate);
            dynamicParam.Add("CreateByIp", refToken.CreateByIp);
            dynamicParam.Add("RevokedDate", null);
            dynamicParam.Add("RevokedByIp", null);
            dynamicParam.Add("ReplacedByToken", null);
            dynamicParam.Add("IsActive", true);
            dynamicParam.Add("UserId", user.UserId);

            var res = sqlConnection.Execute(sql, commandType: System.Data.CommandType.StoredProcedure, param: dynamicParam);
            if (res > 0)
            {
                return refToken;
            }
            else
            {
                return null;
            }
        }

        public List<Users> GetAlls()
        {
            var sql = "select * from Users";
            var result = sqlConnection.Query<Users>(sql);
            return (List<Users>)result;
        }

        public Users Login(Users user)
        {
            var sql = "select * from Users where Email = @Email and Passwords= @Password";
            var dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("Email", user.Email);
            dynamicParameters.Add("Password", user.Password);
            var result = sqlConnection.Query<Users>(sql, param: dynamicParameters).ToList();
            if (result.Count() == 0)
                return null;
            else
                return (Users)(result[0]);
        }

        public UserToken RefreshToken(string token, string accountId)
        {
            var sql = "select * from UserToken where UserId= @UserId and Token= @Token";
            var dynamicParam = new DynamicParameters();
            dynamicParam.Add("UserId", accountId);
            dynamicParam.Add("Token", token);
            var res = sqlConnection.Query<UserToken>(sql, param: dynamicParam).ToList();
            return (UserToken)(res[0]);
        }
    }
}
