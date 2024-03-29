﻿//  Copyright (C) 2022 Jacek Gałuszka
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

using FakturniakDataAccess.DbAccess;
using FakturniakDataAccess.Status;
using System.Linq;
using System.Threading.Tasks;

namespace FakturniakDataAccess.Data
{
    public class DataUzytkownik : IDataUzytkownik
    {
        private readonly ISqlDataAccess _db;

        public DataUzytkownik(ISqlDataAccess db)
        {
            _db = db;
        }

        public async Task<string> GetUzytkownik()
        {
            var result = await _db.LoadData<string, dynamic>("dbo.spGetUser", new { });
            FakturniakStatus.zapytanie = false;
            return result.FirstOrDefault();
        }
    }
}
