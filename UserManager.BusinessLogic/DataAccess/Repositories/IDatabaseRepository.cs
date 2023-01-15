using System.Threading.Tasks;

namespace UserManager.BusinessLogic.DataAccess.Repositories
{
    public interface IDatabaseRepository
    {
        Task<string> GetDatabaseVersion();
    }
}