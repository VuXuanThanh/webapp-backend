using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WebApp.Core.Entities;
using WebApp.Core.Interface.Repository;

namespace WebApp.Infrastructure.Repository
{
    public class CartItemRepository : BaseRepository<CartItem>, ICartItemRepository
    {
        public CartItemRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<bool> CheckDuplicateKey(string userId, string productId)
        {
            var sql = "select count(*) from CartItem where UsersId =@UserId and ProductId = @ProductId";
            var parameter = new DynamicParameters();
            parameter.Add("UserId", userId);
            parameter.Add("ProductId", productId);
            var res = await sqlConnection.QueryFirstAsync<int>(sql, param: parameter);
            return (res >0) ? true : false;
        }
        public new async Task<int> Insert(CartItem cart)
        {
            var checkDuplicateKey = await CheckDuplicateKey(cart.UsersId, cart.ProductId);
            if (!checkDuplicateKey)
            {
                var sqlCommand = $"Proc_InsertCartItem";
                var parameter = new DynamicParameters();
                var collections = cart.GetType().GetProperties();
                for (int i = 0; i < collections.Length; i++)
                {
                    var classAttrData = ((ColumnAttribute)(collections[i].GetCustomAttribute(typeof(ColumnAttribute), false)));
                    if(classAttrData!=null)
                    {
                        continue;
                    }
                    var name = collections[i].Name;
                    var value = collections[i].GetValue(cart);
                    parameter.Add($"{name}", value);

                }

                var result = await sqlConnection.ExecuteAsync(sqlCommand, commandType: System.Data.CommandType.StoredProcedure, param: parameter);
                return result;
            }
            else
            {
                return -1;
            }

        }


        public async Task<int> Delete(string userId, string productId)
        {
            var sql = $"delete from CartItem where UsersId = @userId and ProductId = @productId";
            var parameter = new DynamicParameters();
            parameter.Add($"userId", userId);
            parameter.Add($"productId", productId);
            var res = await sqlConnection.ExecuteAsync(sql, param: parameter);
            return res;
        }

        public async Task<int> GetAllRecord(string userId)
        {
            var sql = "select count(*) from CartItem where UsersId = @UserId";
            var parameter = new DynamicParameters();
            parameter.Add("UserId", userId);
            var res = await sqlConnection.QueryFirstAsync<int>(sql, param: parameter);
            return res;

        }

        public new async Task<CartItem> GetById(string userId)
        {
            var sql = $"select * from {tableName} where UsersId = @UserId";
            var parameter = new DynamicParameters();
            parameter.Add("UserId", userId);
            var entity = await sqlConnection.QueryFirstOrDefaultAsync<CartItem>(sql, param: parameter);
            return entity;
        }

        public async Task<List<CartItem>> GetCartItemsByUserId(string userId, bool order)
        {
            
            var sql = (order==false)? "select Price, Quanlity, c.ProductId, p.ProductName from CartItem c" +
                " inner join Product p on c.ProductId = p.ProductId " +
                "where c.UsersId = @UserId" : "select Price, Quanlity, c.ProductId, p.ProductName from CartItem c" +
                " inner join Product p on c.ProductId = p.ProductId " +
                "where c.UsersId = @UserId and StatusItem = 1";
            var parameter = new DynamicParameters();
            parameter.Add("UserId", userId);
            var entity = await sqlConnection.QueryAsync<CartItem, Product, CartItem>(sql,(cartItem, product)=> {
                cartItem.Product = product;
                return cartItem;
            },
            splitOn: "ProductId"
            ,  param: parameter);
            return (List<CartItem>)entity;
        }

        public async Task<int> Update(List<ProductItem> list)
        {
            var count = 0;
            foreach (var item in list)
            {
                var sql = "update CartItem set Quanlity = @quanlity, StatusItem = @status " +
                    "where UsersId = @userId and ProductId = @productId";
                var parameter = new DynamicParameters();
                parameter.Add("userId", item.UserId);
                parameter.Add("status", item.StatusItem);
                parameter.Add("quanlity", item.Quanlity);
                parameter.Add("productId", item.ProductId);
                var res = await sqlConnection.ExecuteAsync(sql, param: parameter);
                count += res;
            }
            return count;
        }

       
    }
}
