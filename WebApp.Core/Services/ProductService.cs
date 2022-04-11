using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.Core.Entities;
using WebApp.Core.Interface.Repository;
using WebApp.Core.Interface.Services;

namespace WebApp.Core.Services
{
    public class ProductService : BaseService<Product>, IProductService
    {
        IProductRepository _productRepository;
        public ProductService(IProductRepository productRepository):base(productRepository)
        {
            _productRepository = productRepository;
        }

        public Task<object> Filter(string categoryId, string productName, double priceMin, double priceMax, string brandId, int pageSize=1, int pageIndex=24)
        {
            var res = _productRepository.Filter(categoryId, productName, priceMin, priceMax, brandId, pageSize, pageIndex);
            return res;
        }

        public Task<object> Filter1(int type, string categoryId, string productName, double priceMin, double priceMax, string brandId, int pageSize, int pageIndex)
        {
            var res = _productRepository.Filter1(type, categoryId, productName, priceMin, priceMax, brandId, pageSize, pageIndex);
            return res;
        }
    }
}
