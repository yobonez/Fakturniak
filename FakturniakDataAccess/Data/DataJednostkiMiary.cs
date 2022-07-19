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
using System.Linq;
using System.Threading.Tasks;

namespace FakturniakDataAccess.Data
{
    public class DataJednostkiMiary : IDataJednostkiMiary
    {
        private readonly ISqlDataAccess _db;

        public DataJednostkiMiary(ISqlDataAccess db)
        {
            _db = db;
        }

        /*
        public Task<IEnumerable<ModelStawkaVAT>> Get() =>
            _db.LoadData<ModelStawkaVAT, dynamic>("dbo.spProdukty_GetAll", new { });
        */
        public Task Insert(ModelJednostkaMiary jm) =>
            _db.SaveData(
                "dbo.spJednostkiMiary_Add",
                new { jm.id_jednostki, jm.nazwa });
        public Task Delete(int _id_jednostki) =>
            _db.SaveData("dbo.spJednostkiMiary_Delete", new { id_jednostki = _id_jednostki });

        public async Task<ModelJednostkaMiary?> Load(int _id_jednostki)
        {
            var results = await _db.LoadData<ModelJednostkaMiary, dynamic>("dbo.spJednostkiMiary_GetById", new { id_jednostki = _id_jednostki });
            return results.FirstOrDefault();
        }


        /*
        public Task<IEnumerable<ModelMTMFakturaProdukt>> Search(string _input) =>
            _db.LoadData<ModelMTMFakturaProdukt, dynamic>("dbo.spMtmProdukty_Search", new { input = _input });
        */

    }
}
