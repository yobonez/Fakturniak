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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakturniakDataAccess.Data
{
    public class DataSposobyPlatnosci : IDataSposobyPlatnosci
    {
        private readonly ISqlDataAccess _db;

        public DataSposobyPlatnosci(ISqlDataAccess db)
        {
            _db = db;
        }

        public Task<IEnumerable<ModelSposobPlatnosci>> Get()
        {
            var result = _db.LoadData<ModelSposobPlatnosci, dynamic>("dbo.spSposobyPlatnosci_GetAll", new { });
            FakturniakStatus.zapytanie = false;
            return result;
        }

        public Task Insert(ModelSposobPlatnosci sp) =>
            _db.SaveData(
                "dbo.spSposobyPlatnosci_Add",
                new { sp.nazwa });
        public Task Delete(int _id_sposob_platnosci) =>
            _db.SaveData("dbo.spSposobyPlatnosci_Delete", new { id_sposob_platnosci = _id_sposob_platnosci });

        public async Task<ModelSposobPlatnosci?> Load(int _id_sposob_platnosci)
        {
            var results = await _db.LoadData<ModelSposobPlatnosci, dynamic>("dbo.spSposobyPlatnosci_GetById", new { id_sposob_platnosci = _id_sposob_platnosci });
            FakturniakStatus.zapytanie = false;
            return results.FirstOrDefault();
        }
    }
}
