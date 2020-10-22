using AutoMapper;
using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using UserManager.BusinessLogic.DataAccess;
using UserManager.BusinessLogic.Model;
using UserManager.Components;
using UserManager.DTOs;
using UserManager.Extensions;

namespace UserManager.ViewModels
{
    public class UsersViewModel : IViewModel
    {
        private readonly IUserRepository _userRepository;
        private readonly IMessageDialog _messageDialog;
        private readonly IMapper _mapper;

        public BindingList<UserListItemDto> Users { get; set; }

        public UsersViewModel(IUserRepository userRepository, IMessageDialog messageDialog, IMapper mapper)
        {
            _userRepository = userRepository;
            _messageDialog = messageDialog;
            _mapper = mapper;
            this.Users = new BindingList<UserListItemDto>();
        }

        public async Task Load()
        {
            Users.Clear();
            var users = await _userRepository.GetAll();
            Users.AddRange(users.Select(_mapper.Map<UserListItemDto>).ToList());
        }

        public async Task<bool> CreateUser(CreateUserDto user)
        {
            try
            {
                if(!GenericValidator.TryValidate(user, out var validationResults))
                {
                    _messageDialog.Show(validationResults);
                    return false;
                }
                var newUser = _mapper.Map<User>(user);
                var createdId = await _userRepository.CreateUser(newUser);
                Users.Add(_mapper.Map<UserListItemDto>(newUser));
                _messageDialog.Show(title: "User created", message: $"Name = {newUser.FirstName}, Id = {createdId}");
                return true;
            }
            catch (Exception ex)
            {
                _messageDialog.Show(title: "Error in creation", message: ex.Message);
                return false;
            }
        }
    }
}
