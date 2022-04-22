using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApp.Core.Entities
{
    public class ProductItem
    {
        public string UserId { get; set; }
        public string ProductId { get; set; }
        public int Quanlity { get; set; }

        public bool StatusItem { get; set; }

    }
}
