using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApp.Core.Entities
{
    public class Product
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public string Summary { get; set; }
        public string Descriptions { get; set; }
        public double PriceOrgin { get; set; }
        public bool StatusProduct { get; set; }
        public string Material { get; set; }
        public string Accessory { get; set; }
        public string Weights { get; set; }
        public string Dimension { get; set; }
        public int Star { get; set; }
        public string Powers { get; set; }
        public string Orgin { get; set; }
        public string BrandId { get; set; }
        public string CategoryId { get; set; }
        public double PriceDeal { get; set; }


    }
}
