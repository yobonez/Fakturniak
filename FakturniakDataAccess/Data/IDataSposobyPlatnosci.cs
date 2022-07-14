using FakturniakDataAccess.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FakturniakDataAccess.Data
{
    public interface IDataSposobyPlatnosci
    {
        Task Delete(int _id_sposob_platnosci);
        Task<IEnumerable<ModelSposobPlatnosci>> Get();
        Task Insert(ModelSposobPlatnosci sp);
        Task<ModelSposobPlatnosci> Load(int _id_sposob_platnosci);
    }
}