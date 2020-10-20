using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserManager.BusinessLogic.DataAccess;
using UserManager.BusinessLogic.Model;
using UserManager.Components;

namespace UserManager.ViewModels
{
    public class UsersViewModel : IViewModel
    {
        private readonly IUserRepository _userRepository;
        private readonly IMessageDialog _messageDialog;

        public List<User> Users { get; set; }

        public UsersViewModel(IUserRepository userRepository, IMessageDialog messageDialog)
        {
            _userRepository = userRepository;
            _messageDialog = messageDialog;
            this.Users = new List<User>();
        }

        public async Task Load()
        {
            Users.Clear();
            var users = await _userRepository.GetAll();
            Users.AddRange(users);
        }

        public async Task CreateUser(CreateUserDto user)
        {
            try
            {
                var newUser = new User
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = new Email(user.Email)
                };
                var createdId = await _userRepository.CreateUser(newUser);
                Users.Add(newUser);
                _messageDialog.Show(title: "User created", message: $"Name = {newUser.FirstName}, Id = {createdId}");
            }
            catch (Exception ex)
            {
                _messageDialog.Show(title: "Error in validation", message: ex.Message);
            }
        }
    }

    public class CreateUserDto
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }
        
        public string Email { get; set; }
    }
}
