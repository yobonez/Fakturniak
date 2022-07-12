using FakturniakDataAccess.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FakturniakDataAccess.Data
{
    public interface IDataFaktury
    {
        Task Delete(string _numer_faktury);
        Task<IEnumerable<ModelFaktura>> Get();
        Task Insert(ModelFaktura f);
        Task<ModelFaktura> Load(string _numer_faktury);
    }
}