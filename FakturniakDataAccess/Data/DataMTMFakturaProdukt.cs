using FakturniakDataAccess.DbAccess;
using FakturniakDataAccess.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace FakturniakDataAccess.Data
{
    public class DataMTMFakturaProdukt : IDataMTMFakturaProdukt
    {
        private readonly ISqlDataAccess _db;

        public DataMTMFakturaProdukt(ISqlDataAccess db)
        {
            _db = db;
        }

        public Task<IEnumerable<ModelMTMFakturaProdukt>> Get() =>
            _db.LoadData<ModelMTMFakturaProdukt, dynamic>("dbo.spProdukty_GetAll", new { });

        public async Task<ModelMTMFakturaProdukt?> Load(int _id_produktu)
        {
            var results = await _db.LoadData<ModelMTMFakturaProdukt, dynamic>("dbo.spProdukty_GetByNumer", new { id_produktu = _id_produktu });
            return results.FirstOrDefault();
        }

        public Task Insert(ModelMTMFakturaProdukt mtmp) =>
            _db.SaveData(
                "dbo.spMtmFakturaProdukty_Add",
                new { mtmp.numer_faktury, mtmp.id_produktu, mtmp.ilosc });
        /*
        public Task<IEnumerable<ModelMTMFakturaProdukt>> Search(string _input) =>
            _db.LoadData<ModelMTMFakturaProdukt, dynamic>("dbo.spMtmProdukty_Search", new { input = _input });
        */
        public Task Delete(int _id_produktu) =>
            _db.SaveData("dbo.spMtmFakturaProdukty_Delete", new { id_produktu = _id_produktu });
    }
}
