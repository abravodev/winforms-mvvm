using Dapper;
using System.Threading.Tasks;
using System;
using System.Data.SqlClient;

namespace UserManager.BusinessLogic.DataAccess
{
    public class DatabaseService : IDatabaseService
    {
        private readonly DatabaseContext _databaseContext;

        public DatabaseService(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public string GetName() => new SqlConnectionStringBuilder(_databaseContext.ConnectionString).InitialCatalog;

        public async Task<bool> CanConnectToDatabase()
        {
            try
            {
                using (var connection = _databaseContext.GetOpenedConnection())
                {
                    var databaseVersion = await connection.QueryAsync<string>("SELECT @@version");
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
