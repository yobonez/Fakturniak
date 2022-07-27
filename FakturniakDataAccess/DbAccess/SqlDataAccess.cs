//  Copyright (C) 2022 Jacek Gałuszka
/*
    This file is part of Fakturniak.

    Fakturniak is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation; either version 3 of the License, or
    (at your option) any later version.

    Fakturniak is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with Fakturniak.  If not, see <http://www.gnu.org/licenses/>.
*/

using Dapper;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Collections.Generic;
using FakturniakDataAccess.Status;


namespace FakturniakDataAccess.DbAccess
{
    public class SqlDataAccess : ISqlDataAccess
    {
        Helper helper = new Helper();
        private string cnnStr;

        public SqlDataAccess(string username, string pass)
        {
            helper.setConnectionString("FakturniakDB", $@"Server=localhost\FAKTURNIAKDB;Database=FakturniakDB;User Id={username};Password={pass};");

            cnnStr = helper.getConnectionString("FakturniakDB");
        }

        public async Task<IEnumerable<T>> LoadData<T, U>(string storedProcedure, U parameters)
        {
            using IDbConnection connection = new SqlConnection(cnnStr);
            FakturniakStatus.zapytanie = true;
            return await connection.QueryAsync<T>(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
        }

        public async Task SaveData<T>(string storedProcedure, T parameters)
        {
            using IDbConnection connection = new SqlConnection(cnnStr);
            FakturniakStatus.zapytanie = true;
            await connection.ExecuteAsync(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
            FakturniakStatus.zapytanie = false;
        }
    }
}
