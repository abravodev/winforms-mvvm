using AutoMapper;
using Easy.MessageHub;
using WinformsTools.MVVM.Components;
using WinformsTools.MVVM.Core;
using Serilog;
using System;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using UserManager.BusinessLogic.DataAccess.Repositories;
using UserManager.BusinessLogic.Extensions;
using UserManager.BusinessLogic.Model;
using UserManager.Common.Extensions;
using UserManager.DTOs;
using UserManager.Events;
using UserManager.Resources;

namespace UserManager.ViewModels
{
    public class CreateUserViewModel : IViewModel
    {
        private static ILogger _logger = Log.ForContext<CreateUserViewModel>();

        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IMessageDialog _messageDialog;
        private readonly IMapper _mapper;
        private readonly IMessageHub _eventAggregator;

        public CreateUserDto CreateUserInfo { get; }

        public ICommand CreateUserCommand { get; }

        public ICommand CancelUserCreationCommand { get; }

        public BindingList<RoleSelectDto> Roles { get; }

        public CreateUserViewModel(
            IUserRepository userRepository,
            IRoleRepository roleRepository,
            IMessageDialog messageDialog,
            IMapper mapper,
            IMessageHub eventAggregator)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _messageDialog = messageDialog;
            _mapper = mapper;
            _eventAggregator = eventAggregator;

            this.CreateUserInfo = new CreateUserDto();
            this.CreateUserCommand = Command.From(CreateUser);
            this.CancelUserCreationCommand = Command.From(ClearCreateForm);

            this.Roles = new BindingList<RoleSelectDto>();
        }

        public async Task Load()
        {
            var roles = await _roleRepository.GetAll();
            var mappedRoles = roles.Select(_mapper.Map<RoleSelectDto>);
            ApplicationDispatcher.Invoke(() => this.Roles.AddRange(mappedRoles));
        }

        public bool CanCreateUser => GenericValidator.TryValidate(CreateUserInfo, out _);

        public async Task<bool> CreateUser()
        {
            try
            {
                if (!GenericValidator.TryValidate(CreateUserInfo, out var validationResults))
                {
                    _messageDialog.Show(validationResults);
                    return false;
                }
                var newUser = _mapper.Map<User>(CreateUserInfo);
                var createdId = await _userRepository.CreateUser(newUser);
                _eventAggregator.Publish(new UserCreatedEvent(newUser));
                _messageDialog.Show(
                    title: General.UserCreatedTitle,
                    message: string.Format(General.UserCreatedMessage, newUser.FirstName, createdId));
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                _messageDialog.ShowError(title: General.UserCreationFailedTitle, message: ex.Message);
                return false;
            }
            finally
            {
                ClearCreateForm();
            }
        }

        private void ClearCreateForm()
        {
            CreateUserInfo.FirstName = string.Empty;
            CreateUserInfo.LastName = string.Empty;
            CreateUserInfo.Email = string.Empty;
            CreateUserInfo.Role = this.Roles.First(x => x.Id == Role.Basic.Id);
        }
    }
}
