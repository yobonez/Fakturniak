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
using System.Threading.Tasks;

namespace FakturniakDataAccess.Data
{
    public class DataKontrahenci : IDataKontrahenci
    {
        private readonly ISqlDataAccess _db;

        public DataKontrahenci(ISqlDataAccess db)
        {
            _db = db;
        }

        public Task<IEnumerable<ModelKontrahent>> Get()
        {
            var result = _db.LoadData<ModelKontrahent, dynamic>("dbo.spKontrahenci_GetAll", new { });
            FakturniakStatus.zapytanie = false;
            return result;
        }

        public async Task<ModelKontrahent?> Load(int id)
        {
            var results = await _db.LoadData<ModelKontrahent, dynamic>("dbo.spKontrahenci_GetById", new { id_kontrahenta = id });
            FakturniakStatus.zapytanie = false;
            return results.FirstOrDefault();
        }

        public Task Insert(ModelKontrahent k) =>
            _db.SaveData(
                "dbo.spKontrahenci_Add",
                new { k.imie, k.nazwisko, k.nazwa, k.nip, k.regon, k.krs, k.pesel, k.email, k.telefon, k.adres, k.kod_pocztowy, k.miasto, k.numer_konta, k.swift });
        public Task Delete(int id) =>
            _db.SaveData("dbo.spKontrahenci_Delete", new { id_kontrahenta = id });
    }
}
