using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApp.Core.Entities
{
    public class CartItem
    {
        public string UsersId { get; set; }
        public string ProductId { get; set; }
        public double Price{ get; set; }
        public int Quanlity { get; set; }

        public bool StatusItem { get; set; } = false;


        [ColumnAttribute("Navigation")]
        public Product Product { get; set; }


    }
}
