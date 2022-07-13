using FakturniakDataAccess.Models;
using System.Threading.Tasks;

namespace FakturniakDataAccess.Data
{
    public interface IDataJednostkiMiary
    {
        Task Delete(int _id_jednostki);
        Task Insert(ModelJednostkaMiary jm);
        Task<ModelJednostkaMiary> Load(int _id_jednostki);
    }
}