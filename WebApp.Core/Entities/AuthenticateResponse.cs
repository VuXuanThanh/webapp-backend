﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApp.Core.Entities
{
    public class AuthenticateResponse
    {
        public string UserId{ get; set; }
        public string UserName { get; set; }
        public string JwtToken { get; set; }

        public string RefreshToken { get; set; }

        public AuthenticateResponse(string userId, string userName, string jwtToken, string refreshToken)
        {
            UserId = userId;
            UserName = userName;
            JwtToken = jwtToken;
            RefreshToken = refreshToken;
        }
    }
}

