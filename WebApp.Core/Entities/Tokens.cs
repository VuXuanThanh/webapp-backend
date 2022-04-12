using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApp.Core.Entities
{
    public class Tokens
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }

        public DateTime Expires = DateTime.Now;
    }
}
