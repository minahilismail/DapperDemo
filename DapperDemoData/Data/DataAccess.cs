using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Protocols;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperDemoData.Data
{
    public class DataAccess : IDataAccess
    {
        private readonly IConfiguration _config;

        public DataAccess(IConfiguration config)
        {
            _config = config;
        }

        public async Task<IEnumerable<T>> GetData<T,P>(string query, P parameters, string connectionId= "DefaultSQLConnection")
        {
            using IDbConnection connection = new SqlConnection(_config.GetConnectionString(connectionId));
            
                return await connection.QueryAsync<T>(query, parameters);
            
        }

        public async Task SaveData<P>(string query, P parameters, string connectionId = "DefaultSQLConnection")
        {
            using IDbConnection connection = new SqlConnection(_config.GetConnectionString(connectionId));
            await connection.ExecuteAsync(query, parameters);
        }
    }
}
