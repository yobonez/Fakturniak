using FakturniakDataAccess.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FakturniakDataAccess.Data
{
    public interface IDataKontrahenci
    {
        Task DeleteKontrahent(int id);
        Task<IEnumerable<ModelKontrahent>> GetKontrahenci();
        Task InsertKontrahent(ModelKontrahent k);
        Task<ModelKontrahent> LoadKontrahent(int id);
    }
}