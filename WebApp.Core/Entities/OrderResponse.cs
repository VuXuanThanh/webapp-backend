using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApp.Core.Entities
{
    public class OrderResponse
    {
        public int SuccessResult { get; set; }
        public string SuccessOrderID { get; set; }

        public OrderResponse(int result, string orderId)
        {
            SuccessResult = result;
            SuccessOrderID = orderId;

        }
    }
}
