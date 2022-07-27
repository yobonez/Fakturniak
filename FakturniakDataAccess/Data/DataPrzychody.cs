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

using FakturniakDataAccess.DbAccess;
using FakturniakDataAccess.Models;
using FakturniakDataAccess.Status;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FakturniakDataAccess.Data
{
    public class DataPrzychody : IDataPrzychody
    {
        private readonly ISqlDataAccess _db;

        public DataPrzychody(ISqlDataAccess db)
        {
            _db = db;
        }

        public Task<IEnumerable<ModelPrzychody>> Get()
        {
            var result = _db.LoadData<ModelPrzychody, dynamic>("dbo.spPrzychodyOgolem", new { });
            FakturniakStatus.zapytanie = false;
            return result;
        }
    }
}
