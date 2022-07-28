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
    public class DataFaktury : IDataFaktury
    {
        private readonly ISqlDataAccess _db;

        public DataFaktury(ISqlDataAccess db)
        {
            _db = db;
        }

        public Task<IEnumerable<ModelFaktura>> Get()
        {
            var result = _db.LoadData<ModelFaktura, dynamic>("dbo.spFaktury_GetAll", new { });
            FakturniakStatus.zapytanie = false;
            return result;
        }

        public async Task<string> GetNumerFaktury(int? _numer, int _id_typu_faktury)
        {
            var result = await _db.LoadData<string, dynamic>("dbo.spGetNumerFaktury", new { numer = _numer, id_typu_faktury = _id_typu_faktury });
            FakturniakStatus.zapytanie = false;
            return result.FirstOrDefault();
        }

        public async Task<ModelFaktura?> Load(string _numer_faktury)
        {
            var results = await _db.LoadData<ModelFaktura, dynamic>("dbo.spFaktury_GetByNumer", new { numer_faktury = _numer_faktury });
            FakturniakStatus.zapytanie = false;
            return results.FirstOrDefault();
        }

        public Task<IEnumerable<ModelFaktura>> Search(string _input)
        {
            var result = _db.LoadData<ModelFaktura, dynamic>("dbo.spFaktury_Search", new { input = _input });
            FakturniakStatus.zapytanie = false;
            return result;
        }

        public Task Insert(ModelFaktura f) =>
            _db.SaveData(
                "dbo.spFaktury_Add",
                new { f.data_wystawienia, f.data_sprzedazy, f.miejsce_wystawienia, f.id_sprzedawca, f.id_nabywca, f.id_sposob_platnosci, f.termin_platnosci, f.id_typu_faktury, f.uwagi, f.uwagi_wewnetrzne });
        public Task Delete(string _numer_faktury) =>
            _db.SaveData("dbo.spFaktury_Delete", new { numer_faktury = _numer_faktury });
    }
}
