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
