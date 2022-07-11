using FakturniakDataAccess.DbAccess;
using FakturniakDataAccess.Models;
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

        public Task<IEnumerable<ModelFaktura>> GetFaktury() =>
            _db.LoadData<ModelFaktura, dynamic>("dbo.spFaktury_GetAll", new { });

        public async Task<ModelFaktura?> LoadFaktura(string _numer_faktury)
        {
            var results = await _db.LoadData<ModelFaktura, dynamic>("dbo.spFaktury_GetByNumer", new { numer_faktury = _numer_faktury });
            return results.FirstOrDefault();
        }

        public Task InsertFaktura(ModelFaktura f) =>
            _db.SaveData(
                "dbo.spFaktury_Add",
                new { f.numer_faktury, f.data_wystawienia, f.data_sprzedazy, f.miejsce_wystawienia, f.id_sprzedawca, f.id_nabywca, f.id_sposob_platnosci, f.termin_platnosci, f.id_typu_faktury });
        public Task DeleteFaktura(string _numer_faktury) =>
            _db.SaveData("dbo.spFaktury_Delete", new { numer_faktury = _numer_faktury });
    }
}
