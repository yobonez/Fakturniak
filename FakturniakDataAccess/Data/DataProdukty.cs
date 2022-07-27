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
    public class DataProdukty : IDataProdukty
    {
        private readonly ISqlDataAccess _db;

        public DataProdukty(ISqlDataAccess db)
        {
            _db = db;
        }

        public Task<IEnumerable<ModelProdukt>> Get()
        {
            var result = _db.LoadData<ModelProdukt, dynamic>("dbo.spProdukty_GetAll", new { });
            FakturniakStatus.zapytanie = false;
            return result;
        }

        public async Task<ModelProdukt?> Load(int _id_produktu)
        {
            var results = await _db.LoadData<ModelProdukt, dynamic>("dbo.spProdukty_GetByNumer", new { id_produktu = _id_produktu });
            FakturniakStatus.zapytanie = false;
            return results.FirstOrDefault();
        }

        public Task Insert(ModelProdukt p) =>
            _db.SaveData(
                "dbo.spProdukty_Add",
                new { p.nazwa, p.cena_netto, p.cena_brutto, p.id_jednostki, p.id_stawki });
        public Task InsertNetto(ModelProdukt p) =>
            _db.SaveData(
                "dbo.spProdukty_AddByNetto",
                new { p.nazwa, p.cena_netto, p.id_jednostki, p.id_stawki });
        public Task InsertBrutto(ModelProdukt p) =>
            _db.SaveData(
                "dbo.spProdukty_AddByBrutto",
                new { p.nazwa, p.cena_brutto, p.id_jednostki, p.id_stawki });

        public Task<IEnumerable<ModelProdukt>> Search(string _input)
        {
            var result = _db.LoadData<ModelProdukt, dynamic>("dbo.spProdukty_Search", new { input = _input });
            FakturniakStatus.zapytanie = false;
            return result;
        }

        public Task Delete(int _id_produktu) =>
            _db.SaveData("dbo.spProdukty_Delete", new { id_produktu = _id_produktu });
    }
}