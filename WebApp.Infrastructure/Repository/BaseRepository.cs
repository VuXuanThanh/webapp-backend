﻿using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.Core.Interface.Repository;

namespace WebApp.Infrastructure.Repository
{
    public class BaseRepository<T> : IBaseRepository<T>
    {
        public static string _connectionString;
        protected SqlConnection sqlConnection;

        protected IConfiguration _configuration;
        protected string tableName;

        public BaseRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DbConnection");
            _configuration = configuration;
            sqlConnection = new SqlConnection(_connectionString);
            tableName = typeof(T).Name;
        }

        public async Task<int> Delete(Guid entityId)
        {
            var sql = $"delete from {tableName} where {tableName}Id = @entityId";
            var parameter = new DynamicParameters();
            parameter.Add($"entityId", entityId);
            var res = await sqlConnection.ExecuteAsync(sql,param: parameter);
            return res;
        }

        public Task<object> Filter(string searchString, int pageSize, int pageIndex)
        {
            throw new NotImplementedException();
        }

        public async Task<List<T>> GetAll()
        {
            var sql = $"select * from {tableName}";
            var res = await sqlConnection.QueryAsync<T>(sql);
            return (List<T>)res;
        }

        public async Task<T> GetById(string entityId)
        {
            var sql = $"select * from {tableName} where {tableName}Id = @{tableName}Id";
            var parameter = new DynamicParameters();
            parameter.Add($"{tableName}Id", entityId);
            var entity = await sqlConnection.QueryFirstOrDefaultAsync<T>(sql, param: parameter);
            return entity;
        }


        public int Insert()
        {
            var x = 200000;
            return x;
        }

        public async Task<int> Insert(T entity)
        {
            var sqlCommand = $"Proc_Add{tableName}";
            var parameter = new DynamicParameters();
            var collections = entity.GetType().GetProperties();
            parameter.Add($"{tableName}Id", Guid.NewGuid());
            for (int i = 1; i < collections.Length; i++)
            {

                var name = collections[i].Name;
                var value = collections[i].GetValue(entity);
                parameter.Add($"{name}", value);

            }

            var result = await sqlConnection.ExecuteAsync(sqlCommand, commandType: System.Data.CommandType.StoredProcedure, param: parameter);
            return result;
        }

        public Task<int> Update(Guid entityId, T entity)
        {
            throw new NotImplementedException();
        }

     
    }
}
