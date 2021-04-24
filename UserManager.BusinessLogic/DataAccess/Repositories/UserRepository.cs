using Dapper;
using Dapper.Contrib.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserManager.BusinessLogic.Model;

namespace UserManager.BusinessLogic.DataAccess.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DatabaseContext _context;

        public UserRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<int> CreateUser(User user)
        {
            using (var connection = _context.GetOpenedConnection())
            {
                return await connection.InsertAsync(user);
            }
        }

        public async Task<List<User>> GetAll()
        {
            using (var connection = _context.GetOpenedConnection())
            {
                return await connection.GetAllAsync<User>().ToListAsync();
            }
        }

        public async Task<User> GetById(int id)
        {
            using (var connection = _context.GetOpenedConnection())
            {
                return await connection.GetAsync<User>(id);
            }
        }

        public async Task UpdateUser(User user)
        {
            // Nothing, this is updated by reference
        }

        public async Task Remove(int id)
        {
            using (var connection = _context.GetOpenedConnection())
            {
                await connection.ExecuteAsync("DELETE FROM [User] WHERE Id = @Id", new { Id = id });
            }
        }
    }
}
