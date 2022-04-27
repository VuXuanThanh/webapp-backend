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

        public async Task<Object> Filter()
        {
            var sqlCmd = $"Proc_FilterOrders";
            var parameter = new DynamicParameters();
            parameter.Add("@TotalRecords", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameter.Add("@SuccessOrders", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameter.Add("@PendingOrders", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameter.Add("@TotalMoney", dbType: DbType.Double, direction: ParameterDirection.Output);
            var data = await sqlConnection.QueryAsync<Orders>(sqlCmd, commandType: System.Data.CommandType.StoredProcedure, param: parameter);
            var totalRecords = parameter.Get<int>("@TotalRecords");
            var successOrders = parameter.Get<int>("@SuccessOrders");
            var pendingOrders = parameter.Get<int>("@PendingOrders");
            var totalMoney = parameter.Get<double>("@TotalMoney");

            object result = new
            {
                totalRecords = totalRecords,
                successOrders = successOrders,
                pendingOrders = pendingOrders,
                totalMoney = totalMoney,
                data = data
            };
            return result;
        }

        public new async Task<int> Update(Orders order)
        {
            var sql = "update Orders set StatusOrder = @statusOrder where OrdersId = @orderId";
            var dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("statusOrder", 1);
            dynamicParameters.Add("orderId", order.OrdersId);
            var res = await sqlConnection.ExecuteAsync(sql, param: dynamicParameters);
            return res;
        }

       
    }
}
