using FakturniakDataAccess.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FakturniakDataAccess.Data
{
    public interface IDataProdukty
    {
        Task Delete(int _id_produktu);
        Task<IEnumerable<ModelProdukt>> Get();
        Task Insert(ModelProdukt p);
        Task InsertBrutto(ModelProdukt p);
        Task InsertNetto(ModelProdukt p);
        Task<ModelProdukt> Load(int _id_produktu);
    }
}