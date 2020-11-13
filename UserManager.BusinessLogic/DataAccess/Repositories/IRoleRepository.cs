using System.Collections.Generic;
using System.Threading.Tasks;
using UserManager.BusinessLogic.Model;

namespace UserManager.BusinessLogic.DataAccess.Repositories
{
    public interface IRoleRepository
    {
        Task<List<Role>> GetAll();
    }
}