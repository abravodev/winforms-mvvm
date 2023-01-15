using System.Threading.Tasks;

namespace UserManager.BusinessLogic.DataAccess
{
    public interface IDatabaseService
    {
        string GetName();

        Task<bool> CanConnectToDatabase();
        string GetServer();
    }
}