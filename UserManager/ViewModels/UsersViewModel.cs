using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserManager.BusinessLogic.DataAccess;
using UserManager.BusinessLogic.Model;
using UserManager.Components;
using UserManager.DTOs;

namespace UserManager.ViewModels
{
    public class UsersViewModel : IViewModel
    {
        private readonly IUserRepository _userRepository;
        private readonly IMessageDialog _messageDialog;

        public List<UserListItemDto> Users { get; set; }

        public UsersViewModel(IUserRepository userRepository, IMessageDialog messageDialog)
        {
            _userRepository = userRepository;
            _messageDialog = messageDialog;
            this.Users = new List<UserListItemDto>();
        }

        public async Task Load()
        {
            Users.Clear();
            var users = await _userRepository.GetAll();
            Users.AddRange(users.Select(UserListItemDto.Map).ToList());
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
                Users.Add(UserListItemDto.Map(newUser));
                _messageDialog.Show(title: "User created", message: $"Name = {newUser.FirstName}, Id = {createdId}");
            }
            catch (Exception ex)
            {
                _messageDialog.Show(title: "Error in validation", message: ex.Message);
            }
        }
    }
}
