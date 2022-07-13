using FakturniakDataAccess.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FakturniakDataAccess.Data
{
    public interface IDataMTMFakturaProdukt
    {
        Task Delete(int _id_produktu);
        Task<IEnumerable<ModelMTMFakturaProdukt>> Get();
        Task Insert(ModelMTMFakturaProdukt mtmp);
        Task<ModelMTMFakturaProdukt> Load(int _id_produktu);
    }
}