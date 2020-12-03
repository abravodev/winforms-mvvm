using AutoMapper;
using System;
using System.ComponentModel;
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

namespace UserManager.ViewModels
{
    public class UsersViewModel : BindableObject, IViewModel
    {
        private static ILogger _logger = Log.ForContext<UsersViewModel>();
        private readonly IUserRepository _userRepository;
        private readonly IMessageDialog _messageDialog;
        private readonly IMapper _mapper;
        private readonly IMessageHub _eventAggregator;

        private bool _loading;

        public bool Loading
        {
            get { return _loading; }
            private set { SetProperty(ref _loading, value); }
        }

        public BindingList<UserListItemDto> Users { get; }

        public ICommand<UserListItemDto> DeleteUserCommand { get; }

        public UsersViewModel(
            IUserRepository userRepository,
            IMessageDialog messageDialog,
            IMapper mapper,
            IMessageHub eventAggregator)
        {
            _userRepository = userRepository;
            _messageDialog = messageDialog;
            _mapper = mapper;
            _eventAggregator = eventAggregator;

            this.Users = new BindingList<UserListItemDto>();
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
            _messageDialog.Show(
                title: General.UserDeletedTitle,
                message: string.Format(General.UserDeletedMessage, user.Fullname));
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
                _messageDialog.ShowError(title:General.ErrorLoadingUsersTitle, message: ex.Message);
            }
            finally
            {
                this.Loading = false;
            }
        }

        public void OnUserCreated(UserCreatedEvent evt)
        {
            Users.Add(_mapper.Map<UserListItemDto>(evt.CreatedUser));
        }
    }
}
