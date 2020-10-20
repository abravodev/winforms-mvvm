using System.Linq;
using System.Threading.Tasks;
using UserManager.BusinessLogic.Model;

namespace UserManager.BusinessLogic.DataAccess
{
    public class UserSeed
    {
        private readonly IUserRepository _userRepository;

        public UserSeed(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task Execute()
        {
            var users = new[]
            {
                new User
                {
                    FirstName = "John", LastName = "Doe"
                },
                new User
                {
                    FirstName = "Jack", LastName = "Smith"
                }
            };
            
            var userCreations = users.Select(_userRepository.CreateUser);
            await Task.WhenAll(userCreations);
        }
    }
}
