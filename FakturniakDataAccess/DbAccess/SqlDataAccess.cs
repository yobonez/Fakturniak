using Dapper;
using System.Data;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace FakturniakDataAccess.DbAccess
{
    public class SqlDataAccess : ISqlDataAccess
    {
        Helper helper = new Helper();
        private string cnnStr;

        public SqlDataAccess()
        {
            cnnStr = helper.getConnectionString("FakturniakDB");
        }

        public async Task<IEnumerable<T>> LoadData<T, U>(string storedProcedure, U parameters)
        {
            using IDbConnection connection = new SqlConnection(cnnStr);

            return await connection.QueryAsync<T>(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
        }

        public async Task SaveData<T>(string storedProcedure, T parameters)
        {
            using IDbConnection connection = new SqlConnection(cnnStr);

            await connection.ExecuteAsync(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
        }
    }
}
