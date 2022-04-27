using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApp.Core.Entities
{
    public class Orders
    {
        public string OrdersId { get; set; }
        public int StatusOrder { get; set; }
        public DateTime CreateDate { get; set; } 
        public int Payment { get; set; }
        public double Total { get; set; }
        public string UsersId { get; set; }
        public string DiscountId { get; set; } = null;

        public string ReceivingAddress { get; set; }

        public string Note { get; set; }
    }
}
