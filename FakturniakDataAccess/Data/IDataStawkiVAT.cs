using FakturniakDataAccess.Models;
using System.Threading.Tasks;

namespace FakturniakDataAccess.Data
{
    public interface IDataStawkiVAT
    {
        Task Delete(int _id_stawki);
        Task Insert(ModelStawkaVAT sv);
        Task<ModelStawkaVAT> Load(int _id_stawki);
    }
}