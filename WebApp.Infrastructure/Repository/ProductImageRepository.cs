using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.Core.Entities;
using WebApp.Core.Interface.Repository;

namespace WebApp.Infrastructure.Repository
{
    public class ProductImageRepository : BaseRepository<ProductImage>, IProductImageRepository
    {
        public ProductImageRepository(IConfiguration configuration):base(configuration)
        {

        }

        public List<ProductImage> GetImageByProductId(string productId)
        {
            var sql = "select * from ProductImage where ProductId = @ProductId";
            var parameter = new DynamicParameters();
            parameter.Add("productId", productId);
            var res = sqlConnection.Query<ProductImage>(sql, param: parameter).ToList();
            return res;
        }
    }
}
