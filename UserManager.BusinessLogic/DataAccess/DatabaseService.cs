using System.Threading.Tasks;
using System;
using UserManager.BusinessLogic.DataAccess.Repositories;

namespace UserManager.BusinessLogic.DataAccess
{
    public class DatabaseService : IDatabaseService
    {
        private readonly DatabaseContext _databaseContext;
        private readonly IDatabaseRepository _databaseRepository;

        public DatabaseService(DatabaseContext databaseContext, IDatabaseRepository databaseRepository)
        {
            _databaseContext = databaseContext;
            _databaseRepository = databaseRepository;
        }

        public string GetName() => _databaseContext.ConnectionInfo.InitialCatalog;

        public string GetServer() => _databaseContext.ConnectionInfo.DataSource;

        public async Task<bool> CanConnectToDatabase()
        {
            try
            {
                var databaseVersion = await _databaseRepository.GetDatabaseVersion();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
