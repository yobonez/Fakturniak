using System.Configuration;

namespace FakturniakDataAccess.DbAccess
{
    public class Helper
    {
        public string getConnectionString(string name)
        {
            return ConfigurationManager.ConnectionStrings[name].ConnectionString.ToString();
        }
    }
}
