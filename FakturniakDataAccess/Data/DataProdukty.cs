using FakturniakDataAccess.DbAccess;
using FakturniakDataAccess.Models;
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

        public Task<IEnumerable<ModelProdukt>> Get() =>
            _db.LoadData<ModelProdukt, dynamic>("dbo.spProdukty_GetAll", new { });

        public async Task<ModelProdukt?> Load(int _id_produktu)
        {
            var results = await _db.LoadData<ModelProdukt, dynamic>("dbo.spProdukty_GetByNumer", new { id_produktu = _id_produktu });
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

        public Task<IEnumerable<ModelProdukt>> Search(string _input) =>
            _db.LoadData<ModelProdukt, dynamic>("dbo.spProdukty_Search", new { input = _input });

        public Task Delete(int _id_produktu) =>
            _db.SaveData("dbo.spProdukty_Delete", new { id_produktu = _id_produktu });
    }
}