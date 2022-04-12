using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApp.Core.Entities
{
    public class UserToken
    {
        public string TokenId { get; set; }
        public string Token { get; set; }
        public DateTime Expires { get; set; }
        public bool IsExpired { get; set; }
        public string CreateByIp { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime RevokedDate{ get; set; }
        public string RevokedByIp { get; set; }
        public string ReplacedByToken { get; set; }
        public bool IsActive{ get; set; }
        public string UserId { get; set; }
    }
}
