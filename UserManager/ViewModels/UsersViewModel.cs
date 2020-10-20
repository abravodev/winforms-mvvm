using System.Collections.Generic;
using System.Threading.Tasks;
using UserManager.BusinessLogic.DataAccess;
using UserManager.BusinessLogic.Model;

namespace UserManager.ViewModels
{
    public class UsersViewModel : IViewModel
    {
        private readonly IUserRepository _userRepository;

        public List<User> Users { get; set; }

        public UsersViewModel(IUserRepository userRepository)
        {
            _userRepository = userRepository;
            this.Users = new List<User>();
        }

        public async Task Load()
        {
            Users.Clear();
            var users = await _userRepository.GetAll();
            Users.AddRange(users);
        }
    }
}
