using FakturniakDataAccess.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FakturniakDataAccess.Data
{
    public interface IDataFaktury
    {
        Task DeleteFaktura(string _numer_faktury);
        Task<IEnumerable<ModelFaktura>> GetFaktury();
        Task InsertFaktura(ModelFaktura f);
        Task<ModelFaktura> LoadFaktura(string _numer_faktury);
    }
}