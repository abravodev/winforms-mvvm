using System.Collections.Generic;
using System.Threading.Tasks;
using UserManager.BusinessLogic.Model;

namespace UserManager.BusinessLogic.DataAccess
{
    public interface IUserRepository
    {
        Task<User> GetById(int id);

        Task<List<User>> GetAll();

        Task<int> CreateUser(User user);

        Task UpdateUser(User user);

        Task Remove(int id);
    }
}