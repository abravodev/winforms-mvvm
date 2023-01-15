using Dapper;
using System.Threading.Tasks;

namespace UserManager.BusinessLogic.DataAccess.Repositories
{
    public class DatabaseRepository : IDatabaseRepository
    {
        private readonly DatabaseContext _context;

        public DatabaseRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<string> GetDatabaseVersion()
        {
            using (var connection = _context.GetOpenedConnection())
            {
                return await connection.QueryFirstAsync<string>("SELECT @@version");
            }
        }
    }
}
