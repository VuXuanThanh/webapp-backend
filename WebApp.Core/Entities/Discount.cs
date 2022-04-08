using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApp.Core.Entities
{
    public class Discount
    {
        public string DiscountId { get; set; }
        public int DiscountPercent { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool Active { get; set; }
    }
}
