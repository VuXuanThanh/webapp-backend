﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApp.Core.Entities
{
    public class AuthenticateRequest
    {
        public string Email { get; set; }
        public string Passwords { get; set; }

        public int IsManage { get; set; }
    }
}
