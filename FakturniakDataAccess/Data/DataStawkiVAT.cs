using FakturniakDataAccess.DbAccess;
using FakturniakDataAccess.Models;
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

        /*
        public Task<IEnumerable<ModelStawkaVAT>> Get() =>
            _db.LoadData<ModelStawkaVAT, dynamic>("dbo.spProdukty_GetAll", new { });
        */
        public Task Insert(ModelStawkaVAT sv) =>
            _db.SaveData(
                "dbo.spStawkiVAT_Add",
                new { sv.id_stawki, sv.wartosc });
        public Task Delete(int _id_stawki) =>
            _db.SaveData("dbo.spStawkiVAT_Delete", new { id_stawki = _id_stawki });

        public async Task<ModelStawkaVAT?> Load(int _id_stawki)
        {
            var results = await _db.LoadData<ModelStawkaVAT, dynamic>("dbo.spStawkiVAT_GetById", new { id_stawki = _id_stawki });
            return results.FirstOrDefault();
        }


        /*
        public Task<IEnumerable<ModelMTMFakturaProdukt>> Search(string _input) =>
            _db.LoadData<ModelMTMFakturaProdukt, dynamic>("dbo.spMtmProdukty_Search", new { input = _input });
        */
    }
}
