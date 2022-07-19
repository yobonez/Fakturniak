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

namespace FakturniakDataAccess.DbAccess
{
    public class Helper
    {
        public static string connectionString;

        public void setConnectionString (string name, string cnnstr)
        {
            connectionString = cnnstr;

            // windowsowy login
            //ConfigurationManager.ConnectionStrings[name].ConnectionString = cnnstr;
        }
        public string getConnectionString(string name)
        {
            return connectionString;

            // windowsowy login
            //return ConfigurationManager.ConnectionStrings[name].ConnectionString.ToString();
        }
    }
}
