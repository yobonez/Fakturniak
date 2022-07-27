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
    public class DataStawkiVAT : IDataStawkiVAT
    {
        private readonly ISqlDataAccess _db;

        public DataStawkiVAT(ISqlDataAccess db)
        {
            _db = db;
        }

        public Task<IEnumerable<ModelStawkaVAT>> Get()
        {
            var result = _db.LoadData<ModelStawkaVAT, dynamic>("dbo.spStawkiVAT_GetAll", new { });
            FakturniakStatus.zapytanie = false;
            return result;
        }

        public Task Insert(ModelStawkaVAT sv) =>
            _db.SaveData(
                "dbo.spStawkiVAT_Add",
                new { sv.id_stawki, sv.wartosc });
        public Task Delete(int _id_stawki) =>
            _db.SaveData("dbo.spStawkiVAT_Delete", new { id_stawki = _id_stawki });

        public async Task<ModelStawkaVAT?> Load(int _id_stawki)
        {
            var results = await _db.LoadData<ModelStawkaVAT, dynamic>("dbo.spStawkiVAT_GetById", new { id_stawki = _id_stawki });
            FakturniakStatus.zapytanie = false;
            return results.FirstOrDefault();
        }


        /*
        public Task<IEnumerable<ModelMTMFakturaProdukt>> Search(string _input) =>
            _db.LoadData<ModelMTMFakturaProdukt, dynamic>("dbo.spMtmProdukty_Search", new { input = _input });
        */
    }
}
