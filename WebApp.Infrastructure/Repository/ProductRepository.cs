using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.Core.Entities;
using WebApp.Core.Interface.Repository;

namespace WebApp.Infrastructure.Repository
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(IConfiguration configuration) : base(configuration)
        {
        }

        
        public async Task<object> Filter(string categoryId, string productName, double priceMin, double priceMax, string brandId, int pageSize=24, int pageIndex=1)
        {
            var sqlCmd = $"Proc_Filter{tableName}";
            var parameter = new DynamicParameters();
            parameter.Add("@PageIndex", pageIndex);
            parameter.Add("@PageSize", pageSize);
            parameter.Add($"@CategoryId", categoryId);
            parameter.Add($"@ProductName", productName);
            parameter.Add($"@PriceMin", priceMin);
            parameter.Add($"@PriceMax", priceMax);
            parameter.Add($"@BrandId", brandId);
            parameter.Add("@TotalRecords", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameter.Add("@TotalPages", dbType: DbType.Int32, direction: ParameterDirection.Output);
            var data = await sqlConnection.QueryAsync<Product>(sqlCmd, commandType: System.Data.CommandType.StoredProcedure, param: parameter);
            var totalRecords = parameter.Get<int>("@TotalRecords");
            var totalPages = parameter.Get<int>("@TotalPages");

            object result = new
            {
                totalRecord = totalRecords,
                totalPage = totalPages,
                data = data
            };
            return result;
        }

        public async Task<object> Filter1(int type, string categoryId, string productName, double priceMin, double priceMax, string brandId, int pageSize, int pageIndex)
        {
            var sqlCmd = $"Proc_FilterProduct2";
            var parameter = new DynamicParameters();
            parameter.Add("@Type", type);
            parameter.Add("@PageIndex", pageIndex);
            parameter.Add("@PageSize", pageSize);
            parameter.Add($"@CategoryId", categoryId);
            parameter.Add($"@ProductName", productName);
            parameter.Add($"@PriceMin", priceMin);
            parameter.Add($"@PriceMax", priceMax);
            parameter.Add($"@BrandId", brandId);
            parameter.Add("@TotalRecords", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameter.Add("@TotalPages", dbType: DbType.Int32, direction: ParameterDirection.Output);
            var data = await sqlConnection.QueryAsync<Product>(sqlCmd, commandType: System.Data.CommandType.StoredProcedure, param: parameter);
            var totalRecords = parameter.Get<int>("@TotalRecords");
            var totalPages = parameter.Get<int>("@TotalPages");

            object result = new
            {
                totalRecord = totalRecords,
                totalPage = totalPages,
                data = data
            };
            return result;
        }
    }
}
