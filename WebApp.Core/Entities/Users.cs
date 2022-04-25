using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApp.Core.Entities
{
    public class Users
    {
        public string UsersId { get; set; }
        public string UserName { get; set; }
        public string Passwords { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string RoleId { get; set; }
        public bool IsActive{ get; set; }

        [ColumnAttribute("Navigation")]
        public string RoleName { get; set; }


    }
}
