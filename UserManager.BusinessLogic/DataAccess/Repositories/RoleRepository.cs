using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserManager.BusinessLogic.Model;

namespace UserManager.BusinessLogic.DataAccess.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        public Task<List<Role>> GetAll() => Task.FromResult(Role.Roles.ToList());
    }
}
