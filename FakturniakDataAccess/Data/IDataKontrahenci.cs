using FakturniakDataAccess.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FakturniakDataAccess.Data
{
    public interface IDataKontrahenci
    {
        Task Delete(int id);
        Task<IEnumerable<ModelKontrahent>> Get();
        Task Insert(ModelKontrahent k);
        Task<ModelKontrahent> Load(int id);
    }
}