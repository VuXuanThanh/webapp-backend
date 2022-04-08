using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
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

        public BaseRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DbConnection");
            _configuration = configuration;
            sqlConnection = new SqlConnection(_connectionString);
        }
        public int Insert()
        {
            var x = 200000;
            return x;
        }
    }
}
