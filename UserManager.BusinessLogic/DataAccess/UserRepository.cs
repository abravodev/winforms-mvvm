using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserManager.BusinessLogic.Model;

namespace UserManager.BusinessLogic.DataAccess
{
    public class UserRepository : IUserRepository
    {
        private readonly List<User> _users;
        private int _nextGeneratedId;

        public UserRepository()
        {
            _users = new List<User>();
            _nextGeneratedId = 1;
        }

        public Task<int> CreateUser(User user)
        {
            user.Id = _nextGeneratedId++;
            _users.Add(user);
            return Task.FromResult(user.Id);
        }

        public Task<IList<User>> GetAll()
        {
            return Task.FromResult<IList<User>>(_users);
        }

        public Task<User> GetById(int id)
        {
            return Task.FromResult(_users.First(x => x.Id == id));
        }

        public Task UpdateUser(User user)
        {
            // Nothing, this is updated by reference
            return Task.CompletedTask;
        }

        public async Task Remove(int id)
        {
            var user = await GetById(id);
            _users.Remove(user);
        }
    }
}
