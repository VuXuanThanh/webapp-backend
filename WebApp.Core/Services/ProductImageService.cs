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
    public class ProductImageService : BaseService<ProductImage>, IProductImageService
    {
        IProductImageRepository _productImageRepository;

        public ProductImageService(IProductImageRepository productImageRepository):base(productImageRepository)
        {
            _productImageRepository = productImageRepository;
        }

        public List<ProductImage> GetImageByProductId(string productId)
        {
            var res = _productImageRepository.GetImageByProductId(productId);
            return res;
        }
    }
}
