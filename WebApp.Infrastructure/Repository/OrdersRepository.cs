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
    public class OrdersRepository : BaseRepository<Orders>, IOrdersRepository
    {
        public OrdersRepository(IConfiguration configuration) : base(configuration)
        {
        }
        public new async Task<OrderResponse> Insert(Orders entity)
        {
            var sqlCommand = $"Proc_Add{tableName}";
            var parameter = new DynamicParameters();
            var collections = entity.GetType().GetProperties();
            var keyId = Guid.NewGuid().ToString();
            parameter.Add($"{tableName}Id", keyId);
            for (int i = 1; i < collections.Length; i++)
            {

                var name = collections[i].Name;
                var value = collections[i].GetValue(entity);
                parameter.Add($"{name}", value);

            }

            var result = await sqlConnection.ExecuteAsync(sqlCommand, commandType: System.Data.CommandType.StoredProcedure, param: parameter);
            var res = new OrderResponse(result, keyId);
            return res;
        }

       
    }
}
