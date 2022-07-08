using FakturniakDataAccess.DbAccess;
using FakturniakDataAccess.Models;
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

        public Task<IEnumerable<ModelKontrahent>> GetKontrahenci() =>
            _db.LoadData<ModelKontrahent, dynamic>("dbo.spKontrahenci_GetAll", new { });

        public async Task<ModelKontrahent?> LoadKontrahent(int id)
        {
            var results = await _db.LoadData<ModelKontrahent, dynamic>("dbo.spKontrahenci_GetById", new { id_ka = id });
            return results.FirstOrDefault();
        }

        public Task InsertKontrahent(ModelKontrahent k) =>
            _db.SaveData(
                "dbo.spKontrahenci_Add",
                new { k.imie, k.nazwisko, k.nazwa, k.nip, k.regon, k.krs, k.pesel, k.email, k.telefon, k.adres, k.kod_pocztowy, k.miasto, k.numer_konta, k.swift });
        public Task DeleteKontrahent(int id) =>
            _db.SaveData("dbo.spKontrahenci_Delete", new { id_kontrahenta = id });
    }
}
