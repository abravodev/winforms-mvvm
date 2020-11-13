using Dapper;
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
                return await connection.QueryFirstAsync<int>(@"
                    INSERT INTO [USER] (FirstName, LastName, Email) 
                    OUTPUT INSERTED.Id
                    VALUES(@FirstName, @LastName, @Email)",
                    new { user.FirstName, user.LastName, user.Email });
            }
        }

        public async Task<List<User>> GetAll()
        {
            using (var connection = _context.GetOpenedConnection())
            {
                return await connection.QueryAsync<User>("SELECT * FROM [User]").ToListAsync();
            }
        }

        public async Task<User> GetById(int id)
        {
            using (var connection = _context.GetOpenedConnection())
            {
                return await connection
                    .GetFirstAsync<User>("SELECT * FROM [User] WHERE Id = @Id", new { Id = id });
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
