using AutoMapper;
using System;
using System.Linq;
using System.Threading.Tasks;
using WinformsTools.MVVM.Components;
using UserManager.DTOs;
using WinformsTools.MVVM.Bindings;
using WinformsTools.MVVM.Core;
using UserManager.Resources;
using Easy.MessageHub;
using UserManager.Events;
using System.Windows.Forms;
using UserManager.BusinessLogic.DataAccess.Repositories;
using Serilog;
using UserManager.BusinessLogic.Extensions;
using WinformsTools.Common.Extensions;
using WinformsTools.MVVM.Controls.SnackbarControl;

namespace UserManager.ViewModels
{
    public class UsersViewModel : BindableObject, IViewModel
    {
        private static ILogger _logger = Log.ForContext<UsersViewModel>();
        private readonly IUserRepository _userRepository;
        private readonly IMessageDialog _messageDialog;
        private readonly IMapper _mapper;
        private readonly IMessageHub _eventAggregator;
        private readonly ISnackbarProvider _snackbarProvider;

        private bool _loading;
        public bool Loading
        {
            get => _loading;
            private set => SetProperty(ref _loading, value);
        }

        public AdvancedBindingList<UserListItemDto> Users { get; }

        public ICommand<UserListItemDto> DeleteUserCommand { get; }

        public UsersViewModel(
            IUserRepository userRepository,
            IMessageDialog messageDialog,
            IMapper mapper,
            IMessageHub eventAggregator,
            ISnackbarProvider snackbarProvider)
        {
            _userRepository = userRepository;
            _messageDialog = messageDialog;
            _mapper = mapper;
            _eventAggregator = eventAggregator;
            _snackbarProvider = snackbarProvider;

            this.Users = new AdvancedBindingList<UserListItemDto>();
            this.DeleteUserCommand = Command.From<UserListItemDto>(DeleteUser);
            SubscribeToEvents();
        }

        private void SubscribeToEvents()
        {
            _eventAggregator.Subscribe<UserCreatedEvent>(OnUserCreated);
        }

        private async Task DeleteUser(UserListItemDto user)
        {
            var result = _messageDialog.Show(
                title: General.DeleteUserTitle,
                message: string.Format(General.DeleteUserQuestion, user.Fullname),
                buttons: MessageBoxButtons.YesNo,
                icon: MessageBoxIcon.Warning);
            
            if(result == DialogResult.No)
            {
                return;
            }

            await _userRepository.Remove(user.Id);
            Users.Remove(user);
            _snackbarProvider.Get(this).Show(string.Format(General.UserDeletedMessage, user.Fullname));
        }

        public async Task Load()
        {
            try
            {
                this.Loading = true;
                var users = await _userRepository.GetAll();
                var mappedUsers = users.Select(_mapper.Map<UserListItemDto>).ToList();
                ApplicationDispatcher.Invoke(() => Users.AddRange(mappedUsers));
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                _messageDialog.ShowError(title: General.ErrorLoadingUsersTitle, message: ex.Message);
            }
            finally
            {
                this.Loading = false;
            }
        }

        public void OnUserCreated(UserCreatedEvent evt)
        {
            var message = string.Format(General.UserCreatedMessage, evt.CreatedUser.FirstName, evt.CreatedUser.Id);
            _snackbarProvider.Get(this).Show(message);
            Users.Add(_mapper.Map<UserListItemDto>(evt.CreatedUser));
        }
    }
}
