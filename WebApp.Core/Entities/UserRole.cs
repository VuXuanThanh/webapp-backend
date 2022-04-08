using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApp.Core.Entities
{
    public class UserRole
    {
        public string RoleId { get; set; }
        public string RoleName { get; set; }
        public int Permission { get; set; }
    }
}
