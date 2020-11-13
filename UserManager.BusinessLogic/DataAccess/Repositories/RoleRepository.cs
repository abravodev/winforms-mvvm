using System.Collections.Generic;
using System.Threading.Tasks;
using UserManager.BusinessLogic.Model;

namespace UserManager.BusinessLogic.DataAccess.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        public Task<List<Role>> GetAll()
        {
            var roles = new List<Role>
            {
                new Role 
                {
                    Id = 1,
                    Name = "Admin",
                    Description = "Role which can perform elevated actions"
                },
                new Role
                {
                    Id = 2,
                    Name = "Basic",
                    Description = "Role which can only read info"
                }
            };

            return Task.FromResult(roles);
        }
    }
}
