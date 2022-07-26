using System.Threading.Tasks;

namespace FakturniakDataAccess.Data
{
    public interface IDataUzytkownik
    {
        Task<string> GetUzytkownik();
    }
}