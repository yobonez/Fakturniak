using FakturniakDataAccess.DbAccess;
using FakturniakDataAccess.Models;
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

        public Task<IEnumerable<ModelSposobPlatnosci>> Get() =>
            _db.LoadData<ModelSposobPlatnosci, dynamic>("dbo.spSposobyPlatnosci_GetAll", new { });

        public Task Insert(ModelSposobPlatnosci sp) =>
            _db.SaveData(
                "dbo.spSposobyPlatnosci_Add",
                new { sp.id_sposob_platnosci, sp.nazwa });
        public Task Delete(int _id_sposob_platnosci) =>
            _db.SaveData("dbo.spSposobyPlatnosci_Delete", new { id_sposob_platnosci = _id_sposob_platnosci });

        public async Task<ModelSposobPlatnosci?> Load(int _id_sposob_platnosci)
        {
            var results = await _db.LoadData<ModelSposobPlatnosci, dynamic>("dbo.spSposobyPlatnosci_GetById", new { id_sposob_platnosci = _id_sposob_platnosci });
            return results.FirstOrDefault();
        }
    }
}
