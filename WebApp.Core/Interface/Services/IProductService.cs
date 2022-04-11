using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.Core.Entities;

namespace WebApp.Core.Interface.Services
{
    public interface IProductService:IBaseService<Product>
    {
        public Task<Object> Filter(string categoryId, string productName,
            double priceMin, double priceMax, string brandId,
            int pageSize=1, int pageIndex=24);

        public Task<Object> Filter1(int type, string categoryId, string productName,
            double priceMin, double priceMax, string brandId,
            int pageSize, int pageIndex);
    }
}
