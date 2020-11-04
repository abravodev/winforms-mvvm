using AutoMapper;
using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using UserManager.BusinessLogic.DataAccess;
using UserManager.BusinessLogic.Model;
using MvvmTools.Components;
using UserManager.DTOs;
using MvvmTools.Bindings;
using MvvmTools.Core;
using Serilog;
using UserManager.BusinessLogic.Extensions;
using UserManager.Resources;

namespace UserManager.ViewModels
{
    public class UsersViewModel : BindableObject, IViewModel
    {
        private readonly IUserRepository _userRepository;
        private readonly IMessageDialog _messageDialog;
        private readonly IMapper _mapper;
        private static ILogger _logger = Log.ForContext<UsersViewModel>();

        private bool _loading;
        public bool Loading
        {
            get { return _loading; }
            private set { SetProperty(ref _loading, value); }
        }

        public BindingList<UserListItemDto> Users { get; }

        public CreateUserDto CreateUserInfo { get; }

        public ICommand CreateUserCommand { get; }

        public ICommand CancelUserCreationCommand { get; }

        public UsersViewModel(IUserRepository userRepository, IMessageDialog messageDialog, IMapper mapper)
        {
            _userRepository = userRepository;
            _messageDialog = messageDialog;
            _mapper = mapper;
            this.Users = new BindingList<UserListItemDto>();
            this.CreateUserInfo = new CreateUserDto();
            this.CreateUserCommand = Command.From(CreateUser);
            this.CancelUserCreationCommand = Command.From(ClearCreateForm);
        }

        public async Task Load()
        {
            try
            {
                ApplicationDispatcher.Invoke(() => this.Loading = true);
                var users = await _userRepository.GetAll();
                var mappedUsers = users.Select(_mapper.Map<UserListItemDto>).ToList();
                ApplicationDispatcher.Invoke(() => Users.AddRange(mappedUsers));
            }
            catch (Exception ex)
            {
                _messageDialog.Show(title:General.ErrorLoadingUsersTitle, message: ex.Message);
            }
            finally
            {
                ApplicationDispatcher.Invoke(() => this.Loading = false);
            }
        }

        public bool CanCreateUser => GenericValidator.TryValidate(CreateUserInfo, out _);

        public async Task<bool> CreateUser()
        {
            try
            {
                if(!GenericValidator.TryValidate(CreateUserInfo, out var validationResults))
                {
                    _messageDialog.Show(validationResults);
                    return false;
                }
                var newUser = _mapper.Map<User>(CreateUserInfo);
                var createdId = await _userRepository.CreateUser(newUser);
                Users.Add(_mapper.Map<UserListItemDto>(newUser));
                _messageDialog.Show(
                    title: General.UserCreatedTitle,
                    message: string.Format(General.UserCreatedMessage, newUser.FirstName, createdId));
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                _messageDialog.Show(title: General.UserCreationFailedTitle, message: ex.Message);
                return false;
            } 
            finally
            {
                ClearCreateForm();
            }
        }

        private void ClearCreateForm()
        {
            ApplicationDispatcher.Invoke(() =>
            {
                CreateUserInfo.FirstName = string.Empty;
                CreateUserInfo.LastName = string.Empty;
                CreateUserInfo.Email = string.Empty;
            });
        }
    }
}
